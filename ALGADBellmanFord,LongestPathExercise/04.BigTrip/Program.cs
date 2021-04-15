using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.BigTrip
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
        public override string ToString()
        {
            return $"{this.From} {this.To} {this.Weight}";
        }
    }
    class Program
    {
        private static List<Edge>[] graph;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraph(n, e);
            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());
            var distances = Enumerable
                .Repeat(double.NegativeInfinity, n + 1)
                .ToArray();
            var prev = Enumerable
                .Repeat(-1, n+1)
                .ToArray();
            distances[source] = 0;
            var sortedNodes = GetTopologicalSorted();
            while (sortedNodes.Count>0)
            {
                var node = sortedNodes.Pop();
                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;
                    if (newDistance>distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = node;
                    }
                }
            }
            var path = GetPath(prev, destination);
        
            Console.WriteLine(distances[destination]);
            Console.WriteLine(string.Join(" ", path));
        }

        private static Stack<int> GetTopologicalSorted()
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                DFS(i, visited, result);
            }

            return result;
        }

        private static void DFS(int node, bool[] visited,
            Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }
            visited[node] = true;
            foreach (var edge in graph[node])
            {
                var child = edge.To;
                DFS(child, visited, result);
            }
            result.Push(node);
        }

        private static Stack<int> GetPath(int[] prev, int node)
        {
            var result = new Stack<int>();
            while (node != -1)
            {
                result.Push(node);
                node = prev[node];
            }
            return result;
        }

        private static List<Edge>[] ReadGraph(int n, int e)
        {
            var result = new List<Edge>[n + 1];
            for (int i = 0; i < n + 1; i++)
            {
                result[i] = new List<Edge>();
            }
            for (int i = 0; i < e; i++)
            {
                int[] input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int first = input[0];
                int second = input[1];
                int weight = input[2];
                var edge = new Edge
                {
                    From = first,
                    To = second,
                    Weight = weight
                };
                result[first].Add(edge);
            }
            return result;
        }
    }
}

