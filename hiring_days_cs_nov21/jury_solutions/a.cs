using System;
using System.Globalization;

namespace ConsoleApp
{
	class Program
	{
		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var result = TimeSpan.Zero;
			for (int i = 0; i < n; i++)
			{
				var s = Console.ReadLine();
				var time = TimeSpan.ParseExact(s, "mm\\:ss", CultureInfo.InvariantCulture);
				result += time;
			}

			Console.WriteLine(result);
		}
	}
}