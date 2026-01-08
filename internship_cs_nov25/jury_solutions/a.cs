using System;

public class Program
{
	public static void Main(string[] args)
	{
		var input = Console.ReadLine();
		var n = 0;
		foreach (var ch in input)
			if (ch == 'M')
				n++;

		var res = (-1 + Math.Sqrt(1 + 8 * n)) / 2;
		Console.WriteLine((int)Math.Floor(res));
	}
}
