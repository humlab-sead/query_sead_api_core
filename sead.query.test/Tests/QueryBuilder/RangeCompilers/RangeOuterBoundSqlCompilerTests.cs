using DataAccessPostgreSqlProvider;
using Moq;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using SeadQueryInfra;
using SeadQueryTest.Infrastructure.Scaffolding;
using System;
using System.Collections.Generic;
using Xunit;

namespace SeadQueryTest.QueryBuilder.RangeCompilers
{
    public class RangeOuterBoundSqlCompilerTests
    {
        private IFacetSetting mockSettings;
        private RepositoryRegistry mockRegistry;
        private FacetContext mockContext;

        public object ReconstituteFacetConfigService { get; private set; }

        public RangeOuterBoundSqlCompilerTests()
        {
            mockSettings = new MockOptionBuilder().Build().Value.Facet;
            mockContext = ScaffoldUtility.JsonSeededFacetContext();
            mockRegistry = new RepositoryRegistry(mockContext);
        }

        private RangeOuterBoundSqlCompiler CreateRangeOuterBoundSqlCompiler()
        {
            return new RangeOuterBoundSqlCompiler();
        }

        [Fact]
        public void Compile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var rangeOuterBoundSqlCompiler = this.CreateRangeOuterBoundSqlCompiler();
            var facetCode = "tbl_denormalized_measured_values_33_0";

            var facet = SeadQueryTest.Infrastructure.Scaffolds.FacetInstances.Store["tbl_denormalized_measured_values_33_0"];

            var targetFacetConfig = new FacetConfig2 {
                FacetCode = facetCode,
                Position = 1,
                TextFilter = "",
                Picks = new List<FacetConfigPick> {
                },
                Facet = facet
            };

            var sqlJoins = new List<string>() { };
            var criterias = new List<string>() { };
            var routes = new List<GraphRoute>() { };

            QuerySetup query = new QuerySetup() {
                TargetConfig = targetFacetConfig,
                Facet = facet,
                Joins = sqlJoins,
                Criterias = criterias,
                Routes = routes
            };

            // Act
            var result = rangeOuterBoundSqlCompiler.Compile(
                query,
                facet
            );

            // Assert
            Assert.True(false);
        }
    }
}
