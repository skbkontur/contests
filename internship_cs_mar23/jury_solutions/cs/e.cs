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
			var tokens = Console.ReadLine()
				.Split();
			var n = int.Parse(tokens[0]);
			var m = int.Parse(tokens[1]);

			var a = new int[n][];

			var list = new List<(int, int, int)>();

			for (int i = 0; i < n; i++)
			{
				a[i] = Console.ReadLine()
					.Select(s => s - '0')
					.ToArray();

				for (int j = 0; j < m; j++)
					list.Add((a[i][j], i, j));
			}

			list.Sort((x, y) => y.Item1.CompareTo(x.Item1));
			if (list[0].Item2 != list[1].Item2 &&
			    list[0].Item3 != list[1].Item3
			)
			{
				int max1 = Check(a, list[0].Item2, list[1].Item3);
				int max2 = Check(a, list[1].Item2, list[0].Item3);
				if (max1 < max2)
					Console.WriteLine("{0} {1}", list[0].Item2 + 1, list[1].Item3 + 1);
				else
					Console.WriteLine("{0} {1}", list[1].Item2 + 1, list[0].Item3 + 1);
				return;
			}

			if (list[0].Item2 == list[1].Item2)
			{
				for (int i = 2; i < n * m; i++)
					if (list[i].Item2 != list[0].Item2)
					{
						Console.WriteLine("{0} {1}", list[0].Item2 + 1, list[i].Item3 + 1);
						return;
					}
			}

			if (list[0].Item3 == list[1].Item3)
			{
				for (int i = 2; i < n * m; i++)
					if (list[i].Item3 != list[0].Item3)
					{
						Console.WriteLine("{0} {1}", list[i].Item2 + 1, list[0].Item3 + 1);
						return;
					}
			}
		}

		private static int Check(int[][] a, int row, int column)
		{
			int max = -1;
			for (int i = 0; i < a.Length; i++)
			for (int j = 0; j < a[0].Length; j++)
				if (i != row && j != column && a[i][j] > max)
					max = a[i][j];

			return max;
		}

		public static void Main()
		{
			var T = new Thread(Solve, 10000000);
			T.Start();
		}
	}
}