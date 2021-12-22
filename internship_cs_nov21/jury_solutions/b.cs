using System;
using System.Linq;

namespace ConsoleApp
{
	static class Program
	{
		static void Main()
		{
			var password = Console.ReadLine();
			int letters = password.Count(char.IsLetter);
			int digits = password.Count(char.IsDigit);

			if (password.Length >= 7
			    && letters >= 2
			    && digits >= 2
			    && password.Length - letters - digits >= 1)
				Console.WriteLine("GOOD");
			else
				Console.WriteLine("BAD");
		}
	}
}