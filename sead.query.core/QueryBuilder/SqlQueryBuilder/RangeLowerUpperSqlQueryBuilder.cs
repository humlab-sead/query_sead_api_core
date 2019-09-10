﻿
namespace SeadQueryCore
{
    public class RangeLowerUpperSqlQueryCompiler : IRangeLowerUpperSqlQueryCompiler
    {
        public string Compile(QueryBuilder.QuerySetup query, Facet facet)
        {
            string sql = $@"
          SELECT MIN({facet.CategoryIdExpr}) AS lower, MAX({facet.CategoryIdExpr}) AS upper
          FROM {facet.TargetTableName}
        ";
            return sql;
        }
    }
}