using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		static void Main()
		{
			var minGoodMood = ReadIntegers().ToList()[1];
			var moods = ReadIntegers()
				.Select(a => a < minGoodMood ? 0 : 1)
				.ToList();
			var prefixSums = new List<int> {0};
			for (int i = 0; i < moods.Count; i++)
				prefixSums.Add(prefixSums[i] + moods[i]);

			int m = int.Parse(Console.ReadLine());
			int queueStart = 0;

			for (int i = 0; i < m; i++)
			{
				var integers = ReadIntegers().ToList();
				var queryType = integers[0];

				switch (queryType)
				{
					case 1:
					{
						var mood = integers[1] < minGoodMood ? 0 : 1;
						moods.Add(mood);
						prefixSums.Add(prefixSums[prefixSums.Count - 1] + mood);
						break;
					}
					case 2:
					{
						queueStart++;
						break;
					}
					case 3:
					{
						var indexFromQueueStart = integers[1];
						Console.WriteLine(prefixSums[indexFromQueueStart + queueStart] - prefixSums[queueStart]);
						break;
					}
				}
			}
		}

		private static IEnumerable<int> ReadIntegers()
		{
			return Console.ReadLine()
				.Split()
				.Select(int.Parse);
		}
	}
}