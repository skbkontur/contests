using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		static void Main()
		{
			Console.ReadLine();
			var numbers = Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToArray();

			var oppositeElements = FindOppositeElements(numbers);
			Console.WriteLine($"{oppositeElements.Item1} {oppositeElements.Item2}");
		}

		private static (int, int) FindOppositeElements(int[] numbers)
		{
			var lastIndex = new Dictionary<int, int>();
			int i = 1;
			foreach (var x in numbers)
			{
				if (lastIndex.ContainsKey(-x))
					return (i, lastIndex[-x]);

				lastIndex[x] = i;
				i++;
			}

			return (0, 0);
		}
	}
}