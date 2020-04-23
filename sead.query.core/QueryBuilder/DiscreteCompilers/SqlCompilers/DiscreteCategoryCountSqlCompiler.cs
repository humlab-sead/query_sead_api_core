﻿
namespace SeadQueryCore
{
    public class DiscreteCategoryCountSqlCompiler : IDiscreteCategoryCountQueryCompiler
    {
        public string Compile(QueryBuilder.QuerySetup query, Facet facet, Facet countFacet, string aggType)
        {
            string sql = $@"
            SELECT category, {aggType}(value) AS count
            FROM (
                SELECT {facet.CategoryIdExpr} AS category, {countFacet.CategoryIdExpr} AS value
                FROM {query.Facet.TargetTable.ResolvedSqlJoinName}
                     {query.Joins.Combine("\t\t\t\t\t\n")}
                WHERE 1 = 1
                {"AND ".GlueTo(query.Criterias.Combine(" AND "))}
                GROUP BY {facet.CategoryIdExpr}, {countFacet.CategoryIdExpr}
            ) AS x
            GROUP BY category;
        ";
            return sql;
        }
    }
}