using System;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		public static void Main()
		{
			var ints = Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToArray();
			int n = ints[0];
			int m = ints[1];
			var map = new string[n];
			for (int i = 0; i < n; i++)
				map[i] = Console.ReadLine();

			var maxPrefix = new int[n + 1][];
			for (int i = 0; i <= n; i++)
				maxPrefix[i] = new int[m + 1];

			for (int i = 1; i < n; i++)
			for (int j = 1; j < m; j++)
			{
				if (map[i][j] == '*')
					maxPrefix[i][j] = 0;
				else
					maxPrefix[i][j] = maxPrefix[i][j - 1] + 1;
			}

			int max = 0;
			for (int i = 1; i + 1 < n; i++)
			for (int j = 1; j + 1 < m; j++)
				if (map[i][j] == '.')
				{
					int width = n * m;
					for (int h = i; h > 0; h--)
					{
						width = Math.Min(maxPrefix[h][j], width);
						if (width * (i - h + 1) > max)
							max = width * (i - h + 1);
					}
				}

			Console.WriteLine(max);
		}
	}
}