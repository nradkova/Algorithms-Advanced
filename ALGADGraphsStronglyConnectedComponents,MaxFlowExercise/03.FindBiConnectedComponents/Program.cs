using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.FindBiConnectedComponents
{
    class Program
    {
        private static List<int>[] graph;
        private static int[] depth;
        private static int[] lowpoint;
        private static int[] parents;
        private static bool[] visited;
        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());
            graph = ReadGraph(nodes, edges);
            depth = new int[nodes];
            lowpoint = new int[nodes];
            parents = Enumerable.Repeat(-1, nodes).ToArray();
            visited = new bool[nodes];
            var biconnectedComponents = 0;
            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    biconnectedComponents
                        += FindBiconnectedPoints(node, 1);
                    
                }
            }
            Console.WriteLine($"Number of bi-connected components: " +
                $"{biconnectedComponents}");
        }

        private static int FindBiconnectedPoints(int node, int d)
        {
            int biconnectedComponents = 0;
            visited[node] = true;
            depth[node] = d;
            lowpoint[node] = d;
            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parents[child] = node;
                    biconnectedComponents += FindBiconnectedPoints(child, d + 1);
                    if (lowpoint[child]>=depth[node])
                    {
                        biconnectedComponents++;
                    }
                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);
                }
                else if (child!=parents[node])
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depth[child]);
                }
            }
            return biconnectedComponents;
        }

        private static List<int>[] ReadGraph(int nodes, int edges)
        {
            var result = new List<int>[nodes];
            for (int i = 0; i < nodes; i++)
            {
                result[i] = new List<int>();
            }
            for (int i = 0; i < edges; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int first = input[0];
                int second = input[1];
                result[first].Add(second);
                result[second].Add(first);
            }
            return result;
        }
    }
}
