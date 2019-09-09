﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeadQueryCore
{

    public class GraphRoute
    {
        public List<GraphTableRelation> Items { get; set; } = new List<GraphTableRelation>();

        public GraphRoute(List<GraphTableRelation> items)
        {
            Items = items;
        }

        public bool Contains(GraphTableRelation item)
        {
            return Items.Any(x => x.SourceTable.NodeId == item.SourceTable.NodeId && x.TargetTable.NodeId == item.TargetTable.NodeId);
        }

        public GraphRoute ReduceBy(List<GraphRoute> routes)
        {
            return new GraphRoute(Items.Where(z => !Utility.ExistsAny(routes, z)).ToList());
        }

        public override string ToString()
        {
            return String.Join("\n", Items.Select(z => $"{z.SourceTableName};{z.TargetTableName};{z.Weight}"));
        }

        public static class Utility
        {
            public static bool ExistsAny(List<GraphRoute> routes, GraphTableRelation item)
            {
                return routes.Any(x => x.Contains(item));
            }

            public static List<GraphRoute> Reduce(List<GraphRoute> routes)
            {
                List<GraphRoute> reduced_routes = new List<GraphRoute>();
                foreach (var route in routes)
                {
                    GraphRoute reduced_route = route.ReduceBy(reduced_routes);
                    if (reduced_route.Items.Count > 0)
                    {
                        reduced_routes.Add(reduced_route);
                    }
                }
                return reduced_routes;
            }

            public static string ToString(List<GraphRoute> routes)
            {
                return String.Join("\n", routes.Select(z => $"{routes.IndexOf(z)};{z.ToString()}"));
            }
        }
    }
}
