using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.LIS
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var len = new int[numbers.Length];
            var prev = Enumerable.Repeat(-1, numbers.Length).ToArray();
            var bestSeq = 0;
            var lastInd = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                var currentBestSeq = 1;
                for (int j = i - 1; j >= 0; j--)
                {
                    var prevNumber = numbers[j];
                    if (prevNumber<number
                        &&len[j]+1>=currentBestSeq)
                    {
                        currentBestSeq = len[j] + 1;
                        prev[i] = j;
                    }
                }
                len[i] = currentBestSeq;
                if (bestSeq<currentBestSeq)
                {
                    bestSeq = currentBestSeq;
                    lastInd = i;
                }
            }

            var sequence = new Stack<int>();
            while (lastInd!=-1)
            {
                sequence.Push(numbers[lastInd]);
                lastInd = prev[lastInd];
            }
            Console.WriteLine(string.Join(" ",sequence));
        }
    }
}
