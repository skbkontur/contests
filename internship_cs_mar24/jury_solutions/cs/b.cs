using System;

namespace Summer2024
{
    public static class Program
    {
        public static void Main()
        {
            var a = Console.ReadLine();
            var n = a.Length;
            var ans = n;
            
            for (var k = 1; k <= n; k++) {
                if (n % k > 0) continue;
                var result = 0;
                for (var i = 0; i < n; ++i)
                    result += (i / k % 2) != (a[i] - '0') ? 1 : 0;
                
                ans = Math.Min(ans, Math.Min(result, n - result));
            }

            Console.WriteLine(ans);
        }
    }
}