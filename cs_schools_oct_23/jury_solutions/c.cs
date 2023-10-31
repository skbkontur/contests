using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var a = new List<int>[n];
        var order = new SortedDictionary<int, int>();
        for (var i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split();
            var m = int.Parse(input[0]);

            a[i] = new List<int>();
            for (var j = 1; j <= m; j++)
                a[i].Add(int.Parse(input[j]));

            var arr = a[i].ToArray();
            var isSorted = IsSorted(arr);
            if (isSorted)
            {
                if (order.ContainsKey(arr[0]))
                {
                    Console.WriteLine(-1);
                    return;
                }

                order[arr[0]] = i;
                continue;
            }

            arr = arr.Reverse().ToArray();
            isSorted = IsSorted(arr);
            if (isSorted)
            {
                if (order.ContainsKey(arr[0]))
                {
                    Console.WriteLine(-1);
                    return;
                }

                order[arr[0]] = i;
                a[i] = arr.ToList();
                continue;
            }

            Console.WriteLine(-1);
            return;
        }

        var ans = new List<int>();
        foreach (var i in order.Values)
            ans.AddRange(a[i]);

        if (IsSorted(ans.ToArray()))
            Console.WriteLine(string.Join(" ", ans));
        else
            Console.WriteLine(-1);
    }

    private static bool IsSorted(int[] a)
    {
        for (var i = 1; i < a.Length; i++)
            if (a[i - 1] >= a[i])
                return false;
        return true;
    }
}