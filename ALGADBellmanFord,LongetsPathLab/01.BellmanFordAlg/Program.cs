using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.BellmanFordAlg
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge> edges;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            edges = ReadInputData(n, e);
            var distances = Enumerable
                .Repeat(double.PositiveInfinity, n + 1)
                .ToArray();
            var prev = Enumerable
                .Repeat(-1, n + 1)
                .ToArray();

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());
            distances[source] = 0;

            for (int i = 0; i < n - 1; i++)
            {
                bool changedDistances = false;
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(distances[edge.From]))
                    {
                        continue;
                    }
                    var newDistance = distances[edge.From] + edge.Weight;
                    if (distances[edge.To] > newDistance)
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        changedDistances = true;
                    }
                }
                if (changedDistances == false)
                {
                    break;
                }
            }

            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(distances[edge.From]))
                {
                    continue;
                }
                var newDistance = distances[edge.From] + edge.Weight;
                if (distances[edge.To] > newDistance)
                {
                    distances[edge.To] = newDistance;
                    prev[edge.To] = edge.From;
                    Console.WriteLine("Negative Cycle Detected");
                    return;
                }
            }
            
            var path = ReconstructPath(prev, destination);
            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distances[destination]);
            
        }

        private static Stack<int> ReconstructPath(int[] prev, int node)
        {
            var stack = new Stack<int>();
            while (node != -1)
            {
                stack.Push(node);
                node = prev[node];
            }
            return stack;
        }

        private static List<Edge> ReadInputData(int n, int e)
        {
            var result = new List<Edge>();
            for (int i = 0; i < e; i++)
            {
                int[] input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int from = input[0];
                int to = input[1];
                int weight = input[2];
                result.Add(new Edge
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
