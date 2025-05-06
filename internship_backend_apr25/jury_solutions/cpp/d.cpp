#include <bits/stdc++.h>

using namespace std;
typedef long long ll;

mt19937 rnd(chrono::steady_clock::now().time_since_epoch().count());

bool check_cycle (vector<vector<pair<int, ll>>>& graph, int v, ll border, vector<int>& colors)
{
	bool res = false;
	colors[v] = 1;
	for (auto& u : graph[v])
	{
		if (u.second > border) 
			continue;
		switch (colors[u.first])
		{
			case 1:
				return true;
			case 2:
				continue;
			default:
				res = res || check_cycle(graph, u.first, border, colors);
				break;
		}
	}
	colors[v] = 2;
	return res;
}

bool has_cycle (vector<vector<pair<int, ll>>>& graph, ll border)
{
	vector<int> colors(graph.size(), 0);

	bool res = false;

	for (int i = 0; i < graph.size(); i++)
	{
		if (colors[i] != 0) continue;

		res = res || check_cycle(graph, i, border, colors);
	}

	return res;
}

void top_sort (vector<vector<pair<int, ll>>>& graph, int v, ll border, vector<int>& order, vector<char>& visited)
{
	for (auto& u : graph[v])
	{
		if (visited[u.first] || u.second > border) 
		continue;
		visited[u.first] = true;
		top_sort(graph, u.first, border, order, visited);
	}

	order.emplace_back(v);
}

bool check_k_len_path (vector<vector<pair<int, ll>>>& graph, ll k, ll border)
{
	vector<int> order;
	vector<char> visited(graph.size(), 0);

	for (int i = 0; i < graph.size(); i++)
	{
		if (visited[i])
			continue;

		visited[i] = true;
		top_sort(graph, i, border, order, visited);
	}

	if (order.empty()) 
		return false;

	reverse(order.begin(), order.end());

	vector<ll> dp(graph.size(), 0);

	for (auto& v : order)
	{
		if (dp[v] == 0)
			dp[v] = 1;
		for (auto &u: graph[v])
		{
			if (u.second > border)
				continue;
			dp[u.first] = max(dp[u.first], dp[v] + 1);
		}
	}

	ll mx = 0;

	for (auto& i : dp)
		mx = max(mx, i);

	return mx >= k;
}

bool check (vector<vector<pair<int, ll>>>& graph, ll k, ll border)
{
	return has_cycle(graph, border) || check_k_len_path(graph, k, border);
}

int main () 
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	cin.sync_with_stdio(0);
	cout.sync_with_stdio(0);
	srand(time(0));
	cout.precision(20);

	ll n,m,k;
	cin >> n >> m >> k;
	k++;

	ll mx = 0;

	vector<vector<pair<int, ll>>> graph(n, vector<pair<int, ll>>());

	for (int i = 0; i < m; i++)
	{
		int u, v;
		ll cost;
		cin >> u >>v >> cost;
		mx = max(mx ,cost);
		u--;
		v--;
		graph[u].emplace_back(v, cost);
	}

	ll l = -1, r = mx + 10;
	ll ans = -1;

	while (l < r - 1)
	{
		ll mid = (l + r) / 2;

		if (check(graph, k, mid))
		{
			r = mid;
			ans = mid;
		}
		else
			l = mid;
	}

	cout << ans;

	return 0;
}
