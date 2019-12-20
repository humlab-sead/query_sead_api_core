﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataAccessPostgreSqlProvider;
using Microsoft.Extensions.DependencyInjection;
using SeadQueryAPI.Serializers;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using SeadQueryCore.Services.Result;
using SeadQueryInfra;

namespace SeadQueryAPI
{
    public class DependencyService
    {
        public virtual ISeadQueryCache GetCache(StoreSetting settings)
        {
            try {
                if (settings?.UseRedisCache == true)
                    return new RedisCacheFactory().Create(settings.CacheHost, settings.CachePort);
            } catch (InvalidOperationException) {
                Console.WriteLine("Failed to connect to Redis!");
            }
            Console.WriteLine("Warning: Using in memory cache provider!");
            return new MemoryCacheFactory().Create();
        }

        public virtual IContainer Register(IServiceCollection services, IQueryBuilderSetting options)
        {
            var builder = new Autofac.ContainerBuilder();

            // http://docs.autofac.org/en/latest/register/registration.html

            builder.RegisterInstance<IQueryBuilderSetting>(options).SingleInstance().ExternallyOwned();
            builder.RegisterInstance<IFacetSetting>(options.Facet).SingleInstance().ExternallyOwned();

            builder.RegisterType<FacetContext>().As<IFacetContext>().SingleInstance().InstancePerLifetimeScope();
            builder.RegisterType<RepositoryRegistry>().As<IRepositoryRegistry>().InstancePerLifetimeScope();

            builder.RegisterType<FacetGraphFactory>().As<IFacetGraphFactory>().InstancePerLifetimeScope();
            builder.Register<IFacetsGraph>(c => c.Resolve<IFacetGraphFactory>().Build()).InstancePerLifetimeScope();

            builder.RegisterType<QuerySetupCompiler>().As<IQuerySetupCompiler>();
            builder.RegisterType<DiscreteBogusPickService>().As<IDiscreteBogusPickService>();
            builder.RegisterType<FacetConfigReconstituteService>().As<IFacetConfigReconstituteService>();

            builder.RegisterType<RangeCategoryBoundsService>().As<ICategoryBoundsService>();

            builder.RegisterType<UndefinedFacetPickFilterCompiler>().Keyed<IPickFilterCompiler>(0);
            builder.RegisterType<DiscreteFacetPickFilterCompiler>().Keyed<IPickFilterCompiler>(1);
            builder.RegisterType<RangeFacetPickFilterCompiler>().Keyed<IPickFilterCompiler>(2);
            builder.RegisterType<GeoFacetPickFilterCompiler>().Keyed<IPickFilterCompiler>(3);

            builder.RegisterType<RangeCategoryCountService>().Keyed<ICategoryCountService>(EFacetType.Range);
            builder.RegisterType<DiscreteCategoryCountService>().Keyed<ICategoryCountService>(EFacetType.Discrete);
            builder.RegisterType<DiscreteCategoryCountService>().As<IDiscreteCategoryCountService>();

            builder.RegisterType<ValidPicksSqlQueryCompiler>().As<IValidPicksSqlQueryCompiler>();
            builder.RegisterType<EdgeSqlCompiler>().As<IEdgeSqlCompiler>();
            builder.RegisterType<DiscreteContentSqlQueryBuilder>().As<IDiscreteContentSqlQueryCompiler>();
            builder.RegisterType<DiscreteCategoryCountSqlQueryCompiler>().As<IDiscreteCategoryCountSqlQueryCompiler>();
            builder.RegisterType<RangeCategoryCountSqlQueryCompiler>().As<IRangeCategoryCountSqlQueryCompiler>();
            builder.RegisterType<RangeIntervalSqlQueryCompiler>().As<IRangeIntervalSqlQueryCompiler>();
            builder.RegisterType<RangeOuterBoundSqlCompiler>().As<IRangeOuterBoundSqlCompiler>();
            builder.RegisterType<RangeFacetContentService>().Keyed<IFacetContentService>(EFacetType.Range);
            builder.RegisterType<DiscreteFacetContentService>().Keyed<IFacetContentService>(EFacetType.Discrete);

            builder.RegisterType<ResultCompiler>().As<IResultCompiler>();

            builder.RegisterType<RangeCategoryBoundSqlQueryCompiler>().Keyed<ICategoryBoundSqlQueryCompiler>(EFacetType.Range);

            builder.RegisterType<DefaultResultService>().Keyed<IResultService>("tabular");
            builder.RegisterType<MapResultService>().Keyed<IResultService>("map");

            builder.RegisterType<TabularResultSqlQueryCompiler>().Keyed<IResultSqlQueryCompiler>("tabular");
            builder.RegisterType<MapResultSqlQueryCompiler>().Keyed<IResultSqlQueryCompiler>("map");

            /* App Services */

            builder.Register(_ => GetCache(options?.Store)).SingleInstance().ExternallyOwned();
            if (options.Store.UseRedisCache) {
                builder.RegisterType<Services.CachedLoadFacetService>().As<Services.IFacetReconstituteService>();
                builder.RegisterType<Services.CachedLoadResultService>().As<Services.ILoadResultService>();
            } else {
                builder.RegisterType<Services.LoadFacetService>().As<Services.IFacetReconstituteService>();
                builder.RegisterType<Services.LoadResultService>().As<Services.ILoadResultService>();
            }

            if (services != null)
                builder.Populate(services);

            var container = builder.Build();

            return container;
        }
    }
}
