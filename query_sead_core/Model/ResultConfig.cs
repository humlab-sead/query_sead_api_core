using Newtonsoft.Json;
using QuerySeadDomain.QueryBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace QuerySeadDomain {

    public class ResultViewType
    {
        public string ViewTypeId { get; set; }
        public string ViewName { get; set; }
        public bool IsCachable { get; set; }
    }

    public class ResultConfig
    {
        public string RequestId { get; set; }
        public string SessionId { get; set; }
        public string ViewTypeId { get; set; }
        public List<string> AggregateKeys { get; set; } = new List<string>();

        public ResultConfig()
        {
        }

        public string GetCacheId(FacetsConfig2 facetsConfig)
        {
            return  $"{ViewTypeId}_{facetsConfig.GetPicksCacheId()}_{String.Join("", AggregateKeys)}_{facetsConfig.Language}";
        }

        [JsonIgnore] public bool IsEmpty => (AggregateKeys?.Count ?? 0) == 0;
    }
}
