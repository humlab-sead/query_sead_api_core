using Moq;
using SeadQueryCore;
using SeadQueryInfra;
using SeadQueryTest.Infrastructure;
using SeadQueryTest.Mocks;
using System;
using Xunit;

namespace SeadQueryTest.Repository
{
    [Collection("JsonSeededFacetContext")]
    public class FacetTypeRepositoryTests : DisposableFacetContextContainer
    {
        public FacetTypeRepositoryTests(JsonSeededFacetContextFixture fixture) : base(fixture)
        {
        }

        private FacetTypeRepository CreateFacetTypeRepository()
        {
            return new FacetTypeRepository(FacetContext);
        }

        [Theory]
        [InlineData(EFacetType.Discrete)]
        [InlineData(EFacetType.Range)]
        public void Find_WhenCalleWithExistingId_ReturnsType(EFacetType facetType)
        {
            // Arrange
            var repository = new Repository<FacetType, EFacetType>(FacetContext);

            // Act
            var result = repository.Get(facetType);

            // Assert
            Assert.Equal(facetType, result.FacetTypeId);
        }
    }
}
