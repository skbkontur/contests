#include <iostream>
#include <vector>
#include <map>

using namespace std;

int main() {
    string pattern;
    for (char i = 'a'; i <= 'z'; ++i) {
        pattern += i;
        pattern += toupper(i);
    }
    int sz = pattern.size();
    map<char, int> pos;
    for (int i = 0; i < sz; ++i)
        pos[pattern[i]] = i;
    
    string s;
    cin >> s;
    int n = s.length();
    vector<int> t(n);
    for (int i = 0; i < n; ++i)
        t[i] = pos[s[i]];
    
    vector<int> f(n + 1);
    int q, l, r, x;
    cin >> q;
    while(q--) {
        cin >> l >> r >> x;
        f[l - 1] = (f[l - 1] + x) % sz;
        f[r] = ((f[r] - x) % sz + sz) % sz;
    }
    for (int i = 1; i < n; ++i)
        f[i] = (f[i - 1] + f[i]) % sz;
    for (int i = 0; i < n; ++i)
        cout << pattern[(t[i] + f[i]) % sz];
}