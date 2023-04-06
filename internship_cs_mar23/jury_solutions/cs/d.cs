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
			var ints = ReadInts();
			var n = ints[0];
			var possible = new HashSet<int>();
			possible.Add(0);

			for (int i = 0; i < n; i++)
			{
				var a = ReadInts();
				var newPossible = new HashSet<int>();
				for (int j = 0; j < 6; j++)
					foreach (var e in possible)
						newPossible.Add(e + a[j]);

				possible = newPossible;
			}

			Console.WriteLine(possible.Count);
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