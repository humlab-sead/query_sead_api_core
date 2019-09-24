using Moq;
using SeadQueryCore;
using SeadQueryInfra;
using SeadQueryTest.Infrastructure.Scaffolding;
using System;
using Xunit;

namespace SeadQueryTest.Repository
{
    public class NodeRepositoryTests : IDisposable
    {
        private IFacetContext mockFacetContext;

        public NodeRepositoryTests()
        {
            this.mockFacetContext = ScaffoldUtility.DefaultFacetContext();
        }

        public void Dispose()
        {
        }

        private NodeRepository CreateRepository()
        {
            return new NodeRepository(this.mockFacetContext);
        }

        [Fact]
        public void Find_WhenCalleWithExistingId_ReturnsType()
        {
            // Arrange
            var repository = this.CreateRepository();

            // Act
            var result = repository.Get(1);

            // Assert
            Assert.Equal(1, result.NodeId);
        }
    }
}
