#include <iostream>
#include <vector>

using namespace std;

int main() {
	int n, k;
	cin >> n >> k;
	vector<int> arr(n);
	for (int i = 0; i < n; ++i)
		cin >> arr[i];
	int l = 0, zeros = 0;
	long long sum = 0, ans = 0;
	for (int r = 0; r < n; ++r) {
		sum += arr[r];
		zeros += arr[r] == 0;
		while (sum > k || zeros > 1) {
			zeros -= arr[l] == 0;
			sum -= arr[l++];
		}
		if(sum <= k && zeros < 2)
			ans += r - l + 1;
	}
	cout << ans;
}