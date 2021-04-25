using System;
using System.Collections.Generic;
using System.Linq;

namespace _01
{
    public class Street
    {
        public string Name { get; set; }
        public int Pokemons { get; set; }
        public int Fuel { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int maxFuel = int.Parse(Console.ReadLine());
            var streets = ReadStreets();
            var included = new bool[streets.Count + 1, maxFuel + 1];

            var table = new int[streets.Count + 1, maxFuel + 1];
            for (int streetIndex = 0; streetIndex <streets.Count; streetIndex++)
            {
                var street = streets[streetIndex];
                var rowIndex = streetIndex + 1;
                for (int fuel = 0; fuel <=maxFuel; fuel++)
                {
                    if (street.Fuel > fuel)
                    {
                        continue;
                    }

                    var skip = table[rowIndex - 1 , fuel];
                    var take = street.Pokemons
                        + table[rowIndex - 1, fuel - street.Fuel];

                    if (take > skip)
                    {
                        table[rowIndex, fuel] = take;
                        included[rowIndex, fuel] = true;
                    }
                    else
                    {
                        table[rowIndex, fuel] = skip;
                    }
                }
            }

            var fuelLeft = maxFuel;
            var pokemonsTaken = 0;
            var includedStreets = new SortedSet<string>();
            for (int streetIndex = streets.Count - 1; streetIndex >= 0; streetIndex--)
            {
                if (included[streetIndex+1, maxFuel])
                {
                    var street = streets[streetIndex];
                    includedStreets.Add(street.Name);
                    pokemonsTaken += street.Pokemons;
                    fuelLeft -= street.Fuel;
                }
            }

            if (includedStreets.Count > 0)
            {
                Console.WriteLine(string.Join(" -> ", includedStreets));
            }
            Console.WriteLine($"Total Pokemon caught -> {pokemonsTaken}");
            Console.WriteLine($"Fuel Left -> {fuelLeft}");
        }

        private static List<Street> ReadStreets()
        {
            var result = new List<Street>();
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                var tokens = input.Split(", ",
                    StringSplitOptions.RemoveEmptyEntries);
                var name = tokens[0];
                var pokemons = int.Parse(tokens[1]);
                var fuel = int.Parse(tokens[2]);

                var street = new Street
                {
                    Name = name,
                    Pokemons = pokemons,
                    Fuel = fuel
                };

                result.Add(street);
            }
            return result;
        }
    }
}
