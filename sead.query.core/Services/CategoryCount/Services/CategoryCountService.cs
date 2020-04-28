using SeadQueryCore.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static SeadQueryCore.Utility;

namespace SeadQueryCore
{

    public class CategoryCountService : QueryServiceBase, ICategoryCountService {

        protected IFacetRepository Facets { get; set; }
        public ITypedQueryProxy QueryProxy { get; }

        public class CategoryCountData {
            public string SqlQuery { get; set; }
            public Dictionary<string, CategoryCountItem> CategoryCounts { get; set; }
        }
 
        public CategoryCountService(IFacetSetting config, IRepositoryRegistry registry, IQuerySetupBuilder builder, ITypedQueryProxy queryProxy) : base(registry, builder)
        {
            Config = config;
            Facets = Registry.Facets;
            QueryProxy = queryProxy;
        }

        public IFacetSetting Config { get; }

        public virtual CategoryCountData Load(string facetCode, FacetsConfig2 facetsConfig, string intervalQuery=null)
        {
            var sqlQuery = Compile(Facets.GetByCode(facetCode), facetsConfig, intervalQuery);
            var categoryCounts = Query(sqlQuery).ToDictionary(z => Coalesce(z.Category, "(null)"));

            return new CategoryCountData {
                SqlQuery = sqlQuery,
                CategoryCounts = categoryCounts
            };
        }

        protected virtual List<CategoryCountItem> Query(string sql)
        {
            return QueryProxy.QueryRows<CategoryCountItem>(sql,
                x => new CategoryCountItem()
                {
                    Category = GetCategory(x),
                    Count = GetCount(x),
                    Extent = GetExtent(x)
                }).ToList();
        }

        protected virtual string GetCategory(IDataReader x)         => throw new NotSupportedException();
        protected virtual int GetCount(IDataReader x)               => throw new NotSupportedException();
        protected virtual List<decimal> GetExtent(IDataReader x)    => throw new NotSupportedException();

        protected virtual string Compile(Facet facet, FacetsConfig2 facetsConfig, string intervalQuery) => throw new NotSupportedException();
    }

}
