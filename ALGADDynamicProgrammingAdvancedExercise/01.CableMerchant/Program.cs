using System;
using System.Linq;

namespace _01.CableMerchant
{
    class Program
    {
        static void Main(string[] args)
        {
            var prices = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int connector =int.Parse(Console.ReadLine());
            var bestPrices = new int[prices.Length];
            for (int i = 0; i < bestPrices.Length; i++)
            {
                bestPrices[i] = CutRod(i, prices, bestPrices, connector);
            }
            Console.WriteLine(string.Join(" ",bestPrices));
        }

        private static int CutRod(int index, int[] prices,
            int[] bestPrices, int connector)
        {
            if (bestPrices[index]>0)
            {
                return bestPrices[index];
            }
            if (index==0)
            {
                return prices[index];
            }
            var currentBest = prices[index];
            for (int i = 1; i <= index; i++)
            {
                var prevBest = prices[i-1]-connector
                    + CutRod(index - i, prices, bestPrices, connector);
                
                currentBest =
                    Math.Max(currentBest, prevBest-connector);
                if (currentBest>bestPrices[index])
                {
                    bestPrices[index] = currentBest;
                }
            }
            return bestPrices[index];
        }
    }
}
