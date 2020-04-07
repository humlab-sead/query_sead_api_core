﻿using Microsoft.EntityFrameworkCore;
using SeadQueryCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;

namespace SeadQueryInfra
{
    public class RepositoryRegistry : IRepositoryRegistry {

        public FacetContext Context { get; private set; }

        public RepositoryRegistry(IFacetContext _context)
        {
            Context = (FacetContext)_context;
            Facets = new FacetRepository(Context);
            TableRelations = new TableRelationRepository(Context);
            Tables = new TableRepository(Context);
            Results = new ResultRepository(Context);
            FacetGroups = new FacetGroupRepository(Context);
            FacetTypes = new FacetTypeRepository(Context);
            FacetTables = new FacetTableRepository(Context);
            ViewStates = new ViewStateRepository(Context);
        }

        public IFacetRepository Facets { get; private set; }
        public ITableRelationRepository TableRelations { get; private set; }
        public ITableRepository Tables { get; private set; }
        public IResultRepository Results { get; private set; }
        public IFacetGroupRepository FacetGroups { get; private set; }
        public IFacetTypeRepository FacetTypes { get; private set; }
        public IViewStateRepository ViewStates { get; private set; }

        public IFacetTableRepository FacetTables { get; private set; }

        public int Commit() => Context.SaveChanges();
        public void Dispose() => Context.Dispose();

    }

    public static class QueryDynamicExt {

        public static IEnumerable<T> Populate<T>(this DbDataReader dr) where T : class
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            while (dr.Read()) {
                var item = Activator.CreateInstance<T>();
                var index = 0;
                foreach (var property in typeof(T).GetProperties()) {
                    Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    property.SetValue(item, Convert.ChangeType(dr.GetValue(index), convertTo), null);
                    index++;
                }
                yield return item;
            }
        }

        public static T Populate2<T>(this DbDataReader dr, T instance) where T : class
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            var item = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties()) {
                if (!dr.IsDBNull(dr.GetOrdinal(property.Name))) {
                    Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    property.SetValue(item, Convert.ChangeType(dr[property.Name], convertTo), null);
                }
            }
            return item;
        }

        public static IEnumerable<dynamic> QueryDynamic2(this DbContext dbContext, string Sql, Dictionary<string, object> Parameters)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand()) {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                AssignParams(Parameters, cmd);
                using (var dataReader = cmd.ExecuteReader()) {
                    while (dataReader.Read()) {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;
                    }
                }
            }
        }

        private static void AssignParams(Dictionary<string, object> Parameters, DbCommand cmd)
        {
            foreach (KeyValuePair<string, object> param in Parameters) {
                DbParameter dbParameter = cmd.CreateParameter();
                dbParameter.ParameterName = param.Key;
                dbParameter.Value = param.Value;
                cmd.Parameters.Add(dbParameter);
            }
        }

        private static dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
            return dataRow;
        }
    }
}
