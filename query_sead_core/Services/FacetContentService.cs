using Autofac.Features.Indexed;
using QuerySeadDomain.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace QuerySeadDomain
{
    using CatCountDict = Dictionary<string, CategoryCountItem>;

    public interface IFacetContentServiceAggregate {
        DiscreteFacetContentService DiscreteFacetContentService { get; set; }
        RangeFacetContentService RangeFacetContentService { get; set; }
    }

    public interface IFacetContentService {
        FacetContent Load(FacetsConfig2 facetsConfig);
    }

    public class FacetContentService : QueryServiceBase, IFacetContentService {

        public ICategoryCountService CountService { get; set; }

        public FacetContentService(IQueryBuilderSetting config, IUnitOfWork context, IQuerySetupBuilder builder) : base(config, context, builder)
        {
        }

        public FacetContent Load(FacetsConfig2 facetsConfig)
        {
            (int interval, string intervalQuery) = CompileIntervalQuery(facetsConfig, facetsConfig.TargetCode);
            CatCountDict distribution = GetCategoryCounts(facetsConfig, intervalQuery);
            List<FacetContent.ContentItem> items = CompileItems(intervalQuery, distribution).ToList();
            Dictionary<string, FacetsConfig2.UserPickData> picks = facetsConfig.CollectUserPicks(facetsConfig.TargetCode);
            FacetContent facetContent = new FacetContent(facetsConfig, items, distribution, picks, interval, intervalQuery);
            return facetContent;
        }

        protected virtual (int,string) CompileIntervalQuery(FacetsConfig2 facetsConfig, string facetCode, int interval=0) => (0, "");

        private CatCountDict GetCategoryCounts(FacetsConfig2 facetsConfig, string intervalQuery)
        {
            CatCountDict categoryCounts = CountService.Load(facetsConfig.TargetCode, facetsConfig, intervalQuery);
            return categoryCounts;
        }

        protected List<FacetContent.ContentItem> CompileItems(string intervalQuery, CatCountDict distribution)
        {
            var rows = Context.QueryRows(intervalQuery, dr => CreateItem(dr, distribution)).ToList();
            return rows;
        }

        protected FacetContent.ContentItem CreateItem(DbDataReader dr, CatCountDict distribution)
        {
            string category = GetCategory(dr);
            string name = GetName(dr);
            CategoryCountItem countValue = distribution.ContainsKey(category) ? distribution[category] : null;
            return new FacetContent.ContentItem() {
                // FacetType = EFacetType.Unknown,
                Category = category,
                DisplayName = name,
                Name = name,
                Value = countValue
            };
        }

        protected virtual string GetCategory(DbDataReader dr) => dr.GetInt32(0).ToString();
        protected virtual string GetName(DbDataReader dr) => dr.GetString(1);
    }

    public class DiscreteFacetContentService : FacetContentService {
        public DiscreteFacetContentService(IQueryBuilderSetting config, IUnitOfWork context, IQuerySetupBuilder builder,
            IIndex<EFacetType, ICategoryCountService> countServices) : base(config, context, builder)
        {
            CountService = countServices[EFacetType.Discrete];
        }

        protected override (int, string) CompileIntervalQuery(FacetsConfig2 facetsConfig, string facetCode, int count=0)
        {
            QuerySetup query = QueryBuilder.Build(facetsConfig, facetsConfig.TargetCode, null, facetsConfig.GetFacetCodes());
            string sql = DiscreteContentSqlQueryBuilder.compile(query, facetsConfig.TargetFacet, facetsConfig.GetTargetTextFilter());
            return ( 1, sql );
        }
    }

    public class RangeFacetContentService : FacetContentService {
        public RangeFacetContentService(IQueryBuilderSetting config, IUnitOfWork context, IQuerySetupBuilder builder, IIndex<EFacetType, ICategoryCountService> countServices) : base(config, context, builder)
        {
            CountService = countServices[EFacetType.Range];
        }

        private (decimal, decimal) GetLowerUpperBound(FacetConfig2 config)
        {
            var bounds = config.GetPickedLowerUpperBounds();      // Get client picked bound if exists...
            if (bounds.Count != 2) {
                bounds = config.getStorageLowerUpperBounds();     // ...else fetch from database
            }
            return (bounds[EFacetPickType.lower], bounds[EFacetPickType.upper]);
        }

        protected override (int,string) CompileIntervalQuery(FacetsConfig2 facetsConfig, string facetCode, int interval_count=120)
        {
            (decimal lower, decimal upper) = GetLowerUpperBound(facetsConfig.GetConfig(facetCode));
            int interval = Math.Max((int)Math.Floor((upper - lower) / interval_count), 1);
            string sql = RangeIntervalSqlQueryBuilder.compile(interval, (int)lower, (int)upper, interval_count);
            return ( interval, sql );
        }

        protected override string GetName(DbDataReader dr)
        {
            return $"{dr.GetInt32(1)} to {dr.GetInt32(2)}";
        }
    }
}