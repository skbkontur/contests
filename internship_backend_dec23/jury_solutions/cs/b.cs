using System;
using System.Linq;
 
public static void Main()
{
    var n = int.Parse(Console.ReadLine());
    var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

    var sumLeft = 0;
    var sumRight = 0;
    var left = 0;

    var right = n - 1;
    while (left <= right)
    {
        if (sumLeft <= sumRight)
        {
            sumLeft += numbers[left];
            left++;
        }
        else
        {
            sumRight += numbers[right];
            right--;
        }
    }

    if (sumRight == sumLeft)
    {
        var leftPart = string.Join("+", numbers.Take(left));
        var rightPart = string.Join("+", numbers.Skip(left));
        Console.WriteLine($"{leftPart}={rightPart}");
    }
    else
        Console.WriteLine(-1);
}