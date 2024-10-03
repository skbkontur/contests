using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Solution
{
    static void Main()
    {
        new Solution().Run();
    }

    List<int> beautifulNumbers = new List<int>();

    void BruteForce(int now, int digits, int digitsNeed, bool[] used)
    {
        if (digits == digitsNeed)
            return;

        for (int d = digits == 0 ? 1 : 0; d <= 9; d++)
            if (!used[d])
            {
                used[d] = true;
                int next = now * 10 + d;
                beautifulNumbers.Add(next);
                BruteForce(next, digits + 1, digitsNeed, used);
                used[d] = false;
            }
    }

    void Run()
    {
        BruteForce(0, 0, 8, new bool[10]);
        beautifulNumbers.Add(1000000000);
        var beautifulNumbersArray = beautifulNumbers.OrderBy(x => x).ToArray();
        
        int t = ReadInts()[0];
        for (int i = 0; i < t; i++)
        {
            var ints = ReadInts();
            int l = ints[0];
            int r = ints[1];
            Console.WriteLine(CountLessThanOrEqualTo(beautifulNumbersArray, r) - CountLessThanOrEqualTo(beautifulNumbersArray, l - 1));
        }
    }

    private int CountLessThanOrEqualTo(int[] array, int bound)
    {
        int index = Array.BinarySearch(array, bound);
        if (index < 0)
            return ~index;
        else
            return index + 1;
    }

    int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }
}