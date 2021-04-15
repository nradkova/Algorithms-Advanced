using System;
using System.Collections.Generic;
using System.Linq;
//using Wintellect.PowerCollections;

namespace _02.Second
{
    class Program
    {
        public class Box
        {
            public int Width { get; set; }
            public int Depth { get; set; }
            public int Height { get; set; }
        }
        static void Main(string[] args)
        {
            int n =int.Parse(Console.ReadLine());
            var boxes = ReadInfo(n);
            var len = new int[n];
            var prev = new int[n];
            var bestLen = 0;
            var lastInd = 0;
            for (int i = 0; i < boxes.Count; i++)
            {
                len[i] = 1;
                prev[i]=-1;
                var box = boxes[i];
                var currentLen = 1;
                for (int j = 0; j < i; j++)
                {
                    var prevBox = boxes[j];
                    if (IsSmaller(prevBox, box)
                        &&len[j]+1>=currentLen)
                    {
                        currentLen = len[j] + 1;
                        prev[i] = j;
                    }
                }
                len[i] = currentLen;
                if (currentLen>bestLen)
                {
                    bestLen = currentLen;
                    lastInd = i;
                }
            }
            var BoxStack = new Stack<Box>();
            while (lastInd!=-1)
            {
                BoxStack.Push(boxes[lastInd]);
                lastInd = prev[lastInd];
            }
            foreach (var box in BoxStack)
            {
                Console.WriteLine($"{box.Width} {box.Depth} {box.Height}");
            }
        }

        private static bool IsSmaller(Box prevBox, Box box)
        {
            if (box.Width<=prevBox.Width)
            {
                return false;
            }
            if (box.Height <= prevBox.Height)
            {
                return false;
            }
            if (box.Depth <= prevBox.Depth)
            {
                return false;
            }
            return true;
        }

        private static List<Box> ReadInfo(int n)
        {
            var boxes = new List<Box>();
            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var box = new Box
                {
                    Width = tokens[0],
                    Depth = tokens[1],
                    Height = tokens[2]
                };
                boxes.Add(box);
            }
            return boxes;
        }
    }
}
