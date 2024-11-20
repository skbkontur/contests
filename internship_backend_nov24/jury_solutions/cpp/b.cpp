#include <bits/stdc++.h>

int main() {
    int n;
    std::cin >> n;
    long long throwsX = 0L;
    long long throwsY = 0L;

    for (int i = 0; i < n; i++) {
        long long x, y;
        std::cin >> x >> y;
        throwsX -= x;
        throwsY -= y;
    }

    for (int i = 0; i < n; i++) {
        long long x, y;
        std::cin >> x >> y;
        throwsX += x;
        throwsY += y;
    }

    std::cout << throwsX / n << ' ' << throwsY / n;
    return 0;
}