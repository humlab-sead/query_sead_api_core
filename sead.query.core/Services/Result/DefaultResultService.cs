
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Indexed;
using SeadQueryCore.Model;
using SeadQueryCore.QueryBuilder;

namespace SeadQueryCore.Services.Result {

    public class DefaultResultService : IResultService
    {
        public IRepositoryRegistry RepositoryRegistry { get; set; }

        public string FacetCode { get; protected set; }

        public IResultCompiler QueryCompiler { get; set; }

        public DefaultResultService(
            IRepositoryRegistry registry,
            IResultCompiler compiler
        )
        {
            RepositoryRegistry = registry;
            FacetCode = "result_facet";
            QueryCompiler = compiler;
        }

        public virtual ResultContentSet Load(FacetsConfig2 facetsConfig, ResultConfig resultConfig)
        {
            string sql = CompileSql(facetsConfig, resultConfig);

            if (Utility.empty(sql))
                return null;

            var resultSet = new TabularResultContentSet(resultConfig, GetResultFields(resultConfig), RepositoryRegistry.Query(sql)) {
                Payload = GetExtraPayload(facetsConfig),
                Query = sql
            };

            return resultSet;
        }

        protected virtual List<ResultAggregateField> GetResultFields(ResultConfig resultConfig)
        {
            return RepositoryRegistry.Results.GetFieldsByKeys(resultConfig.AggregateKeys).Where(z => z.FieldType.IsResultValue).ToList();
        }

        protected virtual dynamic GetExtraPayload(FacetsConfig2 facetsConfig)
        {
            // TODO Check if this really always should be null for tabular results
            return null;
        }

        protected virtual string CompileSql(FacetsConfig2 facetsConfig, ResultConfig resultConfig)
        {
            return QueryCompiler.Compile(facetsConfig, resultConfig, FacetCode);
        }
    }

}