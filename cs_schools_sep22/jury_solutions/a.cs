using System;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		public static void Main()
		{
			var s = Console.ReadLine();
			var charArray = s.ToCharArray();
			var m = string.Concat(charArray.OrderBy(c => c));
			var M = string.Concat(charArray.OrderByDescending(c => c));
			Console.WriteLine(int.Parse(M) - int.Parse(m));
		}
	}
}