using Autofac.Features.Indexed;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SeadQueryCore.QueryBuilder
{
    public interface IQuerySetupCompiler
    {
        IFacetsGraph Graph { get; set; }

        QuerySetup Build(FacetsConfig2 facetsConfig, Facet facet, List<string> extraTables = null);
        QuerySetup Build(FacetsConfig2 facetsConfig, Facet facet, List<string> extraTables, List<string> facetCodes);
    }

    public class QuerySetupCompiler : IQuerySetupCompiler {
        public IFacetsGraph Graph { get; set; }
        public IIndex<int, IPickFilterCompiler> PickCompilers { get; set; }
        public IEdgeSqlCompiler EdgeCompiler { get; }

        public QuerySetupCompiler(
            IFacetsGraph graph,
            IIndex<int, IPickFilterCompiler> pickCompilers,
            IEdgeSqlCompiler edgeCompiler
        ) {
            Graph = graph;
            PickCompilers = pickCompilers;
            EdgeCompiler = edgeCompiler;
        }

        public QuerySetup Build(FacetsConfig2 facetsConfig, Facet facet, List<string> extraTables = null)
        {
            List<string> facetCodes = facetsConfig.GetFacetCodes().AddIfMissing(facet.FacetCode);
            return Build(facetsConfig, facet, extraTables ?? new List<string>(), facetCodes);
        }

        public QuerySetup Build(FacetsConfig2 facetsConfig, Facet targetFacet, List<string> extraTables, List<string> facetCodes)
        {
            // Noteworthy: TargetFacet differs from facetsConfig.TargetFacet when a result-facet is applied

            Debug.Assert(facetsConfig.TargetFacet != null, "facetsConfig.TargetFacet is NULL ");

            if (facetsConfig == null)
                facetCodes = new List<string>();

            var affectedConfigs = facetsConfig.GetFacetConfigsAffectedBy(targetFacet, facetCodes);

            // Find all tables that are involved in the final query
            List<string> tables = GetInvolvedTables(targetFacet, extraTables, affectedConfigs);

            // Compute criteria clauses for user picks and store in Dictionary keyed by tablename
            var tableCriterias = CompilePickCriterias(targetFacet, affectedConfigs);

            // Find all routes from target facet's table to all tables collected in affected facets
            List<GraphRoute> routes = Graph.Find(targetFacet.ResolvedName, tables, true);

            // Compile list of joins for the reduced route
            List<string> joins = CompileJoins(tableCriterias, routes);

            // Add TargetFacets Query Criteria (if exists)
            var criterias = tableCriterias.Values
                .AppendIf(targetFacet.QueryCriteria).ToList();

            QuerySetup querySetup = new QuerySetup() {
                TargetConfig = facetsConfig?.TargetConfig,
                Facet = targetFacet,
                Routes = routes,
                Joins = joins,
                Criterias = criterias
            };

            return querySetup;
        }

        protected List<string> CompileJoins(Dictionary<string, string> pickCriterias, List<GraphRoute> reducedRoutes)
        {
            return reducedRoutes
                .SelectMany(route => route.Items)
                .Select(edge => EdgeCompiler.Compile(Graph, edge, HasUserPicks(edge, pickCriterias)))
                .ToList();
        }

        protected Dictionary<string, string> CompilePickCriterias(Facet targetFacet, List<FacetConfig2> affectedConfigs)
        {
            // Compute criteria clauses for user picks for each affected facet
            var criterias = affectedConfigs
                .Select(config => (
                    // FIXME: Add ObjectArgs to ResolvedName???
                    (
                        Tablename: config.Facet.ResolvedName,
                        Criteria: PickCompiler(config).Compile(targetFacet, config.Facet, config))
                    )
                 )
                .ToList();

            // Group and concatenate the criterias for each table
            var pickCriterias = criterias
                .GroupBy(p => p.Tablename, p => p.Criteria, (key, g) => new { TableName = key, Clauses = g.ToList() })
                .ToDictionary(z => z.TableName, z => $"({z.Clauses.Combine(" AND ")})");

            return pickCriterias;
        }

        /// <summary>
        /// Collect all affected tables (those defined in affected facets)
        /// </summary>
        /// <param name="targetFacet"></param>
        /// <param name="extraTables"></param>
        /// <param name="affectedConfigs"></param>
        /// <returns></returns>
        protected List<string> GetInvolvedTables(Facet targetFacet, List<string> extraTables, List<FacetConfig2> affectedConfigs)
        {
            var tables =

                // ...extra tables (if any)...
                (extraTables ?? new List<string>())

                // ...target facet's tables...
                .Concat(
                    targetFacet.ExtraTables.Select(z => z.ObjectName)
                )

                // ...tables from affected facets...
                .Concat(
                    // FIXME: Shouldn't all tables be added???
                    affectedConfigs.SelectMany(c => c.Facet.ExtraTables.Select(z => z.ObjectName).ToList())
                );

            return tables.Distinct().ToList();
        }

        protected bool HasUserPicks(GraphEdge edge, Dictionary<string, string> tableCriterias)
        {
            return tableCriterias.ContainsKey(edge.SourceName) || tableCriterias.ContainsKey(edge.TargetName);
        }

        protected IPickFilterCompiler PickCompiler(FacetConfig2 c)
        {
            return PickCompilers[(int)c.Facet.FacetTypeId];
        }
    }
}