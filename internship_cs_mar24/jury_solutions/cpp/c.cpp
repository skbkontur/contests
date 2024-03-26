#include <iostream>
#include <vector>

#define int long long

using namespace std;

signed main() {
    int n, q, x, y;
    cin >> n >> q;
    char type;
    vector<int> rows(n + 2), cols(n + 2);
    rows[0] = rows[n + 1] = cols[0] = cols[n + 1] = 1;
    int v = 2, h = 2;
    while(q--) {
        cin >> type >> x >> y;
        if (type == '+') {
            if (rows[x] == 0) {
                if (rows[x - 1] && rows[x + 1])
                    --h;
                else if (!rows[x - 1] && !rows[x + 1])
                    ++h;
            }
            if (cols[y] == 0) {
                if (cols[y - 1] && cols[y + 1])
                    --v;
                else if (!cols[y - 1] && !cols[y + 1])
                    ++v;
            }
            ++rows[x], ++cols[y];
        }
        else {
            --rows[x], --cols[y];
            if (rows[x] == 0) {
                if (rows[x - 1] && rows[x + 1])
                    ++h;
                else if (!rows[x - 1] && !rows[x + 1])
                    --h;
            }
            if (cols[y] == 0) {
                if (cols[y - 1] && cols[y + 1])
                    ++v;
                else if (!cols[y - 1] && !cols[y + 1])
                    --v;
            }
        }
        cout << (h - 1) * (v - 1)  << '\n';
    }
}