using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.KruskalAlgorithm
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

    }
    class Program
    {
        private static List<Edge> edges;
        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            edges = ReadEdges(e);
            var sortedEdges = edges.OrderBy(x => x.Weight).ToList();
            var nodes = new HashSet<int>(edges.Select(x => x.First)
                .Union(edges.Select(x => x.Second)));
            var maxNode = nodes.Max();
            var parents = Enumerable.Repeat(-1, maxNode + 1).ToArray();
            foreach (var node in nodes)
            {
                parents[node] = node;
            }
            foreach (var edge in sortedEdges)
            {
                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot != secondNodeRoot)
                {
                    Console.WriteLine($"{edge.First} - {edge.Second}");
                }
                parents[firstNodeRoot] = secondNodeRoot;
            }
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }
            return node;
        }

        private static List<Edge> ReadEdges(int e)
        {
            var result = new List<Edge>();
            for (int i = 0; i < e; i++)
            {
                int[] input = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int fisrt = input[0];
                int second = input[1];
                int weight = input[2];
                result.Add(new Edge
                {
                    First = fisrt,
                    Second = second,
                    Weight = weight
                });
            }
            return result;
        }
    }
}
