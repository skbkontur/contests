using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
	class Program
	{
		public static void Main()
		{
			int n = int.Parse(Console.ReadLine());
			var builder = new StringBuilder();
			var data = new Dictionary<string, List<int>>();
			for (int i = 0; i < n; i++)
			{
				var s = Console.ReadLine();
				int index = Enumerable.Range(0, s.Length).First(j => char.IsDigit(s[j]));
				var name = s.Substring(0, index);
				var number = int.Parse(s.Substring(index));
				if (!data.ContainsKey(name))
					data[name] = new List<int>();
				data[name].Add(number);
			}

			var dataQueues = data.ToDictionary
			(
				kvp => kvp.Key,
				kvp => new Queue<int>(kvp.Value.OrderBy(x => x))
			);

			var firstFree = new Dictionary<string, int>();
			int q = int.Parse(Console.ReadLine());
			for (int i = 0; i < q; i++)
			{
				var query = Console.ReadLine();
				var candidate = firstFree.ContainsKey(query) ? firstFree[query] : 1;

				if (dataQueues.ContainsKey(query))
				{
					var queue = dataQueues[query];
					while (queue.Count > 0 && queue.Peek() == candidate)
					{
						candidate++;
						queue.Dequeue();
					}
				}

				builder.AppendLine(candidate.ToString());
				firstFree[query] = candidate + 1;
			}

			Console.Write(builder);
		}
	}
}