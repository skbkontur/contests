using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp2
{
	public class Program
	{
		private static void Solve()
		{
			int n = int.Parse(Console.ReadLine());
			var x = new int[n];
			var y = new int[n];
			var points = new HashSet<(int, int)>();
			for (int i = 0; i < n; i++)
			{
				var ints = ReadInts();
				x[i] = ints[0];
				y[i] = ints[1];
				points.Add((x[i], y[i]));
			}

			long max = 0;
			for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
			{
				if (x[i] == x[j] || y[i] == y[j])
					continue;
				if (points.Contains((x[i], y[j])) && points.Contains((x[j], y[i])))
				{
					var s = (long) Math.Abs(x[i] - x[j]) * Math.Abs(y[i] - y[j]);
					if (s > max)
						max = s;
				}
			}

			Console.WriteLine(max);
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