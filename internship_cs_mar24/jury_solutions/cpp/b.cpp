#include <iostream>

using namespace std;

int main() {
    string s;
    cin >> s;
    int n = s.length();
    int ans = n;
    for (int k = 1; k <= n; ++k) {
        if (n % k) continue;
        int res = 0;
        for (int i = 0; i < n; ++i)
            res += (i / k % 2) != (s[i] - '0');
        ans = min(ans, min(res, n - res));
    }
    cout << ans;
}