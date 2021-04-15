using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02.Picker
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
        private static HashSet<int> forest;
        static void Main(string[] args)
        {
            int n =int.Parse(Console.ReadLine());
            int m =int.Parse(Console.ReadLine());
            graph = ReadGraph(n, m);
            forest = new HashSet<int>();
            var included = new OrderedBag<Edge>
                (Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            int totalWeight = 0;
            for (int i = 0; i < n; i++)
            {
                if (!forest.Contains(i))
                {
                   totalWeight+= Prim(i,included);
                }
            }
            foreach (var edge in included)
            {
                Console.WriteLine($"{edge.First} {edge.Second}");
            }
            Console.WriteLine(totalWeight);
        }

        private static int Prim(int node, OrderedBag<Edge> included)
        {
            int totalWeight = 0;
            forest.Add(node);
            var queue = new OrderedBag<Edge>
                (graph[node],
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            while (queue.Count>0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);
                if (nonTreeNode!=-1)
                {
                    totalWeight += edge.Weight;
                    included.Add(edge);
                    forest.Add(nonTreeNode);
                    queue.AddMany(graph[nonTreeNode]);
                }
            }
            return totalWeight;
        }

        private static int GetNonTreeNode(int first, int second)
        {
            if (forest.Contains(first)
                &&!forest.Contains(second))
            {
                return second;
            }
            if (!forest.Contains(first)
               && forest.Contains(second))
            {
                return first;
            }
            return -1;
        }

        private static List<Edge>[] ReadGraph(int n, int m)
        {
            var result = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = new List<Edge>();
            }
            for (int i = 0; i < m; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int first = line[0];
                int second = line[1];
                int weight = line[2];
               var edge=new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                result[first].Add(edge);
                result[second].Add(edge);
            }
            return result;
        }
    }
}
