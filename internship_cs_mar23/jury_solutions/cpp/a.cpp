#include <iostream>
#include <vector>

using namespace std;

int main(){
    int n;
    cin >> n;
    vector<int> f(n);
    int mx = 0, mn = 0;
    for(int i = 0; i < n; ++i){
        cin >> f[i];
        if(f[i] >= f[mx])
            mx = i;
            
        if(f[i] < f[mn])
            mn = i;
    }
    cout << mx + 1 << ' ' << mn + 1;
}