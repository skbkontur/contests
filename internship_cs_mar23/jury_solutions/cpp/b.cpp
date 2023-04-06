#include <iostream>
#include <vector>
#include <set>

using namespace std;

signed main() {
	int n;
	cin >> n;
	vector<pair<int, int>> f(n);
	set<pair<int, int>> s;
	for (int i = 0; i < n; ++i) {
		cin >> f[i].first >> f[i].second;
		s.insert(f[i]);
	}
	long long ans = 0;
	for (int i = 0; i < n; ++i) {
		for (int j = i + 1; j < n; ++j) {
			long long area = (long long)abs(f[i].first - f[j].first) *
				abs(f[i].second - f[j].second);
			if (area <= ans) continue;
			pair<int, int> a(f[i].first, f[j].second);
			pair<int, int> b(f[j].first, f[i].second);
			if (s.count(a) && s.count(b))
				ans = area;
		}
	}
	cout << ans;
}
