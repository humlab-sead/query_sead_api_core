﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace QuerySeadDomain
{

    public interface IFacetsGraph {
        IEnumerable<FacetDefinition> AliasFacets { get; }
        Dictionary<Tuple<string, string>, GraphEdge> Edges { get; }
        Dictionary<int, GraphNode> NodeIds { get; }
        Dictionary<string, GraphNode> Nodes { get; }
        Dictionary<int, Dictionary<int, int>> Weights { get; }
        GraphEdge GetEdge(int sourceId, int targetId);
        GraphEdge GetEdge(string source, string target);
        string ResolveName(string tableName);
    }

    public class FacetGraphFactory {


        public static IFacetsGraph instance = null;

        public static IFacetsGraph Create(IUnitOfWork context)
        {
            return instance ?? (instance = new FacetGraphFactory().Build(context));
        }

        public IFacetsGraph Build(IUnitOfWork context)
        {
            var edgesList = context.Edges.GetAll().ToList();

            var nodes = context.Nodes.GetAll().ToDictionary(x => x.TableName);

            var aliasFacets = context.Facets.FindThoseWithAlias().ToList();
            var aliasTables = aliasFacets.ToDictionary(x => x.TargetTableName, x => x.AliasName);

            AddAliasNodes(nodes, aliasFacets);

            var weights = edgesList.GroupBy(p => p.SourceTableId,
                (key, g) => (key, g.ToDictionary(x => x.TargetTableId, x => x.Weight)))
                .ToDictionary(x => x.Item1, y => y.Item2);

            var edges = edgesList.ToDictionary(z => z.Key);
            AddAliasEdges(edges, nodes, aliasFacets);
            AddReverseEdges(edges);

            return new FacetsGraph() {
                Nodes = nodes,
                Edges = edges,
                AliasFacets = aliasFacets,
                AliasTables = aliasTables,
                NodeIds = nodes.Values.ToDictionary(x => x.TableId),
                Weights = weights
            };
        }

        private void AddAliasNodes(Dictionary<string, GraphNode> nodes, IEnumerable<FacetDefinition> aliasFacets)
        {
            int id = nodes.Max(z => z.Value.TableId) + 1;
            aliasFacets.Select(z => z.AliasName)
                .ForEach(z => nodes[z] = new GraphNode() { TableId = id++, TableName = z });
        }

        private void AddAliasEdges(Dictionary<Tuple<string, string>, GraphEdge> edges, Dictionary<string, GraphNode> nodes, IEnumerable<FacetDefinition> aliasFacets)
        {
            foreach (var facet in aliasFacets) {
                var values = edges.Where(x => x.Key.Item1 == facet.TargetTableName)
                     .Select(z => z.Value.makeAlias(nodes[facet.AliasName])).ToList();
                     values.ForEach(z => edges[z.Key] = z);
            }
        }

        private void AddReverseEdges(Dictionary<Tuple<string, string>, GraphEdge> edges)
        {
            var values = edges.Values.Select(x => x.Reverse()).ToList();
            values.ForEach(x => edges[x.Key] = x);
        }

    }

    public class FacetsGraph : IFacetsGraph {

        public Dictionary<string, GraphNode> Nodes { get; set; }
        public Dictionary<Tuple<string, string>, GraphEdge> Edges { get; set; }
        public IEnumerable<FacetDefinition> AliasFacets { get; set; }
        public Dictionary<string, string> AliasTables;
        public Dictionary<int, GraphNode> NodeIds { get; set; }
        public Dictionary<int, Dictionary<int, int>> Weights { get; set; }

        public GraphEdge GetEdge(string source, string target)
            => Edges[Tuple.Create(source, target)];

        public GraphEdge GetEdge(int sourceId, int targetId)
            => Edges[Tuple.Create(NodeIds[sourceId].TableName, NodeIds[targetId].TableName)];

        public string ResolveName(string tableName) =>
            AliasTables.ContainsKey(tableName) ? AliasTables[tableName] : tableName;

    }
}