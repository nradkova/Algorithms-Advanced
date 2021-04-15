using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.DijkstraAlgorithm
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static Dictionary<int, List<Edge>> graph;

        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraphData(e);
            var startNode = int.Parse(Console.ReadLine());
            var endNode = int.Parse(Console.ReadLine());
            int maxNode = graph.Keys.Max();
            var distances = new int[maxNode + 1];
            var prev = new int[maxNode + 1];

            distances = distances.Select(x => x = int.MaxValue).ToArray();
            distances[startNode] = 0;
            prev[startNode] = -1;

            var queue = new OrderedBag<int>
                (Comparer<int>.Create((x, y) => distances[x] - distances[y]));
            queue.Add(startNode);
            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                if (node == endNode)
                {
                    break;
                }
                var children = graph[node];
                foreach (var child in children)
                {
                    var childNode = child.First == node
                        ? child.Second
                        : child.First;
                    if (distances[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                    }
                    var newDistance = child.Weight + distances[node];
                    if (distances[childNode] > newDistance)
                    {
                        distances[childNode] = newDistance;
                        prev[childNode] = node;
                        queue = new OrderedBag<int>(queue, Comparer<int>
                            .Create((x, y) => distances[x] - distances[y]));
                    }
                }
            }
            if (distances[endNode] == int.MaxValue)
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {
                var path = new Stack<int>();
                path.Push(endNode);
                var current = prev[endNode];
                while (current > -1)
                {
                    path.Push(current);
                    current = prev[current];
                }

                Console.WriteLine(distances[endNode]);
                Console.WriteLine(string.Join(" ", path));
            }

        }

        private static Dictionary<int, List<Edge>> ReadGraphData(int e)
        {
            var graph = new Dictionary<int, List<Edge>>();
            for (int i = 0; i < e; i++)
            {
                int[] input = Console.ReadLine()
                    .Split(new [] { ", " },StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int first = input[0];
                int second = input[1];
                int weight = input[2];
                if (!graph.ContainsKey(first))
                {
                    graph.Add(first, new List<Edge>());
                }
                if (!graph.ContainsKey(second))
                {
                    graph.Add(second, new List<Edge>());
                }
                var edge = new Edge { First = first, Second = second, Weight = weight };
                graph[first].Add(edge);
                graph[second].Add(edge);
            }
            return graph;
        }
    }
}
