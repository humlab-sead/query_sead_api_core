using Autofac.Features.Indexed;
using QueryFacetDomain.QueryBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static QueryFacetDomain.Utility;

namespace QueryFacetDomain
{
    public interface ICategoryCountService {
        Dictionary<string, CategeryCountValue> Load(string facetCode, FacetsConfig2 facetsConfig, string intervalQuery);
    }

    public class CategoryCountService : QueryServiceBase, ICategoryCountService {


        public class CategeryCountValue {
            public string Category { get; set; }
            public int Count { get; set; }
            public Dictionary<EFacetPickType, decimal> Details;
        }

        public CategoryCountService(IQueryBuilderSetting config, IUnitOfWork context, IQuerySetupBuilder builder) : base(config, context, builder)
        {
        }

        public Dictionary<string, CategeryCountValue> Load(string facetCode, FacetsConfig2 facetsConfig, string intervalQuery=null)
        {
            FacetDefinition facet = Context.Facets.GetByCode(facetCode);
            string sql = Compile(facet, facetsConfig, intervalQuery);
            Dictionary<string, CategeryCountValue> data = Query(sql).ToDictionary(z => z.Category);
            return data; 
        }

        protected virtual IEnumerable<CategeryCountValue> Query(string sql)
        {
            throw new NotImplementedException();
        }

        protected virtual string Compile(FacetDefinition facet, FacetsConfig2 facetsConfig, string intervalQuery)
        {
            throw new NotImplementedException();
        }
    }

    public class RangeCategoryCountService : CategoryCountService {

        public RangeCategoryCountService(IQueryBuilderSetting config, IUnitOfWork context, IQuerySetupBuilder builder) : base(config, context, builder) { }

        protected override string Compile(FacetDefinition facet, FacetsConfig2 facetsConfig, string intervalQuery)
        {
            List<string> tables = new List<string>() { facet.TargetTableName, Config.DirectCountTable };
            QueryBuilder.QuerySetup query = QueryBuilder.Build(facetsConfig, facet.FacetCode, tables);
            string sql = RangeCounterSqlQueryBuilder.compile(query, facet, intervalQuery, Config.DirectCountColumn);
            return sql;
        }

        protected override IEnumerable<CategeryCountValue> Query(string sql)
        {
            return Context.QueryRows<CategeryCountValue>(sql,
                x => new CategeryCountValue() {
                    Category = x.GetInt32(0).ToString(),
                    Count = x.GetInt32(1),
                    Details = new Dictionary<EFacetPickType, decimal>() {
                        { EFacetPickType.lower, x.GetInt32(2) },
                        { EFacetPickType.upper, x.GetInt32(3) } }
                });
        }
    }

    public class DiscreteCategoryCountService : CategoryCountService {

        public DiscreteCategoryCountService(IQueryBuilderSetting config, IUnitOfWork context, IQuerySetupBuilder builder) : base(config, context, builder) { }

        protected override string Compile(FacetDefinition facet, FacetsConfig2 facetsConfig, string payload)
        {
            FacetDefinition countFacet = Context.Facets.Get(facet.AggregateFacetId); // default to ID 1 = "result_facet"

            string targetCode = Coalesce(facetsConfig?.TargetCode, countFacet.FacetCode);

            FacetDefinition targetFacet = Context.Facets.GetByCode(targetCode);

            List<string> extraTables = CollectTables(facetsConfig, targetFacet, countFacet);
            List<string> facetCodes  = array_insert_before_existing(facetsConfig.GetFacetCodes(), targetFacet.FacetCode, countFacet.FacetCode);

            QueryBuilder.QuerySetup query = QueryBuilder.Build(facetsConfig, countFacet.FacetCode, extraTables, facetCodes);
            string sql = DiscreteCounterSqlQueryBuilder.compile(query, targetFacet, countFacet, Coalesce(facet.AggregateType, "count"));
            return sql;
        }

        private List<string> CollectTables(FacetsConfig2 facetsConfig, FacetDefinition targetFacet, FacetDefinition countFacet)
        {
            List<string> tables = targetFacet.ExtraTables.Select(x => x.TableName).ToList();
            if (facetsConfig.TargetCode != null) {
                tables.Add(targetFacet.ResolvedName);
            }
            if (countFacet.FacetCode != targetFacet.FacetCode) {
                tables.Add(countFacet.TargetTableName);
            }
            return tables;
        }

        protected override IEnumerable<CategeryCountValue> Query(string sql)
        {
            return Context.QueryRows<CategeryCountValue>(sql,
                x => new CategeryCountValue() {
                    Category = x.GetInt32(0).ToString(),
                    Count = x.GetInt32(1),
                    Details = new Dictionary<EFacetPickType, decimal>() { {  EFacetPickType.discrete, x.GetInt32(0) } }
                });
        }
    }

}
