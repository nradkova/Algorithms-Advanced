using System;
using System.Collections.Generic;
using System.Linq;


namespace _02.CheapTownTour
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Cost { get; set; }

    }
    class Program
    {
        private static List<Edge> map;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            map = ReadMap(e);
            var costs = Enumerable
                .Repeat(double.PositiveInfinity, n).ToArray();

            var nodes = new HashSet<int>(map.Select(x => x.From)
                .Union(map.Select(x => x.To)));
            var parents = new int[n];
            foreach (var node in nodes)
            {
                parents[node] = node;
            }
            var sortedEdges = map.OrderBy(x => x.Cost).ToList();
            var totalCost = 0;
            foreach (var edge in sortedEdges)
            {
                var firstRoot = GetRoot(parents, edge.From);
                var secondRoot = GetRoot(parents, edge.To);
                if (firstRoot != secondRoot)
                {
                    totalCost += edge.Cost;
                    parents[firstRoot] = secondRoot;
                }
            }

            Console.WriteLine($"Total cost: {totalCost} ");
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }
            return node;
        }

        private static List<Edge> ReadMap(int e)
        {
            var result = new List<Edge>();
            for (int i = 0; i < e; i++)
            {
                int[] inputData = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();
                int from = inputData[0];
                int to = inputData[1];
                int cost = inputData[2];
                var edge = new Edge
                {
                    From = inputData[0],
                    To = inputData[1],
                    Cost = inputData[2]
                };
                result.Add(edge);
            }
            return result;
        }
    }
}
