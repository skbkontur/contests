using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	private static bool _answerYES = true;
	private const string Chars = "@!$&%#^*()";
	private static readonly Dictionary<char, int> CharToIndex = [];

	public static void Main()
	{
		for (var i = 0; i < 10; i++)
		{
			CharToIndex[Chars[i]] = i;
		}
		var n = int.Parse(Console.ReadLine());

		var biggerChars = new Dictionary<char, HashSet<char>>();
		var cantBeNull = new bool[10];
		foreach (var @char in Chars)
		{
			biggerChars[@char] = [];
		}

		var previous = "";
		for (var i = 0; i < n; i++)
		{
			var current = Console.ReadLine();
			if (current.Length > 1)
				cantBeNull[CharToIndex[current[0]]] = true;
			if (current.Length == previous.Length)
			{
				var position = 0;
				while (position < previous.Length && previous[position] == current[position])
				{
					position++;
				}

				if (position == previous.Length)
				{
					_answerYES = false;
				}
				else
				{
					biggerChars[previous[position]].Add(current[position]);
					cantBeNull[CharToIndex[current[position]]] = true;
				}
			}
			else if (current.Length <= previous.Length)
			{
				_answerYES = false;
			}
			previous = current;
		}

		if (cantBeNull.All(can => can))
		{
			_answerYES = false;
		}

		var allBiggerCharsCount = new Dictionary<char, int>();
		foreach (var @char in Chars)
		{
			allBiggerCharsCount[@char] = Bfs(biggerChars, @char);
		}

		if (_answerYES)
		{
			var bit = allBiggerCharsCount
				.OrderBy(pair => cantBeNull[CharToIndex[pair.Key]])
				.ThenByDescending(pair => pair.Value)
				.Select(pair => pair.Key)
				.ToList();

			var res = bit.Select((@char, index) => new Tuple<char, int>(@char, index))
				.OrderBy(pair => CharToIndex[pair.Item1])
				.Select(pair => pair.Item2)
				.ToList();

			Console.WriteLine("YES");
			Console.WriteLine(string.Join(" ", res));
		}
		else
		{
			Console.WriteLine("NO");
		}
	}

	private static int Bfs(Dictionary<char, HashSet<char>> biggerChars, char cur)
	{
		var visited = new HashSet<char>();
		var stack = new Stack<char>(biggerChars[cur]);
		while (stack.Count != 0)
		{
			var l = stack.Pop();
			if (l == cur)
			{
				_answerYES = false;
			}
			if (!visited.Add(l))
			{
				continue;
			}

			foreach (var @char in biggerChars[l])
				stack.Push(@char);
		}
		return visited.Count;
	}
}