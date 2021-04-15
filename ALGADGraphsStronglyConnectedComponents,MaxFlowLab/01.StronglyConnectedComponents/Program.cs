using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.StronglyConnectedComponents
{
    class Program
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;

        static void Main(string[] args)
        {
            int nodesCount =int.Parse(Console.ReadLine());
            int edgesCount =int.Parse(Console.ReadLine());
            (graph, reversedGraph) = ReadInputData(nodesCount, edgesCount);
            var topologicallySorted = GetTopologicallySorted(graph);
            var visited = new bool[reversedGraph.Length];
            Console.WriteLine("Strongly Connected Components: ");
            while (topologicallySorted.Count>0)
            {
                var node = topologicallySorted.Pop();
                if (!visited[node])
                {
                    var connected = new Stack<int>();
                    DFS(node, reversedGraph, visited, connected);
                    Console.WriteLine($"{{{string.Join(", ",connected)}}}");
                }
            }
        }

        private static Stack<int> GetTopologicallySorted(List<int>[] graph)
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                var node = i;
                DFS(node, graph, visited, result);
            }
            return result;
        }

        private static void DFS(int node, List<int>[] map,
            bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }
            visited[node] = true;
            foreach (var child in map[node])
            {
                DFS(child, map, visited, result);
            }
            result.Push(node);
        }

        private static (List<int>[] initial, List<int>[] reversed) 
            ReadInputData(int nodesCount, int edgesCount)
        {
            var result = new List<int>[nodesCount];
            var reversedResult = new List<int>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                result[i] = new List<int>();
                reversedResult[i] = new List<int>();
            }
            for (int i = 0; i < edgesCount; i++)
            {
                var input = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                var node = input[0];
                for (int j = 1; j < input.Length; j++)
                {
                    var child = input[j];
                    result[node].Add(child);
                    reversedResult[child].Add(node);
                }
            }
            return (result, reversedResult);
        }
    }
}
