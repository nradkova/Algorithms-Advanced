using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.MostReliablePath
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }
    class Program
    {
        private static List<Edge>[] edges;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            edges = ReadEdges(n, e);
            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());
            var realibilities = Enumerable
                .Repeat(double.NegativeInfinity, n)
                .ToArray();
            var prev = Enumerable
                .Repeat(-1, n)
                .ToArray();
            realibilities[source] = 100;

            var queue = new OrderedBag<int>
                (Comparer<int>.Create((x, y) =>
                (int)(realibilities[y] - realibilities[x])));
           
            queue.Add(source);
            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                if (node == destination)
                {
                    break;
                }
                var children = edges[node];
               
                foreach (var child in children)
                {
                    var childNode = child.First == node
                        ? child.Second
                        : child.First;
                    if (double.IsNegativeInfinity(realibilities[childNode]))
                    {
                    queue.Add(childNode);
                    }
                    var newRealibility =
                        (realibilities[node] * child.Weight) / 100;
                    if (newRealibility > realibilities[childNode])
                    {
                        realibilities[childNode] = newRealibility;
                        prev[childNode] = node;
                        queue = new OrderedBag<int>(
                             queue,
                             Comparer<int>.Create((x, y) =>
                             (int)(realibilities[y] - realibilities[x])));
                    }

                }
            }
            var path = GetPath(prev, destination);
            Console.WriteLine($"Most reliable path reliability: " +
                $"{realibilities[destination]:f2}%");
            Console.WriteLine(string.Join(" -> ", path));
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

        private static List<Edge>[] ReadEdges(int n, int e)
        {
            var result = new List<Edge>[n];
            for (int i = 0; i < n; i++)
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
