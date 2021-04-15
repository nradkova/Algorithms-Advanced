using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.LeTourDeSofia
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
        private static int[] distances;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            int startNode = int.Parse(Console.ReadLine());
            graph = ReadGraph(n, e);

            distances = Enumerable.Repeat(int.MaxValue, n).ToArray();
            distances[startNode] = 0;

            var result = FindPathValue(startNode);
            Console.WriteLine(result);
        }

        private static int FindPathValue(int startNode)
        {
            var queue = new OrderedBag<int>
                 (Comparer<int>.Create((f, s) => distances[f] - distances[s]));
            queue.Add(startNode);
            int childCount = 1;
            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                var children = graph[node];
                foreach (var child in children)
                {
                    var childNode = child.From == node
                        ? child.To
                        : child.From;
                    if (distances[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                        childCount++;
                    }
                    var newDistance = child.Weight + distances[node];
                    if (childNode == startNode)
                    {
                        return newDistance;
                    }
                    if (distances[childNode] > newDistance)
                    {
                        distances[childNode] = newDistance;
                        queue = new OrderedBag<int>(queue,
                            Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }
            }
            
            return childCount;
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
                var from = line[0];
                var to = line[1];
                var weight = line[2];
                result[from].Add(new Edge { From = from, To = to, Weight = weight });
            }
            return result;
        }
    }
}
