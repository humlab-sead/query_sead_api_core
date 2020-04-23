﻿using System;
using System.Collections.Generic;

namespace SQT.Models
{
    public partial class ResultAggregateField
    {
        public int AggregateFieldId { get; set; }
        public int AggregateId { get; set; }
        public int ResultFieldId { get; set; }
        public string FieldTypeId { get; set; }
        public int SequenceId { get; set; }

        public virtual ResultAggregate Aggregate { get; set; }
        public virtual ResultFieldType FieldType { get; set; }
        public virtual ResultField ResultField { get; set; }
    }
}
