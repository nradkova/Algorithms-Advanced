using System;
using System.Collections.Generic;

namespace _02.Knapsack
{
    class Program
    {
        public class Item
        {
            public string  Name { get; set; }
            public int Weight { get; set; }
            public int Value { get; set; }
        }
        static void Main(string[] args)
        {
            var maxCapacity =int.Parse(Console.ReadLine());
            var items = ReadInput();
            var prices = new int[items.Count + 1, maxCapacity + 1];
            var included = new bool[items.Count + 1, maxCapacity + 1];
            for (int row = 1; row < prices.GetLength(0); row++)
            {
                var item = items[row - 1];
                for (int capacity = 1; capacity < prices.GetLength(1); capacity++)
                {
                    var skip = prices[row - 1, capacity];
                    if (item.Weight>capacity)
                    {
                        continue;
                    }
                    var take=item.Value+ prices[row - 1, capacity-item.Weight];

                    if (take>skip)
                    {
                        prices[row, capacity] = take;
                        included[row, capacity] = true;
                    }
                    else
                    {
                        prices[row, capacity] = skip;
                    }
               
                }
            }
            var totalValue = prices[items.Count, maxCapacity];
            var totalWeight = 0;
            var takenItems = new SortedSet<string>();
            for (int row = included.GetLength(0) - 1; row >= 0; row--)
            {
                if (included[row,maxCapacity])
                {
                    var item = items[row - 1];
                    takenItems.Add(item.Name);
                    totalWeight += item.Weight;
                    maxCapacity -= item.Weight;
                }
            }
            Console.WriteLine($"Total Weight: {totalWeight}");
            Console.WriteLine($"Total Value: {totalValue}");
            Console.WriteLine(string.Join(Environment.NewLine,takenItems));
        }

        private static List<Item> ReadInput()
        {
            var result = new List<Item>();
            while (true)
            {
                var line =Console.ReadLine();
                if (line=="end")
                {
                    break;
                }
                var tokens = line.Split();
                result.Add(new Item
                {
                    Name = tokens[0],
                    Weight=int.Parse(tokens[1]),
                    Value=int.Parse(tokens[2])
                });
            }
            return result;
        }
    }
}
