using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03
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
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraph(n, e);
            var cameras = ReadCameras();
            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());
            var distance = Enumerable.Repeat(int.MaxValue, n).ToArray();
            distance[source] = 0;
            var queue = new OrderedBag<int>
                (Comparer<int>.Create((f, s) => distance[f] - distance[s]));
            queue.Add(source);
            while (queue.Count>0)
            {
                var node = queue.RemoveFirst();
                if (node==destination)
                {
                    break;
                }
                foreach (var edge in graph[node])
                {
                    var child = node == edge.First
                        ? edge.Second
                        : edge.First;
                    if (cameras.Contains(child))
                    {
                        continue;
                    }
                    if (distance[child]==int.MaxValue)
                    {
                        queue.Add(child);
                    }
                    var newDistance = edge.Weight + distance[node];
                    if (newDistance<distance[child])
                    {
                        distance[child] = newDistance;
                        queue = new OrderedBag<int>
                            (queue,
                            Comparer<int>.Create((f, s) => 
                            distance[f] - distance[s]));
                    }
                }
            }
            Console.WriteLine(distance[destination]);
        }       

        private static HashSet<int> ReadCameras()
        {
            var result = new HashSet<int>();
            var line = Console.ReadLine()
                .Split();
            for (int i = 0; i < line.Length; i++)
            {
                var token = line[i];

                if (token[token.Length-1]=='w')
                {
                    var cameraStr = string.Empty;
                    for (int j = 0; j < token.Length-1; j++)
                    {
                        cameraStr += token[j].ToString();
                    }
                    result.Add(int.Parse(cameraStr));
                }
            }
            return result;
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
                var first = line[0];
                var second = line[1];
                var weight = line[2];
                var edge = new Edge
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
