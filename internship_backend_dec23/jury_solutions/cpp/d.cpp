#include <iostream>

using namespace std;

int main() {
    int n, k;
    cin >> n >> k;
    string s;
    cin >> s;
    string all = "RGB";
    int ans = 0;
    for(char ch : all) {
        int cur = 0, l = 0;
        for(int i = 0; i < n; ++i) {
            if(ch != s[i])
                ++cur;
            while(l < i && cur > k) {
                if(ch != s[l])
                    --cur;
                ++l;
            }
            ans = max(ans, i - l + 1);
        }
    }
    cout << ans;
}