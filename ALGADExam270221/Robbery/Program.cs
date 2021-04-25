using System;
using System.Collections.Generic;
using System.Linq;

namespace _02
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
    }
    class Program
    {
        private static Dictionary<int, List<int>> graph;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int target = int.Parse(Console.ReadLine());
            graph = ReadGraph(n, target);
            var targetNode = 0;
            foreach (var node in graph.Keys)
            {
                var visited = new bool[graph.Count + 1];
                int connected = FindConnectedComponents(node, visited);
                if (connected == target)
                {
                    targetNode = node;
                    break;
                }
            }
            Console.WriteLine(targetNode);
            
        }

        private static int FindConnectedComponents(int node, bool[] visited)
        {

            var connected = 0;
            if (visited[node])
            {
                return connected;
            }
            visited[node] = true;
            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    FindConnectedComponents(child, visited);
                    visited[child] = true;
                    connected++;
                }
            }

            return connected;
        }

        private static Dictionary<int, List<int>> ReadGraph(int n, int target)
        {
            var result = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                var currentNode = i + 1;
                result.Add(currentNode, new List<int>());
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                for (int j = 0; j < input.Length; j++)
                {
                    result[currentNode].Add(input[j]);
                }

            }
            return result;
        }
    }
}
