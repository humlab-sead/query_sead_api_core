﻿using SeadQueryCore;
using System.Collections.Generic;
using System.Linq;
using SeadQueryInfra;

namespace SeadQueryTest.Fixtures
{
    public static class ScaffoldFacetConfig
    {
        public static FacetConfig2 Create(Facet facet, int position, List<FacetConfigPick> picks = null, string filter = "")
        {
            return new FacetConfig2(facet, position, filter, picks ?? new List<FacetConfigPick>());
        }

        public static FacetConfig2 Create(Facet facet, int position, List<int> ids, string filter = "")
        {
            return new FacetConfig2(facet, position, filter, FacetConfigPick.CreateDiscrete(ids));
        }

        public static FacetConfig2 Create(Facet facet, int position, decimal lower, decimal upper, string filter = "")
        {
            return new FacetConfig2(facet, position, filter, FacetConfigPick.CreateLowerUpper(lower, upper));
        }
    }
}
