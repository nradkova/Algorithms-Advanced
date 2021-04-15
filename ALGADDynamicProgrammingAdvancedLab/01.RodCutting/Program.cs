using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.RodCutting
{
    class Program
    {
        static void Main(string[] args)
        {
            var prices = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int n =int.Parse(Console.ReadLine());
            var bestPrices =new int[n + 1];
            var prevBest = new int[n + 1];
            int result = CutRot(n, prices, bestPrices, prevBest);
            Console.WriteLine(result);
            var parts = new Queue<int>();
            while (n!=0)
            {
                parts.Enqueue(prevBest[n]);
                n =n- prevBest[n];
            }
            Console.WriteLine(string.Join(" ",parts));
        }

        private static int CutRot(int n, int[] prices,
            int[] bestPrices, int[] prevBest)
        {
            if (bestPrices[n]>0)
            {
                return bestPrices[n];
            }
            if (n==0)
            {
                return 0;
            }
            var currentBest = bestPrices[n];
            for (int i = 1; i <=n; i++)
            {
                currentBest =
                    Math.Max(currentBest, prices[i] 
                    + CutRot(n-i,prices,bestPrices,prevBest));
                if (currentBest>bestPrices[n])
                {
                    bestPrices[n] = currentBest;
                    prevBest[n] = i;
                }
            }
            return bestPrices[n];
        }
    }
}
