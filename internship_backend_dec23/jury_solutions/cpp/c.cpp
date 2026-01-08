#include <iostream>
#include <vector>

using namespace std;

int main() {
    int n, m;
    cin >> n >> m;
    vector<vector<pair<int, int>>> f(n, vector<pair<int, int>>(m));
    vector<pair<int, int>> rows(n), cols(m);
    
    int q, x, y, c;
    cin >> q;
    for(int t = 0; t < q; ++t) {
        cin >> x >> y >> c; --x, --y;
        rows[x] = {c, t};
        cols[y] = {c, t};
    }
    
    for(int i = 0; i < n; ++i) {
        if(rows[i].first) {
            for(int j = 0; j < m; ++j) {
                f[i][j] = rows[i];
            }
        }
    }
    for(int j = 0; j < m; ++j) {
        if(cols[j].first) {
            for(int i = 0; i < n; ++i) {
                if(!f[i][j].first || f[i][j].second < cols[j].second) {
                    f[i][j] = cols[j];
                }
            }
        }
    }
    for(int i = 0; i < n; ++i) {
        for(int j = 0; j < m; ++j) {
            cout << f[i][j].first << ' ';
        } cout << '\n';
    }
}