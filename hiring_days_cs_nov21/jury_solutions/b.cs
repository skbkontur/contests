using System;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		static void Main()
		{
			var ints = Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToList();
			int n = ints[0];
			int m = ints[1];
			var map = new string[n];
			for (int i = 0; i < n; i++)
			{
				map[i] = Console.ReadLine();
			}

			int result = 0;

			for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
			{
				if (map[i][j] == '.')
				{
					if (i + 1 < n && map[i + 1][j] == '.')
						result++;
					if (j + 1 < m && map[i][j + 1] == '.')
						result++;
				}
			}

			Console.WriteLine(result);
		}
	}
}