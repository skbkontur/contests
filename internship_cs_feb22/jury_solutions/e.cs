using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
	class Program
	{
		private static int[] a;

		private class MyComparer : IComparer<(int, int)>
		{
			public int Compare((int, int) pair1, (int, int) pair2)
			{
				var (left1, right1) = pair1;
				var (left2, right2) = pair2;
				if (a[right1] - a[left1] != a[right2] - a[left2])
					return (a[right1] - a[left1]).CompareTo(a[right2] - a[left2]);
				if (a[right1] + a[left1] != a[right2] + a[left2])
					return -(a[right1] + a[left1]).CompareTo(a[right2] + a[left2]);
				return right1.CompareTo(right2);
			}
		}

		static void Main()
		{
			Console.ReadLine();
			a = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();
			var set = new SortedSet<(int, int)>(new MyComparer());
			for (int i = 0; i < a.Length - 1; i++)
				set.Add((i, i + 1));

			var list = new LinkedList<int>();
			var map = new Dictionary<int, LinkedListNode<int>>();
			for (int i = 0; i < a.Length; i++)
			{
				list.AddLast(i);
				map[i] = list.Last;
			}

			var first = new List<int>();
			var second = new List<int>();
			while (set.Count > 0)
			{
				var (u, v) = set.Min;
				set.Remove(set.Min);
				first.Add(a[u]);
				second.Add(a[v]);

				var left = map[u].Previous;
				if (left != null)
					set.Remove((left.Value, u));

				var right = map[v].Next;
				if (right != null)
					set.Remove((v, right.Value));

				if (left != null && right != null)
					set.Add((left.Value, right.Value));

				list.Remove(map[u]);
				list.Remove(map[v]);
			}

			Console.WriteLine(string.Join(" ", second));
			Console.WriteLine(string.Join(" ", first));
		}
	}
}