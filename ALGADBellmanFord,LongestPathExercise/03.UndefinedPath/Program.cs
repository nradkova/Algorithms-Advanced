using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.UndefinedPath
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }

    }
    class Program
    {
        private static List<Edge> map;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            map = ReadMap(e);
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
                bool changed = false;
                foreach (var edge in map)
                {
                    if (double.IsPositiveInfinity(distances[edge.From]))
                    {
                        continue;
                    }
                    var newDistance = edge.Weight + distances[edge.From];
                    if (distances[edge.To] > newDistance)
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        changed = true;
                    }
                }
                if (changed == false)
                {
                    break;
                }
            }
            foreach (var edge in map)
            {
                if (double.IsPositiveInfinity(distances[edge.From]))
                {
                    continue;
                }
                var newDistance = edge.Weight + distances[edge.From];
                if (distances[edge.To] > newDistance)
                {
                    distances[edge.To] = newDistance;
                    prev[edge.To] = edge.From;
                    Console.WriteLine("Undefined");
                    return;
                }
            }
            var path = GetPath(prev, destination);
            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distances[destination]);
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

        private static List<Edge> ReadMap(int e)
        {
            var result = new List<Edge>();
            for (int i = 0; i < e; i++)
            {
                int[] inputData = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();
                int from = inputData[0];
                int to = inputData[1];
                int cost = inputData[2];
                var edge = new Edge
                {
                    From = inputData[0],
                    To = inputData[1],
                    Weight = inputData[2]
                };
                result.Add(edge);
            }
            return result;
        }
    }
}
