#include <iostream>
#include <vector>
#include <algorithm>
#include <map>

using namespace std;

int stop(int idx) {
    cout << "Stop " << idx;
    return 0;
}
int complete() {
    cout << "Complete";
    return 0;
}

int main() {
    int n;
    cin >> n;
    map<int, vector<int>> by_x, by_y;
    int x, y;
    for (int i = 0; i < n; ++i) {
        cin >> x >> y;
        by_x[x].push_back(y);
        by_y[y].push_back(x);
    }
    for (auto& t : by_x)
        sort(t.second.begin(), t.second.end());
    for (auto& t : by_y)
        sort(t.second.begin(), t.second.end());
    int m, k;
    char dir;
    cin >> m;
    x = 0, y = 0;
    int end_x, end_y;
    for (int i = 1; i <= m; ++i) {
        cin >> dir >> k;
        if (dir == 'U')
            end_x = x, end_y = y + k;
        else if (dir == 'D')
            end_x = x, end_y = y - k;
        else if (dir == 'R')
            end_x = x + k, end_y = y;
        else
            end_x = x - k, end_y = y;
        if (x == end_x) {
            int y1 = y, y2 = end_y;
            if (y1 > y2) swap(y1, y2);
            
            auto it = lower_bound(by_x[x].begin(), by_x[x].end(), y1);
            if (it != by_x[x].end() && *it <= y2)
                return stop(i);
        }
        else {
            int x1 = x, x2 = end_x;
            if (x1 > x2) swap(x1, x2);
            
            auto it = lower_bound(by_y[y].begin(), by_y[y].end(), x1);
            if (it != by_y[y].end() && *it <= x2)
                return stop(i);
        }
        x = end_x, y = end_y;
    }
    return complete();
}