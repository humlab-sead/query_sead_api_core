﻿using SeadQueryCore.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static SeadQueryCore.Utility;

namespace SeadQueryCore
{
    public class DiscreteCategoryCountService : CategoryCountService, IDiscreteCategoryCountService
    {

        public DiscreteCategoryCountService(
            IFacetSetting config,
            IRepositoryRegistry registry,
            IQuerySetupBuilder builder,
            IDiscreteCategoryCountQueryCompiler countSqlCompiler,
            ITypedQueryProxy queryProxy) : base(config, registry, builder, queryProxy) {
            CountSqlCompiler = countSqlCompiler;
        }

        public IDiscreteCategoryCountQueryCompiler CountSqlCompiler { get; }

        protected override string Compile(Facet facet, FacetsConfig2 facetsConfig, string payload)
        {
            Facet computeFacet = Facets.Get(facet.AggregateFacetId);
            Facet targetFacet  = Facets.GetByCode(facetsConfig.TargetCode);

            List<string> tables     = GetTables(facetsConfig, targetFacet, computeFacet);
            List<string> facetCodes = facetsConfig.GetFacetCodes();

            facetCodes.InsertAt(targetFacet.FacetCode, computeFacet.FacetCode);

            QuerySetup query = QuerySetupBuilder.Build(facetsConfig, computeFacet, tables, facetCodes);
            string sql = CountSqlCompiler.Compile(query, targetFacet, computeFacet, Coalesce(facet.AggregateType, "count"));
            return sql;
        }

        private List<string> GetTables(FacetsConfig2 facetsConfig, Facet targetFacet, Facet computeFacet)
        {
            List<string> tables = targetFacet.Tables.Select(x => x.ResolvedAliasOrTableOrUdfName).ToList();
            if (computeFacet.FacetCode != targetFacet.FacetCode) {
                tables.AddRange(computeFacet.Tables.Select(x => x.ResolvedAliasOrTableOrUdfName));
            }
            return tables.Distinct().ToList();
        }

        private string Category2String(IDataReader x, int ordinal)
        {
            if (x.GetDataTypeName(ordinal) == "numeric")
                return String.Format("{0:0.####}", x.GetDecimal(ordinal));
            return x.GetInt32(ordinal).ToString();
        }

        protected override string GetCategory(IDataReader x)
            => x.IsDBNull(0) ? null : Category2String(x, 0);

        protected override int GetCount(IDataReader x)
            => x.IsDBNull(1) ? 0 : x.GetInt32(1);

        protected override List<decimal> GetExtent(IDataReader x)
            => new List<decimal>() { x.IsDBNull(1) ? 0 : x.GetInt32(1) };
    }
}
