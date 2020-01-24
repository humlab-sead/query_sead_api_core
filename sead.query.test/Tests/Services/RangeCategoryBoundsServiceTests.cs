using Autofac.Features.Indexed;
using Moq;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using System;
using Xunit;

namespace SeadQueryTest.Services
{
    public class RangeCategoryBoundsServiceTests : IDisposable
    {
        private MockRepository mockRepository;

        private Mock<ISetting> mockQueryBuilderSetting;
        private Mock<IRepositoryRegistry> mockRepositoryRegistry;
        private Mock<IQuerySetupCompiler> mockQuerySetupBuilder;
        private Mock<IIndex<EFacetType, ICategoryBoundSqlQueryCompiler>> mockIndex;

        public RangeCategoryBoundsServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockQueryBuilderSetting = this.mockRepository.Create<ISetting>();
            this.mockRepositoryRegistry = this.mockRepository.Create<IRepositoryRegistry>();
            this.mockQuerySetupBuilder = this.mockRepository.Create<IQuerySetupCompiler>();
            this.mockIndex = this.mockRepository.Create<IIndex<EFacetType, ICategoryBoundSqlQueryCompiler>>();
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private RangeCategoryBoundsService CreateService()
        {
            return new RangeCategoryBoundsService(
                this.mockRepositoryRegistry.Object,
                this.mockQuerySetupBuilder.Object,
                this.mockIndex.Object);
        }

        [Fact]
        public void Load_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.Load();

            // Assert
            Assert.True(false);
        }
    }
}
