using System;
using System.Collections.Generic;
using System.Linq;

namespace Summer2024
{
    public class Program
    {
        public static void Main()
        {
            var n = ReadInt();
            var xCoords = new Dictionary<int, List<int>>();
            var yCoords = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                var (x, y) = Read2Int();
                if (!xCoords.ContainsKey(x))
                    xCoords[x] = new List<int>();
                xCoords[x].Add(y);
                
                if (!yCoords.ContainsKey(y))
                    yCoords[y] = new List<int>();
                yCoords[y].Add(x);
            }

            xCoords = xCoords.ToDictionary(pair => pair.Key, pair => pair.Value.OrderBy(x => x).ToList());
            yCoords = yCoords.ToDictionary(pair => pair.Key, pair => pair.Value.OrderBy(x => x).ToList());

            var q = ReadInt();
            (int X, int Y) current = (0, 0);

            for (int i = 0; i < q; i++)
            {
                var newState = current;
                var command = Console.ReadLine();
                var number = int.Parse(command.Split().Last());
                switch (command[0])
                {
                    case 'U': newState.Y += number;
                        break;
                    case 'D': newState.Y -= number;
                        break;
                    case 'R': newState.X += number;
                        break;
                    case 'L': newState.X -= number;
                        break;
                }
                
                if (newState.X == current.X)
                {
                    var (y1, y2) = (current.Y, newState.Y);
                    if (y1 > y2)
                        (y1, y2) = (y2, y1);

                    if (xCoords.ContainsKey(current.X) && BinarySearch(xCoords[current.X], y1, y2))
                    {
                        Console.WriteLine($"Stop {i + 1}");
                        return;
                    }
                }
                else {
                    var (x1, x2) = (current.X, newState.X);
                    if (x1 > x2)
                        (x1, x2) = (x2, x1);

                    if (yCoords.ContainsKey(current.Y) && BinarySearch(yCoords[current.Y], x1, x2))
                    {
                        Console.WriteLine($"Stop {i + 1}");
                        return;
                    }
                }

                current = newState;
            }
            
            Console.WriteLine($"Complete");
        }
        
        private static bool BinarySearch(List<int> array, int left, int right)
        {
            var (low, high) = (0, array.Count - 1);
            while (low <= high)
            {
                var mid = (low + high) / 2;
                if (array[mid] >= left && array[mid] <= right)
                    return true;
                
                if (array[mid] < left)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            
            return false;
        }

        private static int ReadInt() => int.Parse(Console.ReadLine());

        private static (int, int) Read2Int()
        {
            var s = Console.ReadLine().Split().ToArray();
            return (int.Parse(s[0]), int.Parse(s[1]));
        }
    }
}