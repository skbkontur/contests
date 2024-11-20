using System;
using System.Linq;


class Program
{
    static void Main()
    {
        Solve();
    }

    static void Solve()
    {
        int n = int.Parse(Console.ReadLine());
        var throwsX = 0L;
        var throwsY = 0L;

        for (int i = 0; i < n; i++)
        {
            var longs = Console.ReadLine().Split().Select(long.Parse).ToArray();
            throwsX -= longs[0];
            throwsY -= longs[1];
        }

        for (int i = 0; i < n; i++)
        {
            var longs = Console.ReadLine().Split().Select(long.Parse).ToArray();
            throwsX += longs[0];
            throwsY += longs[1];
        }

        Console.WriteLine($"{throwsX / n} {throwsY / n}");
    }
}
