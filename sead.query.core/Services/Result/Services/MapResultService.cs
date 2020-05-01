﻿using System.Collections.Generic;
using Autofac.Features.Indexed;
using SeadQueryCore.Model;
using SeadQueryCore.QueryBuilder;

namespace SeadQueryCore.Services.Result
{
    public class MapResultService : DefaultResultService
    {

        public ICategoryCountService CategoryCountService { get; set; }

        private readonly string ResultKey = "map_result";

        public MapResultService(
            IRepositoryRegistry repositoryRegistry,
            IResultConfigCompiler resultConfigCompiler,
            IDiscreteCategoryCountService categoryCountService,
            IDynamicQueryProxy queryProxy
        )
            : base(repositoryRegistry, resultConfigCompiler, queryProxy)
        {
            ResultCode = "map_result";
            CategoryCountService = categoryCountService;
        }

        public override ResultContentSet Load(FacetsConfig2 facetsConfig, ResultConfig resultConfig)
        {
            resultConfig.AggregateKeys = new List<string>() { ResultKey };
            return base.Load(facetsConfig, resultConfig);
        }

        private CategoryCountService.CategoryCountData GetCategoryCounts(FacetsConfig2 facetsConfig)
        {
            return CategoryCountService.Load(ResultCode, facetsConfig, null);
        }

        protected override dynamic GetExtraPayload(FacetsConfig2 facetsConfig)
        {
            var filtered = GetCategoryCounts(facetsConfig)?.CategoryCounts ?? new Dictionary<string, CategoryCountItem>();
            var unfiltered = facetsConfig.HasPicks() ? GetCategoryCounts(facetsConfig.ClearPicks())?.CategoryCounts : filtered;
            return new {
                FilteredCategoryCounts = filtered,
                FullCategoryCounts = unfiltered
            };
        }

        protected override string CompileSql(FacetsConfig2 facetsConfig, ResultConfig resultConfig)
        {
            // TODO This override seems redundant - same call as in base?
            return ResultConfigCompiler.Compile(facetsConfig, resultConfig, ResultCode);
        }
    }

}
