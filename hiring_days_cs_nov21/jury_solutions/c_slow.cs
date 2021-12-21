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
			var moods = new Queue<int>
			(
				ReadIntegers().Select(a => a < minGoodMood ? 0 : 1)
			);

			int m = int.Parse(Console.ReadLine());

			for (int i = 0; i < m; i++)
			{
				var integers = ReadIntegers().ToList();
				var queryType = integers[0];

				switch (queryType)
				{
					case 1:
					{
						var mood = integers[1] < minGoodMood ? 0 : 1;
						moods.Enqueue(mood);
						break;
					}
					case 2:
					{
						moods.Dequeue();
						break;
					}
					case 3:
					{
						var indexFromQueueStart = integers[1];
						var result = moods
							.Take(indexFromQueueStart)
							.Sum();
						Console.WriteLine(result);
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