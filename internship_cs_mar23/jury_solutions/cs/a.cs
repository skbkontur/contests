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
			var a = ReadInts();
			var elems = new List<(int, int)>();
			for (int i = 0; i < n; i++)
			{
				elems.Add((a[i], i));
			}

			elems.Sort();
			Console.WriteLine($"{elems.Last().Item2 + 1} {elems.First().Item2 + 1}");
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