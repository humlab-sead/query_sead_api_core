﻿using DataAccessPostgreSqlProvider;
using SeadQueryCore;
using SeadQueryInfra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SeadQueryTest.Infrastructure.Scaffolding
{
    public static class ScaffoldUtility
    {
        public static string GetRootFolder()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var parts = new List<string>(path.Split(Path.DirectorySeparatorChar));
            var pos = parts.FindLastIndex(x => string.Equals("bin", x));
            string root = String.Join(Path.DirectorySeparatorChar.ToString(), parts.GetRange(0, pos));
            return root;
        }

        public static ICollection<Type> GetModelTypes()
        {
            return new List<Type>() {
                    typeof(Facet),
                    typeof(FacetClause),
                    typeof(FacetGroup),
                    typeof(FacetTable),
                    typeof(FacetType),
                    typeof(GraphNode),
                    typeof(GraphEdge),
                    typeof(ResultAggregate),
                    typeof(ResultAggregateField),
                    typeof(ResultField),
                    typeof(ResultFieldType),
                    typeof(ResultViewType),
                    typeof(ViewState)
                };
        }

        public static List<T> LoadJSON<T>(int count=0)
        {
            var folder = Path.Combine(GetRootFolder(), "Infrastructure", "Data");
            var reader = new ScaffoldReader();
            List<T> entities = new List<T>(reader.Deserialize<T>(folder));
            return entities;
        }
        public static T LoadSingleJSON<T>(int index=0)
        {
            var folder = Path.Combine(GetRootFolder(), "Infrastructure", "Data");
            var reader = new ScaffoldReader();
            List<T> entities = new List<T>(reader.Deserialize<T>(folder));
            return LoadJSON<T>()[index];
        }

        public static FacetContext DefaultFacetContext()
        {
            var folder = Path.Combine(GetRootFolder(), "Infrastructure", "Data");
            var seeder = new FacetContextFixtureSeededByFolder(folder);
            return seeder.FacetContext;
        }

        public static IFacetsGraph CreateFacetsGraphByFakeContext(FacetContext testContext)
        {
            var registry = new RepositoryRegistry(testContext);
            var factory = new FacetGraphFactory();
            List<GraphNode> nodes = registry.Nodes.GetAll().ToList();
            List<GraphEdge> edges = registry.Edges.GetAll().ToList();
            List<Facet> facets = registry.Facets.FindThoseWithAlias().ToList();
            var g = factory.Build(
                nodes,
                edges,
                facets
            );
            return g;
        }
    }
}
