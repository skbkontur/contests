using System;
using System.IO;
using System.Linq;

public class Program : IDisposable
{
	const int MAXN = 200000;
	const int SL = 10;
	const string NO_SOL = "No solution";
	StreamReader inStream;
	StreamWriter outStream;

	public Program()
	{
		inStream = new StreamReader(Console.OpenStandardInput());
		outStream = new StreamWriter(Console.OpenStandardOutput());
	}

	private void Solve()
	{
		string[][] g = new string[5][];
		int sum = 0;
		string st = CheckFormat(inStream.ReadLine());
		string fin = CheckFormat(inStream.ReadLine());
		for (int i = 0; i < g.Length; ++i)
		{
			int k = int.Parse(inStream.ReadLine());
			if (k < 0 || k > MAXN) throw new ArgumentOutOfRangeException();
			sum += k;
			g[i] = new string[k];
			for (int j = 0; j < g[i].Length; ++j)
			{
				g[i][j] = CheckFormat(inStream.ReadLine());
			}
		}
		if (sum < 0 || sum > MAXN) throw new ArgumentOutOfRangeException();
		g[0] = new string[] { st };
		g[4] = new string[] { fin };

		for (int i = 1; i <= 3; ++i)
		{
			if (g[i].Length == 0)
			{
				outStream.WriteLine(NO_SOL);
				return;
			}
		}

		foreach (var sa in g)
		{
			Array.Sort(sa);
		}

		string[] ans = new string[5];
		int ansv = int.MaxValue;

		string s0 = g[0][0];
		string s5 = g[4][0];
		string s1 = g[1][GetNearest(g[1], s0)];
		string s4 = g[3][GetNearest(g[3], s5)];
		foreach (string s in g[2])
		{
			string s2 = g[1][GetNearest(g[1], s)];
			string s3 = g[3][GetNearest(g[3], s)];

			if (F(s0, s1) + F(s1, s) < F(s0, s2) + F(s2, s))
			{
				s2 = s1;
			}
			if (F(s, s4) + F(s4, s5) < F(s, s3) + F(s3, s5))
			{
				s3 = s4;
			}
			int tansv = F(s0, s2) + F(s2, s) + F(s, s3) + F(s3, s5);
			if (ansv > tansv)
			{
				ansv = tansv;
				ans[0] = s0;
				ans[1] = s2;
				ans[2] = s;
				ans[3] = s3;
				ans[4] = s5;
			}
		}

		outStream.WriteLine($"{ansv} 5");
		foreach (string s in ans)
		{
			outStream.WriteLine(s);
		}
	}

	int Lcp(string a, string b)
	{
		for (int i = 0; i < SL; ++i)
		{
			if (a[i] != b[i])
			{
				return i;
			}
		}
		return SL;
	}

	int F(string a, string b)
	{
		return 10 - Lcp(a, b);
	}

	int GetNearest(string[] a, string v)
	{
		int l = 0;
		int r = a.Length;
		while (r - l > 1)
		{
			int m = (l + r) / 2;
			if (string.Compare(a[m], v) <= 0)
			{
				l = m;
			}
			else
			{
				r = m;
			}
		}
		int ans = l;
		if (r < a.Length && F(a[l], v) > F(a[r], v))
		{
			ans = r;
		}
		return ans;
	}

	string CheckFormat(string s)
	{
		if (s.Length != SL) throw new ArgumentException();
		foreach (char c in s)
		{
			if (!char.IsDigit(c)) throw new ArgumentException();
		}
		return s;
	}

	public void Run()
	{
		try
		{
			Solve();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			Environment.Exit(-1);
		}
		finally
		{
			outStream.Flush();
			outStream.Close();
			inStream.Close();
		}
	}

	public void Dispose()
	{
		outStream?.Dispose();
		inStream?.Dispose();
	}

	public static void Main(string[] args)
	{
		using (var program = new Program())
		{
			program.Run();
		}
	}
}
