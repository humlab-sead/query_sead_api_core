using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using DataAccessPostgreSqlProvider;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Autofac;
using QuerySeadDomain;
using QuerySeadDomain.QueryBuilder;

namespace QueryFacetTest {
    [TestClass]
    public class TestFacetsGraph
    {

        [TestMethod]
        public void TestResolveUnitOfWork()
        {
            var builder = new ContainerBuilder();

            // http://docs.autofac.org/en/latest/register/registration.html

            builder.RegisterType<DomainModelDbContext>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope()) {
                var service = scope.Resolve<IUnitOfWork>();
                Assert.IsTrue(service.Facets.GetAll().Count() > 0);
            }
        }

        [TestMethod]
        public void TestResolveFacetsGraph()
        {
            var container = RegisterDependencies.Register();
            using (var scope = container.BeginLifetimeScope()) {
                var service = scope.Resolve<IFacetsGraph>();
                Assert.IsTrue(service.Nodes.Count > 0);
            }
        }

    }
}
