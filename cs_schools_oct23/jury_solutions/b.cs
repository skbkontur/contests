using System;
using System.Linq;

class Program
{
	public static void Main()
	{
		var _ = Console.ReadLine();
		var sequence = Console.ReadLine();
		var totalDisabledCount = sequence.Count(e => e.Equals('0'));

		var answer = totalDisabledCount;
		var enabledCount = 0;
		var disabledCount = 0;

		foreach (var isEnable in sequence.Select(e => e.Equals('1')))
		{
			if (isEnable)
				enabledCount++;
			else
				disabledCount++;
			answer = Math.Min(answer, enabledCount + totalDisabledCount - disabledCount);
		}

		Console.WriteLine(answer);
	}
}