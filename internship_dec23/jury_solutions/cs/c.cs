using System;
using System.Linq;
using System.Text;
 
public class Program
{
    public static void Main()
    {
        var ints = ReadInts();
        int n = ints[0];
        int m = ints[1];
        int t = int.Parse(Console.ReadLine());

        var colorsH = new (int, int)[n];
        var colorsV = new (int, int)[m];

        for (int i = 1; i <= t; i++)
        {
            var line = ReadInts();
            int x = line[0] - 1;
            int y = line[1] - 1;
            int c = line[2];
            colorsH[x] = (c, i);
            colorsV[y] = (c, i);
        }

        var result = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                var ans1 = colorsH[i];
                var ans2 = colorsV[j];
                result.Append(ans1.Item2 > ans2.Item2 ? ans1.Item1 : ans2.Item1);
                result.Append(' ');
            }

            result.AppendLine();
        }

        Console.Write(result);
    }

    private static int[] ReadInts()
    {
        return Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();
    }
}
