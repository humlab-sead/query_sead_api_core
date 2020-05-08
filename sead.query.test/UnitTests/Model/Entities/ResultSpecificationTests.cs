using Moq;
using SeadQueryCore;
using SQT.Infrastructure;
using SQT.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SQT.Model
{
    public class ResultSpecificationTests
    {
        private ResultSpecification CreateResultSpecification()
        {
            return new ResultSpecification() {
                SpecificationId = 3,
                SpecificationKey = "sample_group_level",
                Fields = new List<ResultSpecificationField>() {
                    new ResultSpecificationField() {
                        SpecificationFieldId = 1,
                        FieldTypeId = "count_item",
                        SequenceId = 1,
                        ResultField = new ResultField() {
                            ResultFieldKey = "xxx",
                            FieldTypeId = "single_item"
                        },
                        FieldType = new ResultFieldType() {
                            IsResultValue = true
                        }
                    },

                    new ResultSpecificationField() {
                        SpecificationFieldId = 2,
                        FieldTypeId = "count_item",
                        SequenceId = 2,
                        ResultField = new ResultField() {
                            ResultFieldKey = "yyy",
                            FieldTypeId = "single_item"
                        },
                        FieldType = new ResultFieldType() {
                            IsResultValue = false
                        }
                    }
                }
            };
        }

        //private ResultSpecification CreateMockedResultSpecification()
        //{
        //    var field1 = new Mock<ResultSpecificationField>();
        //    field1.Setup(o => o.FieldType.IsResultValue).Returns(false);
        //    field1.Setup(o => o.SpecificationFieldId).Returns(1);

        //    var field2 = new Mock<ResultSpecificationField>();
        //    field2.Setup(o => o.FieldType.IsResultValue).Returns(true);
        //    field1.Setup(o => o.SpecificationFieldId).Returns(2);

        //    var fields = (new List<ResultSpecificationField>() { field1.Object, field2.Object });
        //    var mockResultSpecification = new Mock<ResultSpecification>();
        //    mockResultSpecification.Setup(x => x.Fields).Returns<ICollection<ResultSpecificationField>>(_ => fields);

        //    return mockResultSpecification.Object;
        //}

        [Fact]
        public void GetFields_WithTwoFieldsField_ReturnsFields()
        {
            // Arrange
            var item = CreateResultSpecification();

            // Act
            var result = item.GetSortedFields();

            // Assert
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public void GetResultFields_WithTwoFields_ReturnsTwoResultFields()
        {
            // Arrange
            var item = CreateResultSpecification();

            // Act
            var result = item.GetResultFields();

            // Assert
            Assert.Single(result);
        }
    }
}
