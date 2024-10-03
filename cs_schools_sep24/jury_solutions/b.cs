using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    static void Main()
    {
        new Solution().Run();
    }

    void Run()
    {
        int n = int.Parse(Console.ReadLine());
        var weights = ReadLongs();
        long sum = weights.Sum();
        if (sum % 2 == 1)
        {
            Console.WriteLine("0 0");
            return;
        }

        long currentSum = 0;
        var prefixSums = new Dictionary<long, int>();
        prefixSums[0] = -1;
        for (int i = 0; i < n; ++i)
        {
            currentSum += weights[i];
            prefixSums[currentSum] = i;
            if (currentSum >= sum / 2 && prefixSums.ContainsKey(currentSum - sum / 2))
            {
                Console.WriteLine($"{prefixSums[currentSum - sum / 2] + 2} {i + 1}");
                return;
            }
        }
        Console.WriteLine("0 0");
    }

    long[] ReadLongs()
    {
        return Console.ReadLine().Split().Select(long.Parse).ToArray();
    }
}