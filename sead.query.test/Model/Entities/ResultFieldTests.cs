using Moq;
using SeadQueryCore;
using SeadQueryTest.Infrastructure;
using SeadQueryTest.Infrastructure.Scaffolding;
using System;
using System.Collections.Generic;
using Xunit;

namespace SeadQueryTest.Model.Entities
{
    public class ResultFieldTests : FacetTestBase
    {
        // Has no logic

        public static List<object[]> TestData = new List<object[]>() {
            new object[] {
                typeof(ResultField),
                5,
                new Dictionary<string, object>() {
                    { "ResultFieldId", 5 },
                    { "ResultFieldKey", "site_link_filtered" },
                    { "TableName", "tbl_sites" },
                    { "ColumnName", "tbl_sites.site_id" },
                    { "DisplayText", "Filtered report" },
                    { "FieldTypeId", "link_item" },
                    { "Activated", true },
                    { "LinkUrl", "api/report/show_site_details.php?site_id" },
                    { "LinkLabel", "Show filtered report" }
                }
            }
        };

        // FIXME: SUT is FacetContext - not ResultField!
        [Theory]
        [MemberData(nameof(TestData))]
        public void Find_FromRepository_IsComplete(Type type, object id, Dictionary<string, object> expected)
        {
            // Arrange
            using (var context = ScaffoldUtility.DefaultFacetContext()) {
                var mockRegistry = new Mock<IRepositoryRegistry>();
                mockRegistry.Setup(x => x.Results.GetAllFields())
                    .Returns(ScaffoldUtility.LoadJSON<ResultField>());
                // Act
                var entity = context.Find(type, new object[] { id });
                // Assert
                Assert.NotNull(entity);
                Asserter.EqualByDictionary(type, expected, entity);
            }
        }
    }
}
