#include <iostream>

#define int long long

using namespace std;

signed main() {
    int n, t;
    cin >> n >> t;
    int L = 0, R = 0, a, b;
    for (int i = 0; i < n; ++i) cin >> a, L += a;
    for (int i = 0; i < n; ++i) cin >> b, R += b;
    if (L <= t && t <= R)
        cout << "YES\n";
    else
        cout << "NO\n";
}