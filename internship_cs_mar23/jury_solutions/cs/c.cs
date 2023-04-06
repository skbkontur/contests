using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp2
{
	public class Program
	{
		private static void Solve()
		{
			var ints = ReadInts();
			var n = ints[0];
			var k = (long) ints[1];
			var a = ReadInts();
			var isZero = a.Select(e => e == 0 ? 1 : 0).ToArray();

			int finish = -1;
			int zeroesCount = 0;
			long sum = 0;
			long ans = 0;
			for (int start = 0; start < n; start++)
			{
				while (finish < n - 1 && a[finish + 1] + sum <= k && isZero[finish + 1] + zeroesCount <= 1)
				{
					sum += a[finish + 1];
					zeroesCount += isZero[finish + 1];
					finish++;
				}

				ans += finish - start + 1;
				sum -= a[start];
				zeroesCount -= isZero[start];

				if (finish < start)
				{
					sum += a[finish + 1];
					zeroesCount += isZero[finish + 1];
					finish++;
				}
			}

			Console.WriteLine(ans);
		}


		public static void Main()
		{
			var T = new Thread(Solve, 10000000);
			T.Start();
		}

		private static int[] ReadInts()
		{
			return Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToArray();
		}
	}
}