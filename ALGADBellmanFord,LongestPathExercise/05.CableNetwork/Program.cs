using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _05.CableNetwork
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }
    class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> forest;
        private static int leftBudget;

        static void Main(string[] args)
        {
            int budget = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            forest = new HashSet<int>();
            graph = new Dictionary<int, List<Edge>>();
            ReadInputData(n, e, graph, forest);
            leftBudget = budget;

            Prim();

            Console.WriteLine($"Budget used: {budget - leftBudget}");
        }

        private static void Prim()
        {
            var queue = new OrderedBag<Edge>
               (Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            foreach (var node in forest)
            {
                queue.AddMany(graph[node]);
            }
            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                if (leftBudget - edge.Weight < 0)
                {
                    break;
                }
               
                var nonTreeNode = FindNonTreeNode
                    (edge.First, edge.Second);
                if (nonTreeNode != -1)
                {
                    forest.Add(nonTreeNode);
                    leftBudget -= edge.Weight;
                    queue.AddMany(graph[nonTreeNode]);
                }
            }
        }

        private static int FindNonTreeNode(int first, int second)
        {
            int nonTreeNode = -1;
            if (!forest.Contains(second)
                && forest.Contains(first))
            {
                nonTreeNode = second;
            }
            if (forest.Contains(second)
               && !forest.Contains(first))
            {

                nonTreeNode = first;
            }
            return nonTreeNode;
        }

        private static void ReadInputData(int n, int e,
            Dictionary<int, List<Edge>> graph,
            HashSet<int> forest)
        {
            for (int i = 0; i < e; i++)
            {
                var input = Console.ReadLine()
                    .Split();
                int first = int.Parse(input[0]);
                int second = int.Parse(input[1]);
                int weight = int.Parse(input[2]);
                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                if (input.Length == 4)
                {
                    forest.Add(first);
                    forest.Add(second);
                }
                if (input.Length == 3)
                {
                    if (!graph.ContainsKey(first))
                    {
                        graph.Add(first, new List<Edge>());
                    }
                    if (!graph.ContainsKey(second))
                    {
                        graph.Add(second, new List<Edge>());
                    }
                    graph[first].Add(edge);
                    graph[second].Add(edge);
                }
            }
        }
    }
}
