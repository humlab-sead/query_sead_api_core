using Moq;
using SeadQueryCore;
using SeadQueryCore.QueryBuilder;
using SQT.Infrastructure;
using SQT.SQL.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SQT.QueryBuilder.ResultCompilers
{

    [Collection("JsonSeededFacetContext")]
    public class TabularResultSqlCompilerTests : DisposableFacetContextContainer
    {
        public TabularResultSqlCompilerTests(JsonSeededFacetContextFixture fixture) : base(fixture)
        {
        }

        [Theory]
        //[InlineData("sites:sites")]
        //[InlineData("sites:country@10/sites")]
        //[InlineData("palaeoentomology://rdb_systems:rdb_systems", "tbl_rdb_systems", "tbl_analysis_entities", "tbl_datasets")]
        //[InlineData("palaeoentomology://rdb_codes:rdb_codes", "tbl_rdb_codes", "tbl_analysis_entities", "tbl_datasets")]
        //[InlineData("palaeoentomology://sample_group_sampling_contexts:sample_group_sampling_contexts", "tbl_sample_group_sampling_contexts", "tbl_sample_groups", "tbl_analysis_entities", "tbl_datasets")]
        [InlineData("palaeoentomology://data_types:data_types", "tbl_data_types", "tbl_analysis_entities", "tbl_datasets")]
        //[InlineData("archaeobotany://ecocode_system:ecocode_system", "tbl_ecocode_systems", "tbl_ecocode_systems", "tbl_analysis_entities", "tbl_datasets")]
        //[InlineData("archaeobotany://ecocode:ecocode", "tbl_ecocode_definitions", "tbl_ecocode_definitions", "tbl_analysis_entities", "tbl_datasets")]
        public void Compile_StateUnderTest_ExpectedBehavior(string uri, params string[] expectedJoinTables)
        {
            // Arrange
            var fakeFacetsConfig = FakeFacetsConfig(uri);
            var fakeQuerySetup = FakeQuerySetup(fakeFacetsConfig, "result_facet");
            var facet = fakeQuerySetup.Facet;
            var fields = FakeResultAggregateFields("site_level", "tabular");
            // Act
            var compiler = new TabularResultSqlCompiler();
            var result = compiler.Compile(fakeQuerySetup, facet, fields);

            var match = new TabularResultSqlCompilerMatcher().Match(result);

            // Assert
            Assert.True(match.Success);
            Assert.True(match.InnerSelect.Success);

            Assert.NotEmpty(match.InnerSelect.Tables);
            Assert.True(expectedJoinTables.All(x => match.InnerSelect.Tables.Contains(x)));
        }

    }
}
