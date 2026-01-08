using System;
using static System.Int32;
 
public static void Main()
{
    var input = Console.ReadLine().Split();
    var oneCount = Parse(input[0]);
    var twoCount = Parse(input[1]);
    if (oneCount == twoCount)
        Console.WriteLine(oneCount + twoCount * 2);
    else if (oneCount > twoCount)
        Console.WriteLine(twoCount + 1 + twoCount * 2);
    else
        Console.WriteLine(oneCount + (oneCount + 1) * 2);
}