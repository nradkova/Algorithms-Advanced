using System;
using System.Linq;
using System.Collections.Generic;

namespace _02.BattlePoints
{
    public class Battle
    {
        public int Cost { get; set; }
        public int Gain { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var costs = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var gains = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var battles = new List<Battle>();

            int maxEnergy = int.Parse(Console.ReadLine());
            for (int i = 0; i < costs.Length; i++)
            {
                var cost = costs[i];
                if (cost <= maxEnergy)
                {
                    var gain = gains[i];
                    battles.Add(new Battle { Cost = cost, Gain = gain });
                }
            }

            var table = new int[battles.Count + 1, maxEnergy + 1];
            for (int row = 1; row < table.GetLength(0); row++)
            {
                var battle = battles[row - 1];
                for (int energy = 1; energy <= maxEnergy; energy++)
                {
                    var skip = table[row - 1, energy];
                    if (battle.Cost > energy)
                    {
                        table[row, energy] = skip;
                        continue;
                    }
                    var take = battle.Gain + table[row - 1, energy - battle.Cost];
                   
                    table[row, energy] = Math.Max(take, skip);
                }
            }
            Console.WriteLine(table[battles.Count, maxEnergy]);
        }
    }
}
