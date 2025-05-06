using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


class Program
{
	static void Main()
	{
		Solve();
	}

	static void Solve()
	{
		var nmk = Console.ReadLine().Split(' ');
		var n = int.Parse(nmk[0]);
		var m = int.Parse(nmk[1]);
		var k = long.Parse(nmk[2]);
		k += 1;
		var graph = new List<List<Tuple<int, long>>>(n);
		for (var i = 0; i < n; i++) graph.Add(new List<Tuple<int, long>>());
		long mx = 0;
		for (var i = 0; i < m; i++)
		{
			var uvc = Console.ReadLine().Split(' ');
			var u = int.Parse(uvc[0]) - 1;
			var v = int.Parse(uvc[1]) - 1;
			var c = long.Parse(uvc[2]);
			graph[u].Add(Tuple.Create(v, c));
			mx = Math.Max(mx, c);
		}

		long l = 1, r = mx + 2;
		long ans = -1;

		while (l <= r)
		{
			var mid = (l + r) / 2;

			if (Check(graph, k, mid))
			{
				r = mid - 1;
				ans = mid;
			}
			else
				l = mid + 1;
		}
		
		Console.WriteLine(ans);
	}
	
	static bool CheckGraph(List<List<Tuple<int, long>>> graph, long border, List<int> order)
	{
		int[] colors = new int[graph.Count];
		var res = false;
		int color = 1;
		for (int i = 0; i < graph.Count; i++)
		{
			if (colors[i] != 0) continue;
			res = res || TopSortNoRec(graph, i, border, colors, order, color);
			color++;
		}
			
		return res;
	}

	static bool TopSortNoRec(List<List<Tuple<int, long>>> graph, int v, long border, int[] colors, List<int> order, int curColor)
	{
		List<Tuple<bool, int>> dfs = new List<Tuple<bool, int>>();
		dfs.Add(new Tuple<bool, int>(false, v));
		HashSet<int> visited = new HashSet<int>();
		visited.Add(v);
		while (dfs.Count > 0)
		{
			var node = dfs[dfs.Count - 1];
			dfs.RemoveAt(dfs.Count - 1);
			if (node.Item1)
			{
				order.Add(node.Item2);
				colors[node.Item2] = int.MinValue;
				continue;
			}
			
			if (colors[node.Item2] != 0)
				continue;
			
			colors[node.Item2] = curColor;
			dfs.Add(new Tuple<bool, int>(true, node.Item2));
			
			foreach (var u in graph[node.Item2])
			{
				if (u.Item2 > border)
					continue;

				if (colors[u.Item1] == curColor)
					return true;

				if (colors[u.Item1] == 0 && !visited.Contains(u.Item1))
				{
					dfs.Add(new Tuple<bool, int>(false, u.Item1));
					visited.Add(u.Item1);
				}
			}
		}


		return false;
	}


	static bool CheckKLenPath(List<List<Tuple<int, long>>> graph, long k, long border, List<int> order)
	{
		if (order.Count == 0) return false;
		order.Reverse();

		long[] dp = new long[graph.Count];
		long max = 0;

		foreach (var v in order)
		{
			if (dp[v] == 0)
			{
				dp[v] = 1;
				max = Math.Max(max, 1);
			}

			foreach (var u in graph[v])
			{
				if (u.Item2 > border)
					continue;
				dp[u.Item1] = Math.Max(dp[u.Item1], dp[v] + 1);
				max = Math.Max(max, dp[u.Item1]);
			}
		}

		return max >= k;
	}

	static bool Check(List<List<Tuple<int, long>>> graph, long k, long border)
	{
		var order = new List<int>(graph.Count);
		return CheckGraph(graph, border, order) || CheckKLenPath(graph, k, border, order);
	}
}
