#include <bits/stdc++.h>
 
int main() {
    long long maxX, maxY, xCount, yCount;
    std::cin >> maxX >> maxY >> xCount >> yCount;
 
    std::set<long long> xLines;
    std::set<long long> yLines;
    for (int i = 0; i < xCount; i++) {
        long long border;
        std::cin >> border;
        xLines.insert(border);
    }
 
    for (int i = 0; i < yCount; i++) {
        long long border;
        std::cin >> border;
        yLines.insert(border);
    }
 
    xLines.insert(0);
    yLines.insert(0);
 
    xLines.insert(maxX);
    yLines.insert(maxY);
 
    int queriesCount;
    std::cin >> queriesCount;
 
    while (queriesCount--) {
        long long x1, y1, x2, y2;
        std::cin >> x1 >> y1 >> x2 >> y2;
 
        auto rangX1 = xLines.lower_bound(x1);
        auto rangX2 = xLines.lower_bound(x2);
 
        auto rangY1 = yLines.lower_bound(y1);
        auto rangY2 = yLines.lower_bound(y2);
  
        if (rangX1 == rangX2 && rangY1 == rangY2)
            std::cout << "YES\n";
        else
            std::cout << "NO\n";
    }
 
    return 0;
}
