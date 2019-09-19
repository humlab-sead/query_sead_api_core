using Moq;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using System;
using Xunit;

namespace SeadQueryTest.Services.CategoryCount
{
    public class DiscreteCategoryCountServiceTests : IDisposable
    {
        private MockRepository mockRepository;

        private Mock<IFacetSetting> mockQueryBuilderSetting;
        private Mock<IRepositoryRegistry> mockRepositoryRegistry;
        private Mock<IQuerySetupCompiler> mockQuerySetupBuilder;
        private Mock<IDiscreteCategoryCountSqlQueryCompiler> mockDiscreteCategoryCountSqlQueryCompiler;

        public DiscreteCategoryCountServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockQueryBuilderSetting = this.mockRepository.Create<IFacetSetting>();
            this.mockRepositoryRegistry = this.mockRepository.Create<IRepositoryRegistry>();
            this.mockQuerySetupBuilder = this.mockRepository.Create<IQuerySetupCompiler>();
            this.mockDiscreteCategoryCountSqlQueryCompiler = this.mockRepository.Create<IDiscreteCategoryCountSqlQueryCompiler>();
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private DiscreteCategoryCountService CreateService()
        {
            return new DiscreteCategoryCountService(
                this.mockQueryBuilderSetting.Object,
                this.mockRepositoryRegistry.Object,
                this.mockQuerySetupBuilder.Object,
                this.mockDiscreteCategoryCountSqlQueryCompiler.Object);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var service = this.CreateService();

            // Act


            // Assert
            Assert.True(false);
        }
    }
}
