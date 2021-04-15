using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02.First
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
        static void Main(string[] args)
        {
            int n =int.Parse(Console.ReadLine());
            int e =int.Parse(Console.ReadLine());
            graph = ReadGraph(n, e);
            var shops = new List<Edge>();
            GetBestTime(0,shops);
            Console.WriteLine(shops.Sum(x=>x.Weight));
            
        }

        private static void GetBestTime(int node, List<Edge> shops)
        {
            var visited = new bool[graph.Length];
            visited[node] = true;
            var queue = new OrderedBag<Edge>
                (graph[node], Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            while (queue.Count>0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second, visited);
                if (nonTreeNode==-1)
                {
                    continue;
                }
                shops.Add(edge);
                queue.AddMany(graph[nonTreeNode]);
                visited[nonTreeNode] = true;
            }
        }

        private static int GetNonTreeNode(int first, int second, bool[] visited)
        {
            if (visited[first]&&!visited[second])
            {
                return second;
            }
            if (!visited[first] && visited[second])
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
                var tokens = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var edge = new Edge
                {
                    First = tokens[0],
                    Second = tokens[1],
                    Weight = tokens[2]
                };
                result[tokens[0]].Add(edge);
                result[tokens[1]].Add(edge);
            }
            return result;
        }
    }
}
