using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03.PrimAlgorithm
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

    }
    class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> forest;
        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraphData(e);
            forest = new HashSet<int>();
            foreach (var node in graph.Keys)
            {
                if (!forest.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        private static void Prim(int node)
        {
            forest.Add(node);
            var priorityQueue = new OrderedBag<Edge>
                (graph[node],
                Comparer<Edge>.Create((x, y) => x.Weight - y.Weight));
            while (priorityQueue.Count > 0)
            {
                var edge = priorityQueue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);
                
                if (nonTreeNode != -1)
                {
                    Console.WriteLine($"{edge.First} - {edge.Second}");
                    forest.Add(nonTreeNode);
                    priorityQueue.AddMany(graph[nonTreeNode]);
                }
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            int nonTreeNode = -1;
            if (forest.Contains(first)
                    && !forest.Contains(second))

            {
                nonTreeNode = second;
            }
            if (!forest.Contains(first)
               && forest.Contains(second))

            {
                nonTreeNode = first;
            }
            return nonTreeNode;
        }

        private static Dictionary<int, List<Edge>> ReadGraphData(int e)
        {
            var graph = new Dictionary<int, List<Edge>>();
            for (int i = 0; i < e; i++)
            {
                int[] input = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int first = input[0];
                int second = input[1];
                int weight = input[2];
                if (!graph.ContainsKey(first))
                {
                    graph.Add(first, new List<Edge>());
                }
                if (!graph.ContainsKey(second))
                {
                    graph.Add(second, new List<Edge>());
                }
                var edge = new Edge { First = first, Second = second, Weight = weight };
                graph[first].Add(edge);
                graph[second].Add(edge);
            }
            return graph;
        }
    }
}
