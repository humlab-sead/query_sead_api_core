using Autofac.Features.Indexed;
using Moq;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using SeadQueryTest.Infrastructure;
using System;
using System.Linq;
using System.Data;
using Xunit;
using SeadQueryTest.Mocks;

namespace SeadQueryTest.Services.FacetContent
{
    public class DiscreteFacetContentServiceTests : DisposableFacetContextContainer
    {
        public DiscreteFacetContentServiceTests(JsonSeededFacetContextFixture fixture) : base(fixture)
        {
        }

        [Fact(Skip = "Not implemented")]
        public void TestMethod1()
        {
            // Arrange
            var config = new SettingFactory().Create().Value.Facet;
            var querySetupCompiler = new Mock<IQuerySetupCompiler>();
            var rangeCountSqlCompiler = new Mock<IRangeCategoryCountSqlQueryCompiler>();
            var categoryCountServices = new Mock<IIndex<EFacetType, ICategoryCountService>>();
            var discreteContentSqlQueryCompiler = new Mock<IDiscreteContentSqlQueryCompiler>();

            var queryProxy = new MockQueryProxyFactory().Create<DiscreteContentDataReaderBuilder, CategoryCountItem>(3);

            //var expectedValues = new DiscreteCountDataReaderBuilder()
            //    .GenerateBogusRows(3)
            //    .ToItems<CategoryCountItem>()
            //    .ToList();

            //queryProxy.Setup(foo => foo.QueryRows(It.IsAny<string>(), It.IsAny<Func<IDataReader, CategoryCountItem>>())).Returns(
            //    expectedValues
            //);

            // Act
            var service = new DiscreteFacetContentService(
                config,
                Registry,
                querySetupCompiler.Object,
                categoryCountServices.Object,
                discreteContentSqlQueryCompiler.Object,
                queryProxy.Object
             );

            var facetsConfig = new MockFacetsConfigFactory(Registry).Create("sites:sites");

            var result = service.Load(facetsConfig);

            // Assert
            Assert.True(false);
        }
    }
}