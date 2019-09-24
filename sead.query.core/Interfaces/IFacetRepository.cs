﻿using System.Collections.Generic;

namespace SeadQueryCore
{
    public interface IFacetRepository : IRepository<Facet, int>
    {
        IEnumerable<Facet> FindThoseWithAlias();
        // string GenerateStateId();
        Facet GetByCode(string facetCode);
        IEnumerable<Facet> GetOfType(EFacetType type);
        (decimal, decimal) GetUpperLowerBounds(Facet facet);
        Dictionary<string, Facet> ToDictionary();
    }


    public interface IFacetTypeRepository : IRepository<FacetType, int>
    {
    }

    public interface IFacetGroupRepository : IRepository<FacetGroup, int>
    {
    }

}