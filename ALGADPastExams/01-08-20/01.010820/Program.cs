using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._010820
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge>[] edges;
        private static double[] distance;
        private static int[] prev;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int source = input[0];
            int destination = input[1];
            edges = ReaEdges(n, m);

            distance = Enumerable.Repeat(double.PositiveInfinity, n).ToArray();
            prev = Enumerable.Repeat(-1, n).ToArray();
            distance[source] = 0;
            var sortedNodes = GetTopologicallySorted();
            while (sortedNodes.Count>0)
            {
                var node = sortedNodes.Pop();
                foreach (var edge in edges[node])
                {
                    var child = edge.First == node
                    ? edge.Second
                    : edge.First;
                    var newDistance = edge.Weight + distance[node];
                    if (distance[child]>newDistance)
                    {
                        distance[child] = newDistance;
                        prev[child] = node;
                    }
                }
            }
            var path = ReconstructPath(source,destination);
            Console.WriteLine(string.Join(" ",path));
            Console.WriteLine(distance[destination]);
        }

        private static Stack<int> ReconstructPath(int source, int destination)
        {
            var result = new Stack<int>();
            var node = destination;
            while(node!=-1)
            {
                result.Push(node);
                node = prev[node];
            }
            return result;
        }

        private static Stack<int> GetTopologicallySorted()
        {
            var result = new Stack<int>();
            var visited = new bool[edges.Length];
            for (int node = 0; node < edges.Length; node++)
            {
                DFS(node, visited, result);
            }
            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }
            visited[node] = true;
            foreach (var edge in edges[node])
            {
                var child = edge.First == node
                    ? edge.Second
                    : edge.First;
                DFS(child, visited, result);
            }
            result.Push(node);
        }

        private static List<Edge>[] ReaEdges(int n, int m)
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
