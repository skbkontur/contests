#include <iostream>

using namespace std;

int main() {
    int a, b;
    cin >> a >> b;
    if(a == b)
        cout << 3 * a;
    else if(a < b) cout << 3 * a + 2;
    else cout << 3 * b + 1;
}