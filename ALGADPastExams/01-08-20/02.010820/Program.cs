using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._010820
{
    class Program
    {
        private static int[,] graph;
        private static int[] prev;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int source = input[0];
            int destination = input[1];
            graph = ReadGraph(n, m);
            prev = new int[n];
            int maxFlow = 0;
            while (BFS(source, destination))
            {
                var currentFlow = GetCurrentFlow(source, destination);
                ModifyCapacities(source, destination, currentFlow);
                maxFlow += currentFlow;
            }

            Console.WriteLine(maxFlow);
        }

        private static void ModifyCapacities(int source, int destination, int currentFlow)
        {
            var node = destination;
            while (node != source)
            {
                var parent = prev[node];
                graph[parent, node] -= currentFlow;
                node = parent;
            }
        }

        private static int GetCurrentFlow(int source, int destination)
        {
            int capacity = int.MaxValue;
            var node = destination;
            while (node != source)
            {
                var parent = prev[node];
                var currentFlow = graph[parent, node];
                if (currentFlow < capacity)
                {
                     capacity=currentFlow;
                }
                node = parent;
            }
            return capacity;
        }

        private static bool BFS(int source, int destination)
        {
            var queue = new Queue<int>();
            var visited = new bool[graph.GetLength(0)];
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == destination)
                {
                    return true;
                }
                visited[node] = true;
                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        prev[child] = node;
                    }
                }
            }
            return false;
        }

        private static int[,] ReadGraph(int n, int m)
        {
            var result = new int[n, n];
            for (int i = 0; i < m; i++)
            {
                var line = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int parent = line[0];
                int child = line[1];
                int capacity = line[2];
                result[parent, child] = capacity;
            }
            return result;
        }
    }
}
