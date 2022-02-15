using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		class Jelly
		{
			private int X { get; }
			private int Y { get; }
			private int R { get; }
			public int Z { get; }
			public int Index { get; }

			public Jelly(int x, int y, int z, int r, int index)
			{
				X = x;
				Y = y;
				Z = z;
				R = r;
				Index = index;
			}

			public bool Intersects(Jelly jelly)
			{
				return (X - jelly.X) * (X - jelly.X)
				       + (Y - jelly.Y) * (Y - jelly.Y)
				       <= (R + jelly.R) * (R + jelly.R);
			}
		}

		static void Main()
		{
			var n = int.Parse(Console.ReadLine());
			var jellies = new List<Jelly>(n);
			for (int i = 0; i < n; i++)
			{
				var ints = Console.ReadLine()
					.Split()
					.Select(int.Parse)
					.ToArray();
				var jelly = new Jelly(ints[0], ints[1], ints[2], ints[3], i + 1);
				jellies.Add(jelly);
			}

			jellies.Sort((x, y) => x.Z.CompareTo(y.Z));

			var answer = new HashSet<Jelly>();
			var first = jellies.FindIndex(x => x.Index == 1);
			answer.Add(jellies[first]);

			for (int i = first + 1; i < n; i++)
			{
				var nextJelly = jellies[i];
				foreach (var jelly in answer)
				{
					if (jelly.Intersects(nextJelly))
					{
						answer.Add(nextJelly);
						break;
					}
				}
			}

			Console.WriteLine(answer.Count);
			Console.WriteLine(string.Join(" ", answer.Select(x => x.Index).OrderBy(x => x)));
		}
	}
}