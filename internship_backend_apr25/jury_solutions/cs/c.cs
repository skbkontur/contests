using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2
{
    internal enum State
    {
        New,
        Open,
        Close,
    }

    internal class Colour
    {
        public int RightBorder = -1;
        public int LeftBorder = int.MaxValue;
        public State State = State.New;
    }

    internal class Program
    {
        private static readonly Dictionary<int, Colour> Colours = new Dictionary<int, Colour>();

        public static void Main(string[] args)
        {
            var mn = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var m = mn[0];
            var colorsLine = Console.ReadLine().Split(' ').Select(x => int.Parse(x) - 1).ToArray();
            for (int i = 0; i < m; i++)
            {
                var colourId = colorsLine[i];
                if (!Colours.ContainsKey(colourId))
                    Colours[colourId] = new Colour();
                if (i < Colours[colourId].LeftBorder) Colours[colourId].LeftBorder = i;
                if (i > Colours[colourId].RightBorder) Colours[colourId].RightBorder = i;
            }

            var stack = new Stack<int>();
            var coloursIds = new List<int>(m);
            for (int i = 0; i < m; i++)
            {
                var colourId = colorsLine[i];
                switch (Colours[colourId].State)
                {
                    case State.New:
                        stack.Push(i);
                        Colours[colourId].State = State.Open;
                        break;
                    case State.Open:
                    {
                        while (colorsLine[stack.Peek()] != colourId)
                        {
                            coloursIds.Add(colorsLine[stack.Peek()]);
                            Colours[colorsLine[stack.Pop()]].State = State.Close;
                        }

                        break;
                    }
                    case State.Close:
                        Console.WriteLine(-1);
                        return;
                }
            }

            while (stack.Count > 0)
                coloursIds.Add(colorsLine[stack.Pop()]);

            Console.WriteLine(coloursIds.Count);
            coloursIds.Reverse();
            foreach (var colourId in coloursIds)
            {
                Console.WriteLine($"{colourId + 1} {Colours[colourId].LeftBorder + 1} {Colours[colourId].RightBorder + 1}");
            }
        }
    }
}