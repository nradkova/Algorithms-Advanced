using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.MaxFlow
{
    class Program
    {
        private static int[,] graph;
        private static int[] parents;
        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            graph = ReadInput(nodes);
            int source = int.Parse(Console.ReadLine());
            int target = int.Parse(Console.ReadLine());
            parents = Enumerable.Repeat(-1, nodes).ToArray();
            int maxFlow = 0;
            while (BFS(source, target))
            {
                var currentFlow = GetCurrentFlow(source, target);
                maxFlow += currentFlow;
                ModifyCapacities(source, target, currentFlow);
            }
            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static void ModifyCapacities(int source, int target, int flow)
        {
            var node = target;
            while (node != source)
            {
                var parent = parents[node];
                graph[parent, node] -= flow;
                node = parent;
            }
        }

        private static int GetCurrentFlow(int source, int target)
        {
            int capacity = int.MaxValue;
            var node = target;
            while (node != source)
            {
                var parent = parents[node];
                int currentFlow = graph[parent, node];
                if (currentFlow < capacity)
                {
                    capacity = currentFlow;
                }
                node = parent;
            }
            return capacity;
        }

        private static bool BFS(int source, int target)
        {
            var queue = new Queue<int>();
            var visited = new bool[graph.GetLength(0)];
            visited[source] = true;
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == target)
                {
                    return true;
                }
                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        visited[child] = true;
                        parents[child] = node;
                        queue.Enqueue(child);
                    }
                }
            }

            return false;
        }

        private static int[,] ReadInput(int nodes)
        {
            var result = new int[nodes, nodes];
            for (int node = 0; node < nodes; node++)
            {
                var capacities = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                for (int child = 0; child < nodes; child++)
                {
                    result[node, child] = capacities[child];
                }

            }

            return result;
        }
    }
}
