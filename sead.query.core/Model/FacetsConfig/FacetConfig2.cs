﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SeadQueryCore
{
    public class FacetConfig2 {

        public string FacetCode { get; set; } = "";
        public int Position { get; set; } = 0;
        public string TextFilter { get; set; } = "";

        public List<FacetConfigPick> Picks { get; set; }

        [JsonIgnore]
        public Facet Facet { get; set; }

        [JsonConstructor]
        public FacetConfig2()
        {
        }

        public FacetConfig2(Facet facet, int position, string filter, List<FacetConfigPick> picks)
        {
            FacetCode = facet.FacetCode;
            Facet = facet;
            Position = position;
            TextFilter = filter;
            Picks = picks ?? new List<FacetConfigPick>();
        }

        public bool HasPicks() => (Picks?.Count ?? 0) > 0;
        public bool HasCriterias() => (Facet?.Clauses?.Count ?? 0) > 0;
        public bool HasConstraints() => (HasPicks() || HasCriterias());

        public void ClearPicks() => Picks.Clear();

        public List<decimal> GetPickValues(bool sort = false)
        {
            List<decimal> values = Picks.Select(x => x.ToDecimal()).ToList();
            if (sort)
                values.Sort();
            return values;
        }

        public List<string> GetJoinTables()
        {
            var tables = Facet.Tables.Select(z => z.ResolvedAliasOrTableOrUdfName).ToList();
            return tables;
        }

        public FacetTable GetFacetTable(string name)
        {
            return Facet.Tables.FirstOrDefault(z => z.TableOrUdfName == name || z.ResolvedAliasOrTableOrUdfName == name);
        }

        public bool IsPriorTo(List<string> facetCodes, Facet targetFacet)
        {
            if (!HasConstraints()) {
                // FIXME Is this a relevant condition?
                return false;
            }

            if (targetFacet.FacetCode == FacetCode)
                return targetFacet.FacetType.ReloadAsTarget;

            return facetCodes.IndexOf(targetFacet.FacetCode) > facetCodes.IndexOf(FacetCode);

        }

    }
}
