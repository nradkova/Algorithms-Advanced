using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02.ChainLightning
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

    }
    class Program
    {
        private static List<Edge>[] graph;
        private static int[] damages;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            int lightningsCount = int.Parse(Console.ReadLine());
            graph = ReadGraph(n, e);
            damages = new int[n];
            for (int i = 0; i < lightningsCount; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var startNode = line[0];
                var strike = line[1];
                var strikedNodes = GetStriked(startNode);
                foreach (var node in strikedNodes)
                {
                    int depth = node.Value;
                    int currentStrike = strike;
                    while (depth>1)
                    {
                        currentStrike = currentStrike / 2;
                        depth--;
                    }
                    damages[node.Key] += currentStrike;
                }
            }
            Console.WriteLine(damages.Max());
        }

        private static Dictionary<int, int> GetStriked(int startNode)
        {
            var striked = new Dictionary<int, int>();
            striked.Add(startNode, 1);

            var queue = new OrderedBag<Edge>(graph[startNode],
            Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second, striked);
                if (nonTreeNode != -1)
                {
                    var treeNode = -1;
                    if (nonTreeNode == edge.First)
                    {
                        treeNode = edge.Second;
                    }
                    else
                    {
                        treeNode = edge.First;
                    }
                    int depth = striked[treeNode];
                    striked.Add(nonTreeNode, depth + 1);
                    queue.AddMany(graph[nonTreeNode]);
                    queue = new OrderedBag<Edge>(queue,
                           Comparer<Edge>
                           .Create((f, s) => f.Weight - s.Weight));
                }
            }
            return striked;
        }

        private static int GetNonTreeNode(int first, int second,
            Dictionary<int, int> striked)
        {
            if (striked.ContainsKey(first)
                && !striked.ContainsKey(second))
            {
                return second;
            }
            if (!striked.ContainsKey(first)
                && striked.ContainsKey(second))
            {
                return first;
            }
            return -1;
        }

        private static List<Edge>[] ReadGraph(int n, int e)
        {
            var result = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = new List<Edge>();
            }
            for (int i = 0; i < e; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var edge = new Edge
                {
                    First = line[0],
                    Second = line[1],
                    Weight = line[2]
                };
                result[line[0]].Add(edge);
                result[line[1]].Add(edge);
            }
            return result;
        }
    }
}
