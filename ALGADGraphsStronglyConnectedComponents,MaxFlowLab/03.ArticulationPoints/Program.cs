using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.ArticulationPoints
{
    class Program
    {
        private static List<int>[] graph;
        private static int[] depth;
        private static int[] lowpoint;
        private static int[] parents;
        private static bool[] visited;
        private static List<int> articulationPoints;
        static void Main(string[] args)
        {
            int nodes =int.Parse(Console.ReadLine());
            int inputLines =int.Parse(Console.ReadLine());
            graph = ReadGraph(nodes, inputLines);
            parents = Enumerable.Repeat(-1, nodes).ToArray();
            depth = new int[nodes];
            lowpoint = new int[nodes];
            visited = new bool[nodes];
            articulationPoints = new List<int>();
            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                FindArticulationPoints(node, 1);
                }
            }
            Console.WriteLine($"Articulation points: " +
                $"{string.Join(", ",articulationPoints)}");
        }

        private static void FindArticulationPoints(int node, int d)
        {
            visited[node] = true;
            depth[node] = d;
            lowpoint[node] = d;
            var isArticulation = false;
            var childCount = 0;
            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parents[child] = node;
                    FindArticulationPoints(child, d + 1);
                    childCount += 1;
                    if (lowpoint[child]>=depth[node])
                    {
                        isArticulation = true;
                    }
                    lowpoint[node] =
                        Math.Min(lowpoint[child], lowpoint[node]);
                }
                if (child!=parents[node])
                {
                    lowpoint[node] =
                        Math.Min(lowpoint[node], depth[child]);
                }
            }
            if ((parents[node]==-1&&childCount>1)
                ||(parents[node]!=-1&&isArticulation))
            {
                articulationPoints.Add(node);
            }
        }

        private static List<int>[] ReadGraph(int nodes, int inputLines)
        {
            var result = new List<int>[nodes];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }
            for (int i = 0; i < inputLines; i++)
            {
                var inputData = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                var first = inputData[0];
                for (int j = 1; j < inputData.Length; j++)
                {
                    var second = inputData[j];
                    result[first].Add(second);
                    result[second].Add(first);
                }
            }
            return result;
        }
    }
}
