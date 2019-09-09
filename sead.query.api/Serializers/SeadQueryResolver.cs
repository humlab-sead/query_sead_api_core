﻿using SeadQueryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeadQueryAPI.Serializers
{
    public class SeadQueryResolver : PropertyRenameAndIgnoreSerializerContractResolver
    {
        public SeadQueryResolver() : base()
        {
            IgnoreProperty(typeof(Facet),
                    "FacetGroupId",
                    "FacetTypeId",
                    "CategoryIdExpr",
                    "CategoryNameExpr",
                    "IconIdExpr",
                    "AggregateFacetId",
                    "SortExpr",
                    "FacetType",
                    "FacetGroup",
                    "Tables",
                    "Clauses",
                    "TargetTable",
                    "TargetTableName",
                    "ExtraTables",
                    "aliasName",
                    "AliasName",
                    "HasAliasName",
                    "ResolvedName",
                    "QueryCriteria"
               );

            IgnoreProperty(typeof(FacetClause), "Facet");

            IgnoreProperty(typeof(FacetsConfig2),
                    "Language",
                    "Context",
                    "facetConfigs",
                    "InactiveConfigs",
                    "TargetFacet",
                    "TriggerFacet",
                    "TargetConfig"
               );
            IgnoreProperty(typeof(FacetConfig2),
                    "Context",
                    "Facet"
               );
        }
    }
}
