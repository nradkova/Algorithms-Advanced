using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.LongestStringChain
{
    class Program
    {
        static void Main(string[] args)
        {
            var strings = Console.ReadLine()
                .Split();
            var len = new int[strings.Length];
            var prev = new int[strings.Length];
            var longestSeq = 0;
            var lastInd = 0;

            for (int current = 0; current < strings.Length; current++)
            {
                var currentStr = strings[current];
                prev[current] = -1;
                len[current] = 1;

                for (int previous = current - 1; previous >= 0; previous--)
                {
                    var prevStr = strings[previous];
                    if (prevStr.Length < currentStr.Length
                        && len[previous] + 1 >= len[current])
                    {
                        len[current] = len[previous] + 1;
                        prev[current] = previous;
                    }
                }

                if (longestSeq < len[current])
                {
                    longestSeq = len[current];
                    lastInd = current;
                }
            }

            var result = new SortedSet<string>
                (Comparer<string>.Create((f, s) => f.Length - s.Length));

            while (lastInd > -1)
            {
                result.Add(strings[lastInd]);
                lastInd = prev[lastInd];
            }
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
