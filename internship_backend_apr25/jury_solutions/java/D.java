import java.io.*;
import java.util.*;

public class D {
	public static void main(String[] args) throws Exception {
		new D().run();
	}

	StreamTokenizer in;

	int nextInt() throws Exception {
		in.nextToken();
		return (int)in.nval;
	}

	long nextLong() throws Exception {
		in.nextToken();
		return (long)in.nval;
	}

	ArrayList<Integer>[] graph;
	ArrayList<Long>[] graphW;

	void run() throws Exception {
		in = new StreamTokenizer(new BufferedReader(new InputStreamReader(System.in)));
		int n = nextInt();
		int m = nextInt();
		long kk = nextLong() + 1;
		int k = (int)Math.min(kk, n + 1);	
		if (k == 1) {
			System.out.println(0);
			return;
		}

		graph = new ArrayList[n];
		graphW = new ArrayList[n];
		for (int i = 0; i < n; i++) {
			graph[i] = new ArrayList<Integer>();
			graphW[i] = new ArrayList<Long>();
		}

		long mx = 0;
		for (int i = 0; i < m; i++) {
			int u = nextInt() - 1;
			int v = nextInt() - 1;
			long c = nextLong();
			graph[u].add(v);
			graphW[u].add(c);
			mx = Math.max(mx, c);
		}

		long l = 1, r = mx + 2;
		long ans = -1;

		while (l <= r) {
			long mid = (l + r) / 2;

			if (check(n, k, mid)) {
				r = mid - 1;
				ans = mid;
			} else {
				l = mid + 1;
			}
		}

		System.out.println(ans);
	}
	
	boolean check(int n, int k, long border) {
		ArrayList<Integer> order = new ArrayList<Integer>();
		return checkCycle(n, border, order) || checkKLenPath(n, k, border, order);
	}

	boolean checkCycle(int n, long border, ArrayList<Integer> order) {
		int[] colors = new int[n];
		int curColor = 1;
		for (int start = 0; start < n; start++) {
			if (colors[start] != 0) 
				continue;
			if (topSort(start, border, colors, order, curColor))
				return true;
			curColor++;
		}

		return false;
	}

	boolean topSort(int v, long border, int[] colors, ArrayList<Integer> order, int curColor) {
		colors[v] = -1;
		for (int i = 0; i < graph[v].size(); i++) {
			int u = graph[v].get(i);
			if (graphW[v].get(i) > border)
				continue;

			if (colors[u] == -1)
				return true;

			if (colors[u] == 0 && topSort(u, border, colors, order, curColor))
				return true;
		}

		colors[v] = curColor;
		order.add(v);
		return false;
	}

	boolean checkKLenPath(int n, int k, long border, List<Integer> order) {
		int[] dp = new int[n];

		for (int v : order) {
			dp[v] = 1;

			for (int i = 0; i < graph[v].size(); i++) {
				int u = graph[v].get(i);
				if (graphW[v].get(i) <= border && dp[v] < dp[u] + 1)
					dp[v] = dp[u] + 1;
			}
			
			if (dp[v] >= k)
				return true;
		}

		return false;
	}
}
