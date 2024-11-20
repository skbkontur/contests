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
        var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var size = input[0];
        var border = input[1];
        var changes = input[2];
        var elements = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var left = 0;
        var right = 0;
        var currentChanges = 0;
        var maxLength = 0;
        while (right < size)
        {
            if (elements[right] < border)
            {
                right++;
                continue;
            }

            if (currentChanges < changes)
            {
                currentChanges++;
                right++;
                continue;
            }

            maxLength = Math.Max(maxLength, right - left);

            while (elements[left] < border)
                left++;
        
        
            left++;
            right++;
        }

        maxLength = Math.Max(maxLength, right - left);

        Console.WriteLine(maxLength);
    }
}