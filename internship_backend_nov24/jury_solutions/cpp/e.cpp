#include <bits/stdc++.h>

std::vector<std::pair<int, int>> steps{{1,  0},
                                       {-1, 0},
                                       {0,  1},
                                       {0,  -1}};

bool is_in_bounds(int x, int y, int n, int m) {
    return x >= 0 && x < n && y >= 0 && y < m;
}

void dfs(int startX, int startY, int setNumber, int n, int m,
         std::vector<int> &setsNumbers,
         std::vector<std::vector<char>> &field,
         std::vector<std::vector<int>> &used) {

    std::vector<std::pair<int, int>> stack;
    stack.emplace_back(startX, startY);
    used[startX][startY] = setNumber;

    while(!stack.empty()) {
        auto current = stack[stack.size() - 1];
        stack.pop_back();
        setsNumbers[setNumber]++;

        for (auto& step : steps) {
            int posX = current.first + step.first;
            int posY = current.second + step.second;

            if (is_in_bounds(posX, posY, n, m) && field[posX][posY] == 'X' && used[posX][posY] == 0) {
                stack.emplace_back(posX, posY);
                used[posX][posY] = setNumber;
            }
        }
    }
}

int main() {
    int n, m, q;
    std::cin >> n >> m >> q;

    std::vector<int> setsNumbers(n * m, 0);
    std::vector<std::vector<char>> field(n, std::vector<char>(m, '.'));
    std::vector<std::vector<int>> used(n, std::vector<int>(m, 0));

    for (int i = 0; i < n; i++) {
        std::string line;
        std::cin >> line;
        for (int j = 0; j < m; j++) {
            field[i][j] = line[j];
        }
    }

    int setCounter = 0;

    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++) {
            if (used[i][j] == 0 && field[i][j] == 'X') {
                setCounter++;
                dfs(i, j, setCounter, n, m, setsNumbers, field, used);
            }
        }

    while (q--) {
        int x, y;
        std::cin >> x >> y;
        x--;
        y--;
        if (used[x][y] == 0)
            std::cout << "MISS" << '\n';
        else {
            setsNumbers[used[x][y]]--;
            std::string result = setsNumbers[used[x][y]] == 0 ? "DESTROY" : "HIT";
            std::cout << result << '\n';
        }
    }
    return 0;
}