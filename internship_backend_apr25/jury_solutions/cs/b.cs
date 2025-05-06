using System;
using System.Linq;


public static class Program {
    public static void Main() {
    	var _ = Console.ReadLine();
    	var houses = Console.ReadLine().Split().Select(int.Parse).ToArray();
    	var heaters = Console.ReadLine().Split().Select(int.Parse).ToArray();
    
        var minRadius = new int[houses.Length];
        for (var i = 0; i < minRadius.Length; i++)
            minRadius[i] = int.MaxValue;
    
        var j = 0;
        var k = 0;

        Array.Sort(houses);
        Array.Sort(heaters);

        while (j < houses.Length ){
            minRadius[j] = Math.Min(minRadius[j], Math.Abs(houses[j] - heaters[k]));

            if (k > 0)
                minRadius[j] = Math.Min(minRadius[j], Math.Abs(houses[j] - heaters[k - 1]));

            if (heaters[k] < houses[j] && k < heaters.Length - 1)
                k++;
            else
                j++;
        }

        Console.WriteLine(minRadius.Max());
    }
}