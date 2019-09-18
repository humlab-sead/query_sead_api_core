using Moq;
using SeadQueryCore;
using SeadQueryTest.Infrastructure;
using SeadQueryTest.Infrastructure.Scaffolding;
using System;
using System.Collections.Generic;
using Xunit;

namespace SeadQueryTest.Model.Entities
{
    public class ResultAggregateFieldTests : FacetTestBase
    {
        // Class has no logic...

        public static List<object[]> TestData = new List<object[]>() {
             new object[] {
                typeof(ResultAggregateField),
                13,
                new Dictionary<string, object>() {
                    { "AggregateFieldId", 13 },
                    { "AggregateId", 1 },
                    { "ResultFieldId", 1 },
                    { "FieldTypeId", "sort_item" },
                    { "SequenceId", 99 }
                }
            }
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Find_FromRepository_IsComplete(Type type, object id, Dictionary<string, object> expected)
        {
            // Arrange
            using (var context = ScaffoldUtility.DefaultFacetContext()) {
                // Act
                var entity = context.Find(type, new object[] { id });
                // Assert
                Assert.NotNull(entity);
                Asserter.EqualByDictionary(type, expected, entity);
            }
        }
    }
}
