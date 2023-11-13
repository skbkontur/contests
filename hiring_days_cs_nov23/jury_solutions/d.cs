using System;
using System.Linq;
 
public static class Program
{
    public static void Main()
    {
        var k = Console.ReadLine().Split().Select(int.Parse).ToArray()[1];
        var s = Console.ReadLine();
        var ans = 0;
        foreach (var c in new[] {'R', 'G', 'B'})
        {
            var replacesCount = 0;
            var start = 0;
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] != c)
                    replacesCount++;
                while (replacesCount > k && start < i)
                    if (s[start++] != c)
                        replacesCount--;
                ans = Math.Max(ans, i - start + 1);
            }
        }
        Console.WriteLine(ans);
    }
}