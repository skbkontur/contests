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

        int max = -1;
        string res = "";

        for (int i = 0; i < n; i++)
        {
            var s = Console.ReadLine();
            int unique = s.Distinct().Count();

            if (unique > max)
            {
                res = s;
                max = unique;
            }
        }

        Console.WriteLine($"{max} {res}");
    }
}
