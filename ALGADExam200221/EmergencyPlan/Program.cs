using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace EmergencyPlan
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Time { get; set; }
    }
    class Program
    {
        private static List<Edge>[] graph;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var exits = ReadExits();
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraph(n, e, exits);
            var timeLimit = ReadTime(Console.ReadLine());
            var bestDistance = Enumerable.Repeat(int.MaxValue, graph.Length).ToArray();
            for (int node = 0; node < graph.Length; node++)
            {
                if (!exits.Contains(node))
                {
                    foreach (var exit in exits)
                    {
                        var currentDistance = GetDistance(node, exit);
                        if (currentDistance < bestDistance[node])
                        {
                            bestDistance[node] = currentDistance;
                        }
                    }
                }
            }
            for (int room = 0; room < graph.Length; room++)
            {
                if (!exits.Contains(room))
                {
                    if (bestDistance[room]==int.MaxValue)
                    {
                        Console.WriteLine($"Unreachable {room} (N/A)");
                        continue;
                    }
                    var timeAsStr = TimeFormat(bestDistance[room]);
                    if (bestDistance[room]<=timeLimit)
                    {
                        Console.WriteLine($"Safe {room} ({timeAsStr})");
                    }
                    else
                    {
                        Console.WriteLine($"Unsafe {room} ({timeAsStr})");
                    }
                }
            }
           
        }

        private static string TimeFormat(int time)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(time / 3600).ToString().PadLeft(2,'0')}");
            sb.Append(":");
            sb.Append($"{(time / 60).ToString().PadLeft(2, '0')}");
            sb.Append(":");
            sb.Append($"{(time % 60).ToString().PadLeft(2, '0')}");
            return sb.ToString();
        }

        private static int GetDistance(int startNode, int exit)
        {
            var distance = Enumerable.Repeat(int.MaxValue, graph.Length).ToArray();
            distance[startNode] = 0;
            var queue = new OrderedBag<int>
                (Comparer<int>.Create((f, s) => distance[f] - distance[s]));
            queue.Add(startNode);
            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                if (node == exit)
                {
                    return distance[exit];
                }
                foreach (var edge in graph[node])
                {
                    var child = node == edge.First
                        ? edge.Second
                        : edge.First;
                    if (distance[child]==int.MaxValue)
                    {
                        queue.Add(child);
                    }
                    var newDistance = distance[node] + edge.Time;
                    if (newDistance<distance[child])
                    {
                        distance[child] = newDistance;
                        queue = new OrderedBag<int>
                            (queue,
                            Comparer<int>.Create((f, s) => distance[f] - distance[s]));
                    }
                }
            }
            return distance[exit];
        }

        private static List<Edge>[] ReadGraph(int n, int e, HashSet<int> exits)
        {
            var result = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = new List<Edge>();
            }
            for (int i = 0; i < e; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .ToArray();
                var first = int.Parse(input[0]);
                var second = int.Parse(input[1]);
                var time = ReadTime(input[2]);
                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Time = time
                };
                if (exits.Contains(first))
                {
                    result[second].Add(edge);
                    continue;
                }
                if (exits.Contains(second))
                {
                    result[first].Add(edge);
                    continue;
                }
                result[first].Add(edge);
                result[second].Add(edge);
            }
            return result;
        }

        private static int ReadTime(string time)
        {
            var timeArgs = time.Split(new[] { ":" },
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            return timeArgs[0] * 60 + timeArgs[1];
        }

        private static HashSet<int> ReadExits()
        {
            var result = new HashSet<int>();
            var exitArgs = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            foreach (var exit in exitArgs)
            {
                result.Add(exit);
            }
            return result;
        }
    }
}
