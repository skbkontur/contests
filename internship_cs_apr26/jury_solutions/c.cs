using System;
using System.Collections.Generic;

class Program
{
    public static void Main()
    {
        var input = Console.ReadLine().Split();
        var minRange = long.Parse(input[0]);
        var maxRange = long.Parse(input[1]);
        
        var limit = (int)Math.Sqrt(maxRange) + 1;
        var basePrimes = GetPrimesUpTo(limit);
        
        var count = 0;
        var currentOrderPrimes = new List<long> { 0 };
        var multiplier = 1L;
        
        for (var digitPosition = 1; digitPosition <= 11; digitPosition++) 
        {
            var nextOrderPrimes = new List<long>();
            
            foreach (var suffix in currentOrderPrimes) 
            {
                for (var digit = 1; digit <= 9; digit++) 
                {
                    var candidate = digit * multiplier + suffix;
                    if (candidate > maxRange)
                    {
                        continue;
                    }
                    
                    if (IsPrime(candidate, basePrimes)) 
                    {
                        nextOrderPrimes.Add(candidate);

                        if (minRange <= candidate)
                        {
                            count++;
                        }
                    }
                }
            }

            if (nextOrderPrimes.Count == 0)
            {
                break;
            }
            
            currentOrderPrimes = nextOrderPrimes;
            multiplier *= 10;
        }
        
        Console.WriteLine(count);
    }

    private static bool IsPrime(long value, List<long> basePrimes)
    {
        if (value < 2)
        {
            return false;
        }
        
        foreach (var prime in basePrimes) 
        {
            if (prime * prime > value) 
            {
                break;
            }
            
            if (value % prime == 0) 
            {
                return false;
            }
        }
        
        return true;
    }

    private static List<long> GetPrimesUpTo(long limit)
    {
        var primes = new List<long>();
        var isComposite = new bool[limit];
        
        for (long i = 2; i < isComposite.Length; i++) 
        {
            if (isComposite[i])
            {
                continue;
            }
            
            primes.Add(i);
            var start = i * i;
            if (start > limit)
            {
                continue;
            }

            for (var j = start; j < limit; j += i)
            {
                isComposite[j] = true;
            }
        }
        
        return primes;
    }
}