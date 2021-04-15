using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.ElectricalSubstationNetwork
{
    class Program
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int lines = int.Parse(Console.ReadLine());
            (graph, reversedGraph) = ReadGraphs(nodes, lines);
            var topologicallySorted = GetTopologicallySorting();
            var visited = new bool[reversedGraph.Length];
            while (topologicallySorted.Count > 0)
            {
                var node = topologicallySorted.Pop();
                if (!visited[node])
                {
                    var connectedComponents = new Stack<int>();
                    DFS(node, connectedComponents, visited, reversedGraph);
                    Console.WriteLine(string.Join(", ",connectedComponents));
                }
            }
        }

        private static Stack<int> GetTopologicallySorting()
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                var node = i;
                DFS(node, result, visited, graph);
            }
            return result;
        }

        private static void DFS(int node, Stack<int> result,
            bool[] visited, List<int>[] map)
        {
            if (visited[node])
            {
                return;
            }
            visited[node] = true;
            foreach (var child in map[node])
            {
                DFS(child, result, visited, map);
            }
            result.Push(node);
        }

        private static (List<int>[] graph, List<int>[] reversedGraph)
            ReadGraphs(int nodes, int lines)
        {
            var result = new List<int>[nodes];
            var reversedResult = new List<int>[nodes];

            for (int i = 0; i < nodes; i++)
            {
                result[i] = new List<int>();
                reversedResult[i] = new List<int>();
            }
            for (int i = 0; i < lines; i++)
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
