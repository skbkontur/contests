using System;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		static void Main()
		{
			var ints = Console.ReadLine().Split().Select(int.Parse).ToArray();

			int maxCount = Math.Max(ints[0], ints[1]);
			int minCount = Math.Min(ints[0], ints[1]);

			for (int i = 1; ; i++)
			{
				if (i * i / 2 > minCount || (i * i + 1) / 2 > maxCount)
				{
					Console.WriteLine(i - 1);
					return;
				}
			}
		}
	}
}