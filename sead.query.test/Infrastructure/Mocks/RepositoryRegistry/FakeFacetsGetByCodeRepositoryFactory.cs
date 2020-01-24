using Moq;
using SeadQueryCore;
using SeadQueryTest.Fixtures;

namespace SeadQueryTest.Mocks
{
    internal static class FakeFacetsGetByCodeRepositoryFactory
    {
        public static IRepositoryRegistry Create()
        {

            var mockRegistry = new Mock<IRepositoryRegistry>();

            mockRegistry.Setup(x => x.Facets.GetByCode(It.IsAny<string>()))
                .Returns((string facetCode) => FacetFixtures.Store[facetCode]);

            return mockRegistry.Object;
        }

    }
}
