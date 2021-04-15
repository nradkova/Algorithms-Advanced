using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.VampireLabyrinth
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge>[] graph;
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
            graph = ReadGraph(n, m);
            var distances = Enumerable.Repeat(double.PositiveInfinity, n).ToArray();
            var prev = Enumerable.Repeat(-1, n).ToArray();
            FindPath(source, destination, distances, prev);
            var shortestPath = ReconstructPath(destination,prev);
            Console.WriteLine(string.Join(" ",shortestPath));
            Console.WriteLine(distances[destination]);

        }

        private static Stack<int> ReconstructPath(int destination, int[] prev)
        {
            var result = new Stack<int>();
            var node = destination;
            while (node!=-1)
            {
                result.Push(node);
                node = prev[node];
            }
            return result;
        }

        private static void FindPath(int source, int destination,
            double[] distances, int[] prev)
        {
            var queue = new OrderedBag<int>
                 (Comparer<int>.Create((f, s) => (int)distances[f] - (int)distances[s]));
            queue.Add(source);
            distances[source] = 0;
            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                if (node == destination)
                {
                    break;
                }
                foreach (var edge in graph[node])
                {
                    int child = edge.From == node
                        ? edge.To
                        : edge.From;
                    if (double.IsPositiveInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }
                    var newDistance = edge.Weight + distances[node];
                    if (newDistance < distances[child])
                    {
                        distances[child] = newDistance;
                        prev[child] = node;
                        queue = new OrderedBag<int>
                                (queue, Comparer<int>
                                .Create((f, s) => (int)distances[f] - (int)distances[s]));
                    }
                }
            }
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
                int from = line[0];
                int to = line[1];
                int weight = line[2];
                result[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
            return result;
        }
    }
}
