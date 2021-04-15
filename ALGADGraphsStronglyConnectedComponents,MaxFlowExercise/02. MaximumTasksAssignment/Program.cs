using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._MaximumTasksAssignment
{
    class Program
    {
        private static int[,] graph;
        private static int[] previous;
        static void Main(string[] args)
        {
            int people = int.Parse(Console.ReadLine());
            int tasks = int.Parse(Console.ReadLine());
            graph = ReadGraph(people, tasks);
            previous = Enumerable.Repeat(-1, tasks + people + 2).ToArray();
            int source = 0;
            int target = people + tasks + 1;
            while (BFS(source, target))
            {
                var node = target;
                while (node != source)
                {
                    var previousNode = previous[node];
                    graph[previousNode, node] = 0;
                    graph[node, previousNode] = 1;
                    node = previousNode;
                }
            }

            for (int person = 1; person < people + 1; person++)
            {
                for (int task = tasks + 1; task < graph.GetLength(0) - 1; task++)
                {
                    if (graph[task, person] == 1)
                    {
                        Console.WriteLine($"{(char)(64 + person)}-{task - tasks}");
                    }
                }
            }
        }

        private static bool BFS(int source, int target)
        {
            var visited = new bool[graph.GetLength(0)];
            var queue = new Queue<int>();
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
                    var current = graph[node, child];
                    if (!visited[child] && current > 0)
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        previous[child] = node;
                    }
                }
            }
            return false;
        }

        private static int[,] ReadGraph(int people, int tasks)
        {
            var nodes = people + tasks + 2;
            var result = new int[nodes, nodes];
            int start = 0;
            int end = nodes - 1;
            for (int person = 1; person <= people; person++)
            {
                result[start, person] = 1;
            }
            for (int task = people + 1; task < end; task++)
            {
                result[task, end] = 1;
            }
            for (int row = 1; row <= people; row++)
            {
                var input = Console.ReadLine();
                int index = 0;
                for (int col = people + 1; col < nodes - 1; col++)
                {
                    if (input[index] == 'Y')
                    {
                        result[row, col] = 1;
                    }
                    index++;
                }
            }
            return result;
        }
        private static void Print()
        {
            for (int row = 0; row < graph.GetLength(0); row++)
            {
                for (int col = 0; col < graph.GetLength(1); col++)
                {
                    Console.Write($"{graph[row, col]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
