#include <iostream>
#include <vector>
#include <map>
#include <algorithm>

using namespace std;

signed main() {
	int n, m;
	cin >> n >> m;
	vector<vector<int>> f(n + 2, vector<int>(m + 2));
	char t;
	for (int i = 1; i <= n; ++i)
		for (int j = 1; j <= m; ++j)
			cin >> t, f[i][j] = t - '0';
	auto ul = f; auto ur = f;
	auto dl = f; auto dr = f;
	for (int i = 1; i <= n; ++i) for (int j = 1; j <= m; ++j)
		ul[i][j] = max(ul[i][j], max(ul[i - 1][j], ul[i][j - 1]));
	for (int i = 1; i <= n; ++i) for (int j = m; j >= 1; --j)
		ur[i][j] = max(ur[i][j], max(ur[i - 1][j], ur[i][j + 1]));
	for (int i = n; i >= 1; --i) for (int j = 1; j <= m; ++j)
		dl[i][j] = max(dl[i][j], max(dl[i + 1][j], dl[i][j - 1]));
	for (int i = n; i >= 1; --i) for (int j = m; j >= 1; --j)
		dr[i][j] = max(dr[i][j], max(dr[i + 1][j], dr[i][j + 1]));
	int ans = 1e9, ans_x = -1, ans_y = -1;
	for (int i = 1; i <= n; ++i)
		for (int j = 1; j <= m; ++j){
		    int cur_max =  max(
				max(ul[i - 1][j - 1], ur[i - 1][j + 1]),
				max(dl[i + 1][j - 1], dr[i + 1][j + 1])
			);
			if(ans > cur_max){
    			ans = cur_max;
			    ans_x = i, ans_y = j;
			}
		}
	cout << ans_x << ' ' << ans_y << '\n';
}
