using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.LongestZigzagSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var lenLess = new int[numbers.Length];
            var lenGreater = new int[numbers.Length];
            var prev = Enumerable
                .Repeat(-1, numbers.Length).ToArray();

            lenGreater[0] = 1;
            lenLess[0] = 1;
            var currentBestSeq = 1;
            var bestSeq = 0;
            var lastInd = 0;

            for (int i = 1; i < numbers.Length; i++)
            {
                var current = numbers[i];
                for (int j = 0; j < i; j++)
                {
                    var previous = numbers[j];

                    if (current > previous 
                        && lenGreater[i] < lenLess[j] + 1)
                    {
                        lenGreater[i] = lenLess[j] + 1;
                        currentBestSeq = lenLess[j] + 1;
                        prev[i] = j;
                    }

                    if (current < previous 
                        && lenLess[i] < lenGreater[j] + 1)
                    {
                        lenLess[i] = lenGreater[j] + 1;
                        currentBestSeq = lenGreater[j] + 1;
                        prev[i] = j;
                    }
                }
                if (currentBestSeq>bestSeq)
                {
                    bestSeq = currentBestSeq;
                    lastInd = i;
                }
            }

            var result = new Stack<int>();
            while (lastInd != -1)
            {
                result.Push(numbers[lastInd]);
                lastInd = prev[lastInd];
            }
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
