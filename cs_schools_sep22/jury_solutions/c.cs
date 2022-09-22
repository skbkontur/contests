using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		public static void Main()
		{
			int n = int.Parse(Console.ReadLine());
			var table = new List<(string, int)>();
			for (int i = 0; i < n; i++)
			{
				var tokens = Console.ReadLine().Split();
				table.Add((tokens[0], int.Parse(tokens[1])));
			}

			var teams = Console.ReadLine().Split('-');

			int res1 = Solve(table.ToArray(), teams[0], 3, teams[1], 0);
			int res2 = Solve(table.ToArray(), teams[0], 1, teams[1], 1);
			int res3 = Solve(table.ToArray(), teams[0], 0, teams[1], 3);
			Console.WriteLine(res1 + " " + res2 + " " + res3);
		}

		private static int Solve((string, int)[] table, string team1, int p1, string team2, int p2)
		{
			int index1 = Enumerable.Range(0, table.Length).First(i => table[i].Item1 == team1);
			table[index1] = (table[index1].Item1, table[index1].Item2 + p1);

			int index2 = Enumerable.Range(0, table.Length).First(i => table[i].Item1 == team2);
			table[index2] = (table[index2].Item1, table[index2].Item2 + p2);

			table = table.OrderByDescending(item => item.Item2).ThenBy(item => item.Item1).ToArray();
			return Enumerable.Range(0, table.Length).First(i => table[i].Item1 == team1) + 1;
		}
	}
}