using Moq;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using SeadQueryTest.Infrastructure;
using SeadQueryTest.Mocks;
using System;
using Xunit;

namespace SeadQueryTest.Services.CategoryCount
{

    [Collection("JsonSeededFacetContext")]
    public class CategoryCountServiceTests : DisposableFacetContextContainer
    {
        public CategoryCountServiceTests(JsonSeededFacetContextFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData("sites:sites")]
        public void Load_UsingVariousConfigs_LoadsSuccessfully(string uri)
        {
            // Arrange
            var fakeSettings = FakeFacetSetting();
            var fakeRegistry = FakeRegistry();

            var fakeFacetsConfig = new MockFacetsConfigFactory(Registry.Facets).Create(uri);

            var mockQuerySetupBuilder = MockQuerySetupBuilder(new QuerySetup { /* not used */ });
            var mockCategoryCountSqlQueryCompiler = MockDiscreteCategoryCountSqlQueryCompiler(returnSql: "SELECT * FROM foot.bar");
            var queryProxy = new MockTypedQueryProxyFactory()
                .Create<DiscreteCountDataReaderBuilder, CategoryCountItem>(3);

            // Act

            var service = new DiscreteCategoryCountService(
                fakeSettings,
                fakeRegistry,
                mockQuerySetupBuilder.Object,
                mockCategoryCountSqlQueryCompiler.Object,
                queryProxy.Object
            );

            var result = service.Load(fakeFacetsConfig.TargetCode, fakeFacetsConfig);

            // Assert
            Assert.NotNull(result);
        }
 
    }
}
