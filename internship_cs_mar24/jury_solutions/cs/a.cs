using System;
using System.Linq;

namespace Summer2024
{
    public static class Program
    {
        public static void Main()
        {
            var nt = ReadLongArray();
            var n = nt[0];
            var t = nt[1];

            var a = ReadLongArray();
            var b = ReadLongArray();

            var result = a.Sum() <= t && t <= b.Sum();
            Console.WriteLine(result ? "Yes" : "No");
        }
        
        public static long[] ReadLongArray() => Console.ReadLine().Split().Select(long.Parse).ToArray();
    }
}