using System;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		static void Main()
		{
			var answer = "rgby".ToDictionary(ch => ch, ch => 0);
			for (int i = 1; i < 10; i++)
			for (int j = 1; j < 10; j++)
			{
				var p = i * j;
				if (p % 2 == 0)
					answer['r']++;
				else if (p % 3 == 0)
					answer['g']++;
				else if (p % 5 == 0)
					answer['b']++;
				else
					answer['y']++;
			}

			var color = Console.ReadLine()[0];
			Console.WriteLine(answer[color]);
		}
	}
}