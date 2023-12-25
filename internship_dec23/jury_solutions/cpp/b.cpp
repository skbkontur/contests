#include <iostream>
#include <vector>

using namespace std;

int main() {
    int n;
    cin >> n;
    vector<int> f(n);
    int all_sum = 0;
    for(int i = 0; i < n; ++i) cin >> f[i], all_sum += f[i];
    int cur_sum = 0, border = -1;
    for(int i = 0; i < n; ++i) {
        cur_sum += f[i];
        if(cur_sum == all_sum - cur_sum) {
            border = i;
            break;
        }
    }
    if(border == -1) {
        cout << -1;
        return 0;
    }
    cout << f[0];
    for(int i = 1; i <= border; ++i) cout << '+' << f[i];
    cout << "=";
    for(int i = border + 1; i + 1 < n; ++i) cout << f[i] << '+';
    cout << f[n - 1];
}