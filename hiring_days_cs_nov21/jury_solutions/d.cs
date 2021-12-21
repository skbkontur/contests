using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	public class Program
	{
		private class Rule
		{
			public Rule(string word, int hyphenIndex)
			{
				Word = word;
				HyphenIndex = hyphenIndex;
			}

			public string Word { get; }
			public int HyphenIndex { get; }
		}

		static void Main()
		{
			var ints = Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToArray();
			int rulesCount = ints[0];
			int width = ints[1];
			var rules = new List<Rule>(rulesCount);
			for (int i = 0; i < rulesCount; i++)
			{
				var wordParts = Console.ReadLine().Split('-');
				var rule = new Rule(string.Concat(wordParts), wordParts[0].Length);
				rules.Add(rule);
			}

			ProcessLine(Console.ReadLine(), width, rules);
		}

		private static void ProcessLine(string line, int width, List<Rule> rules)
		{
			int start = 0;

			while (start < line.Length)
			{
				if (line.Length - start <= width)
				{
					Console.WriteLine(line.Substring(start));
					break;
				}

				if (!char.IsLetter(line[start + width]))
				{
					Console.WriteLine(line.Substring(start, width));
					start += width;
					continue;
				}

				int wordStart = start + width;
				while (wordStart > start && char.IsLetter(line[wordStart - 1]))
					wordStart--;

				int wordEnd = start + width;
				while (wordEnd < line.Length && char.IsLetter(line[wordEnd]))
					wordEnd++;

				var word = line.Substring(wordStart, wordEnd - wordStart);
				var bestRule = GetBestHyphenIndex(word, start + width - wordStart, rules);
				var hyphenIndex = bestRule?.HyphenIndex ?? 0;
				Console.Write(line.Substring(start, wordStart - start + hyphenIndex));
				Console.WriteLine(bestRule != null ? "-" : "");

				start = wordStart + hyphenIndex;
			}
		}

		private static Rule GetBestHyphenIndex(string word, int lengthLeft, List<Rule> rules)
		{
			return rules
				.Where(rule => word.Equals(rule.Word, StringComparison.OrdinalIgnoreCase) && rule.HyphenIndex < lengthLeft)
				.OrderByDescending(rule => rule.HyphenIndex)
				.FirstOrDefault();
		}
	}
}