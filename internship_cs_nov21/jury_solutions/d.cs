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
			var cycle1 = ReadInts();
			var cycle2 = ReadInts();
			var road = GetRoads(cycle1)
				.Intersect(GetRoads(cycle2))
				.First();
			Console.WriteLine(road.Item1 + " " + road.Item2);
		}

		private static IEnumerable<(int, int)> GetRoads(int[] cycle)
		{
			return Enumerable.Range(0, cycle.Length)
				.Select(i =>
					(
						Math.Min(cycle[i], cycle[(i + 1) % cycle.Length]),
						Math.Max(cycle[i], cycle[(i + 1) % cycle.Length])
					)
				);
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