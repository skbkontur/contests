using System;

public class Program
{
	public static void Main(string[] args)
	{
		var n = int.Parse(Console.ReadLine());
		var strings = new string[n];
		for (int i = 0; i < n; i++)
			strings[i] = Console.ReadLine();
		Array.Sort(strings, (s1, s2) => -(s1 + s2).CompareTo(s2 + s1));
		Console.WriteLine(string.Join("", strings));
	}
}
