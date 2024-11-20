using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        Solve();
    }

    static List<(int X, int Y)> steps = new List<(int X, int Y)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
    
    static bool IsInBounds(int x, int y, int n, int m) => x >= 0 && x < n && y >= 0 && y < m;
    
    static void DfsNoRec(int startX, int startY, int setNumber, int n, int m,
        List<int> setsNumbers, 
        char[,] field,
        int[,] used
    )
    {
        Stack<(int X, int Y)> stack = new Stack<(int X, int Y)>();
        stack.Push((startX, startY));
        used[startX, startY] = setNumber;

        while (stack.Count > 0)
        {
            var (currentX, currentY) = stack.Pop();
            setsNumbers[setNumber]++;
            foreach (var position in steps.Select(s => (s.X + currentX, s.Y + currentY)).Where(s =>
                         IsInBounds(s.Item1, s.Item2, n, m) 
                         && field[s.Item1, s.Item2] == 'X' 
                         && used[s.Item1, s.Item2] == 0)
                    )
            {
                used[position.Item1, position.Item2] = setNumber;
                stack.Push((position.Item1, position.Item2));
            }
        }
    }
    
    public static void Solve()
    {
        var nmq = Console.ReadLine().Split();

        var n = int.Parse(nmq[0]);
        var m = int.Parse(nmq[1]);
        var q = int.Parse(nmq[2]);

        var setsNumbers = new List<int>(n * m) { 0 };

        var field = new char[n, m];
        var used = new int[n, m];

        for (var i = 0; i < n; i++)
        {
            var line = Console.ReadLine();
            for (var j = 0; j < m; j++)
                field[i, j] = line[j];
        }

        var setCounter = 0;
        for (var i = 0; i < n; i++)
        for (var j = 0; j < m; j++)
        {
            if (used[i, j] == 0 && field[i, j] == 'X')
            {
                setCounter++;
                setsNumbers.Add(0);
                DfsNoRec(i, j, setCounter, n, m, setsNumbers, field, used);
            }
        }

        while (q-- > 0)
        {
            var position = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var x = position[0] - 1;
            var y = position[1] - 1;
            if (used[x, y] == 0)
                Console.WriteLine("MISS");
            else
            {
                setsNumbers[used[x, y]]--;
                Console.WriteLine(setsNumbers[used[x, y]] == 0 ? "DESTROY" : "HIT");
            }
        }
    }
}