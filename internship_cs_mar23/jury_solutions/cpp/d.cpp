#include <iostream>
#include <vector>
#include <unordered_set>

using namespace std;

int ans = 0, n;
vector<vector<int>> f;
vector<unordered_set<int>> h;

void rec(int last = 0, int cur = 0) {
	if (h[last].count(cur)) return;
	h[last].insert(cur);
	if (last == n)
		return void(++ans);
	for (int i = 0; i < 6; ++i)
		rec(last + 1, cur + f[last][i]);
}

signed main() {
	cin >> n;
	f.resize(n, vector<int>(6));
	h.resize(n + 1);
	for (int i = 0; i < n; ++i)
		for (int j = 0; j < 6; ++j)
			cin >> f[i][j];
	rec();
	cout << ans;
}