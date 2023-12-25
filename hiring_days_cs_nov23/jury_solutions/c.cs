using System;
using System.Collections.Generic;
using System.Linq;
 
public class Program
{
    public static void Main()
    {
        Console.ReadLine();
        var train = new[] {ReadInts(), ReadInts()};
        int n = train[0].Length;
        var isGoodPrefix = new bool[n, 2];
        var parent = new int[n, 2];

        parent[0, 0] = -1;
        parent[0, 1] = -1;
        isGoodPrefix[0, 0] = true;
        isGoodPrefix[0, 1] = true;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (isGoodPrefix[i, j] && train[k][i + 1] > train[j][i] && train[1 - k][i + 1] < train[1 - j][i])
                    {
                        isGoodPrefix[i + 1, k] = true;
                        parent[i + 1, k] = j;
                    }
                }
            }
        }

        int last = -1;
        for (int i = 0; i < 2; i++)
            if (isGoodPrefix[n - 1, i])
                last = i;

        if (last == -1)
        {
            Console.WriteLine(-1);
            return;
        }

        List<int> ans = new List<int>();
        int current = n - 1;
        while (last != -1)
        {
            if (last == 1)
                ans.Add(current + 1);
            last = parent[current, last];
            current--;
        }

        ans.Reverse();
        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
    }

    private static int[] ReadInts()
    {
        return Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();
    }
}
