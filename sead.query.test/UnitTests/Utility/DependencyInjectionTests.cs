using Autofac;
using SeadQueryAPI.Services;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using SeadQueryCore.Services.Result;
using SQT.Infrastructure;
using System;
using System.Diagnostics;
using Xunit;

namespace SQT.IoC
{

    public interface IDependent {
        void DoSomeThingNice();
    }

    public class MyDependent : IDependent {
        public void DoSomeThingNice()
        {
            Debug.Write("Hello World!");
            Console.Write("HEJ");
        }
    }

    public interface IMyController {
        void CallMyDependent();
    }

    public class MyController : IMyController {

        public IDependent Dependent { get; set; }

        public void CallMyDependent()
        {
            Dependent.DoSomeThingNice();
        }

    }

    public class MyController2 : MyController
    {
        public MyController2(IDependent dependent)
        {
            this.Dependent = dependent;
        }
    }

    public class DependencyInjectionTests
    {
        [Fact]
        public void TestResolveService()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IDependent>(new MyDependent());
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope()) {
                var service = scope.Resolve<IDependent>();
                service.DoSomeThingNice();
            }
        }

        [Fact]
        public void TestResolveDependent()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IDependent>(new MyDependent()); //.As<IDependent>();
            builder.RegisterType<MyController2>().As<IMyController>();
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope()) {
                var service = scope.Resolve<IMyController>();
                service.CallMyDependent();
            }
        }

        [Fact]
        public void TestMemberInjection()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IDependent>(new MyDependent()); //.As<IDependent>();
            builder.RegisterType<MyController>().As<IMyController>()
                .OnActivating(e => { e.Instance.Dependent = e.Context.Resolve<IDependent>(); });

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope()) {
                var service = scope.Resolve<IMyController>();
                service.CallMyDependent();
            }
        }

        [Fact]
        public void CanResolveRegisteredDependencies()
        {
            //using (var context = JsonSeededFacetContextFactory.Create())
            using (var container = TestDependencyService.CreateContainer(null, null))
            using (var scope = container.BeginLifetimeScope()) {
                Assert.NotNull(scope.Resolve<ISetting>());
                Assert.NotNull(scope.Resolve<ISeadQueryCache>());
                Assert.NotNull(scope.Resolve<IFacetContext>());
                Assert.NotNull(scope.Resolve<IRepositoryRegistry>());
                Assert.NotNull(scope.Resolve<IFacetGraphFactory>());
                Assert.NotNull(scope.Resolve<IFacetsGraph>());
                Assert.NotNull(scope.Resolve<IQuerySetupBuilder>());
                Assert.NotNull(scope.Resolve<IDiscreteBogusPickService>());
                Assert.NotNull(scope.ResolveKeyed<IFacetContentService>(EFacetType.Discrete));
                Assert.NotNull(scope.ResolveKeyed<IFacetContentService>(EFacetType.Range));
                Assert.NotNull(scope.ResolveKeyed<ICategoryCountService>(EFacetType.Discrete));
                Assert.NotNull(scope.ResolveKeyed<ICategoryCountService>(EFacetType.Range));
                Assert.NotNull(scope.ResolveKeyed<IFacetContentService>(EFacetType.Discrete));
                Assert.NotNull(scope.ResolveKeyed<IFacetContentService>(EFacetType.Range));
                Assert.NotNull(scope.ResolveKeyed<ICategoryBoundSqlCompiler>(EFacetType.Range));
                Assert.NotNull(scope.ResolveKeyed<IResultSqlCompiler>("tabular"));
                Assert.NotNull(scope.ResolveKeyed<IResultSqlCompiler>("map"));
                Assert.NotNull(scope.ResolveKeyed<IResultService>("tabular"));
                Assert.NotNull(scope.ResolveKeyed<IResultService>("map"));

                Assert.NotNull(scope.Resolve<IResultConfigCompiler>());
                Assert.NotNull(scope.Resolve<IFacetReconstituteService>());
                Assert.NotNull(scope.Resolve<ILoadResultService>());
            }
        }

        [Fact]
        public void CannotResolveGeoDependency()
        {
            using (var container = TestDependencyService.CreateContainer(null, null))
            using (var scope = container.BeginLifetimeScope()) {

                Assert.Throws<Autofac.Core.Registration.ComponentNotRegisteredException>(() => scope.ResolveKeyed<IFacetContentService>(EFacetType.Geo));

            }
        }
    }
}
