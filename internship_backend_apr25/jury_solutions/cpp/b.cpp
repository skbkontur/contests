#include <iostream>
#include <vector>
#include <algorithm>
#include <cmath>
#include <limits>

using namespace std;

int main ()
{
	string temp;
	getline(cin, temp);
	vector<int> houses;
	vector<int> heaters;

	int n;
	while (cin >> n)
	{
		houses.push_back(n);
		if (cin.peek() == '\n')
			break;
	}
	while (cin >> n)
	{
		heaters.push_back(n);
		if (cin.peek() == '\n')
			break;
	}

	vector<int> min_radius(houses.size(), numeric_limits<int>::max());

	int j = 0;
	int k = 0;

	sort(houses.begin(), houses.end());
	sort(heaters.begin(), heaters.end());

	while (j < houses.size())
	{
		min_radius[j] = min(min_radius[j], abs(houses[j] - heaters[k]));

		if (k > 0)
			min_radius[j] = min(min_radius[j], abs(houses[j] - heaters[k - 1]));

		if (heaters[k] < houses[j] && k < heaters.size() - 1)
			k++;
		else
			j++;
	}

	cout << *max_element(min_radius.begin(), min_radius.end()) << endl;

	return 0;
}
