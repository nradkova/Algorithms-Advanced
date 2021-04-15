using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.RoadTrip
{
    public class Item
    {
        public int Value { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Item> items;
        static void Main(string[] args)
        {
            items = ReadItems();
            int maxCapacity =int.Parse(Console.ReadLine());
            var table = new int[items.Count + 1, maxCapacity + 1];
            for (int row = 1; row < table.GetLength(0); row++)
            {
                var item = items[row - 1];
                for (int capacity = 1; capacity < table.GetLength(1); capacity++)
                {
                    var skip = table[row - 1, capacity];
                    if (item.Weight>capacity)
                    {
                        table[row, capacity] = skip;
                        continue;
                    }
                    var take = item.Value + table[row - 1, capacity - item.Weight];
                    table[row, capacity] = Math.Max(skip, take);
                }
            }
            Console.WriteLine($"Maximum value: {table[items.Count,maxCapacity]}");
        }

        private static List<Item> ReadItems()
        {
            var result = new List<Item>();
            var values = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            var weights = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                result.Add(new Item { Value = values[i], Weight = weights[i] });
            }
            return result;
        }
    }
}
