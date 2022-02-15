using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		static void Main()
		{
			var k = int.Parse(Console.ReadLine());
			var s = Console.ReadLine();
			var t = Console.ReadLine();

			var sCounts = Enumerable.Range(0, 26).ToDictionary(i => (char) ('a' + i), i => 0);
			var tCounts = Enumerable.Range(0, 26).ToDictionary(i => (char) ('a' + i), i => 0);

			foreach (var c in t)
				tCounts[c]++;

			for (int i = 0; i < k; i++)
				sCounts[s[i]]++;

			if (Check(sCounts, tCounts))
			{
				Console.WriteLine("YES");
				Console.WriteLine(s.Substring(0, k));
				return;
			}

			for (int i = k; i < s.Length; i++)
			{
				sCounts[s[i - k]]--;
				sCounts[s[i]]++;
				if (Check(sCounts, tCounts))
				{
					Console.WriteLine("YES");
					Console.WriteLine(s.Substring(i - k + 1, k));
					return;
				}
			}

			Console.WriteLine("NO");
		}

		private static bool Check(Dictionary<char, int> sCounts, Dictionary<char, int> tCounts)
		{
			foreach (var kvp in sCounts)
			{
				if (!tCounts.ContainsKey(kvp.Key) || tCounts[kvp.Key] < kvp.Value)
					return false;
			}

			return true;
		}
	}
}