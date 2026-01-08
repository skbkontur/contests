using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	private class Temple
	{
		public long Cats;
		public long Cost;
		public int Index;
	}

	public static void Main(string[] args)
	{
		int n = int.Parse(Console.ReadLine());
		var temples = new List<Temple>();

		for (int i = 0; i < n; i++)
		{
			var longs = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
			temples.Add(
				new Temple
				{
					Cats = longs[0],
					Cost = longs[1] == -1 ? Int32.MaxValue : longs[1],
					Index = i + 1
				}
			);
		}

		temples = temples
			.OrderByDescending(x => x.Cats)
			.ThenByDescending(x => x.Cost)
			.ToList();

		var prefixSums = new long[n];
		prefixSums[0] = temples[0].Cats;
		for (int i = 1; i < n; i++)
			prefixSums[i] = prefixSums[i - 1] + temples[i].Cats;

		long optimal = long.MaxValue;
		int optimalIndex = -1;
		long optimalCats = -1;

		if (n == 1 || (temples[0].Cats > temples[1].Cats && temples[0].Cost != Int32.MaxValue))
		{
			optimal = temples[0].Cost;
			optimalIndex = 0;
			optimalCats = temples[0].Cats;
		}

		for (int k = 1; k < n; k++)
			if (temples[k].Cost != Int32.MaxValue)
			{
				long catsNeed = Int32.MaxValue;
				int l = -1;
				int r = k;
				while (l < r - 1)
				{
					int i = (l + r) / 2;
					var x = MyDiv(prefixSums[i] + temples[k].Cats, i + 2) + 1;
					if (x > temples[i + 1].Cats)
					{
						r = i;
						catsNeed = x;
					}
					else
					{
						l = i;
					}
				}

				if (catsNeed - temples[k].Cats + temples[k].Cost < optimal)
				{
					optimal = catsNeed - temples[k].Cats + temples[k].Cost;
					optimalIndex = k;
					optimalCats = catsNeed;
				}
			}

		for (int i = 0; i < n && temples[optimalIndex].Cats < optimalCats; i++)
		{
			long delta = temples[i].Cats >= optimalCats ?
				temples[i].Cats - optimalCats + 1 :
				1;
			temples[optimalIndex].Cats += delta;
			temples[i].Cats -= delta;
		}

		Console.WriteLine(optimal);
		Console.WriteLine(temples[optimalIndex].Index);
		Console.WriteLine(string.Join(" ", temples.OrderBy(x => x.Index).Select(x => x.Cats)));
	}

	private static long MyDiv(long u, long v)
	{
		return u / v + (u % v > 1 ? 1 : 0);
	}
}