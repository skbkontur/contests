#include <bits/stdc++.h>

int main() {
    int n;
    std::cin >> n;
    std::string res = "";
    int mx = INT_MIN;
    while (n--) {
        std::string s;
        std::cin >> s;
        std::vector<int> charsCount(40, 0);

        int unique = 0;

        for (auto& i : s)
            charsCount[i - 'a']++;

        for (auto& i : charsCount)
            unique += i == 0 ? 0 : 1;

        if (unique > mx) {
            res = s;
            mx = unique;
        }
    }

    std::cout << mx << ' ' << res;
    return 0;
}