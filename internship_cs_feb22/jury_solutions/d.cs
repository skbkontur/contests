using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		public class LongNumber
		{
			private List<int> digits;
			private int shift;

			public LongNumber(string s)
			{
				shift = 0;
				digits = s.Select(c => c - '0').Reverse().ToList();
			}

			public int Length => digits.Count - shift;
			public int LastDigit => digits[shift];

			public static LongNumber operator +(LongNumber a, int x)
			{
				a.digits[a.shift] += x;
				int i = a.shift;
				while (a.digits[i] >= 10)
				{
					if (i == a.digits.Count - 1)
						a.digits.Add(a.digits[i] / 10);
					else
						a.digits[i + 1] += a.digits[i] / 10;
					a.digits[i] %= 10;
					i++;
				}

				return a;
			}

			public void DivideByTen()
			{
				shift++;
			}
		}

		static void Main()
		{
			int t = int.Parse(Console.ReadLine());
			for (int i = 0; i < t; i++)
			{
				var s = Console.ReadLine();
				var number = new LongNumber(s);
				var used = new HashSet<int>();
				while (number.Length > 1 || !used.Contains(number.LastDigit))
				{
					if (number.Length == 1)
						used.Add(number.LastDigit);
					int lastDigit = number.LastDigit;
					while (number.LastDigit != 0)
						number += lastDigit;
					number.DivideByTen();
				}

				Console.WriteLine(used.Min());
			}
		}
	}
}