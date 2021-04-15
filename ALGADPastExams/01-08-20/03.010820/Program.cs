using System;
using System.Collections.Generic;

namespace _03._010820
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstStr =Console.ReadLine().Split();
            string[] secondStr =Console.ReadLine().Split();
            var table = new int[firstStr.Length + 1, secondStr.Length + 1];
            for (int r = 1; r < table.GetLength(0); r++)
            {
                var first = firstStr[r - 1];
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    var second = secondStr[c - 1];
                    if (first==second)
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max
                            (table[r - 1, c], table[r, c - 1]);
                    }
                }
            }
            var lcs = new Stack<string>();
            int row = firstStr.Length;
            int col = secondStr.Length;
            while (row>0&&col>0)
            {
                if (firstStr[row-1]==secondStr[col-1])
                {
                    row--;
                    col--;
                    lcs.Push(firstStr[row]);
                }
                else if(table[row-1,col]>table[row,col-1])
                {
                    row--;
                }
                else
                {
                    col--;
                }
            }
            Console.WriteLine(string.Join(" ",lcs));
            Console.WriteLine(lcs.Count);
        }
    }
}
