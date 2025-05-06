#include <iostream>
#include <map>
#include <stack>
#include <vector>
#include <algorithm>

using namespace std;

const int NEW = 0;
const int OPEN = 1;
const int CLOSE = 2;

struct colour
{
	int right_border, left_border, state;

	colour (int _right_border, int _left_border, int _state)
		: right_border(_right_border), left_border(_left_border), state(_state)
	{
	}

	colour() = default;
};

map<int, colour> colours;

int main ()
{
	int m, n;
	cin >> m >> n;

	vector<int> colours_line;
	for (int i = 0; i < m; i++)
	{
		int c;
		cin >> c;
		colours_line.push_back(c - 1);
	}

	for (int i = 0; i < m; i++)
	{
		auto colour_id = colours_line[i];
		if (colours.count(colour_id) == 0)
			colours[colour_id] = colour(-1, INT32_MAX, NEW);

		if (i < colours[colour_id].left_border)
			colours[colour_id].left_border = i;
		if (i > colours[colour_id].right_border)
			colours[colour_id].right_border = i;
	}

	stack<int> stack;
	vector<int> colours_ids;
	for (int i = 0; i < m; i++)
	{
		auto colour_id = colours_line[i];
		switch (colours[colour_id].state)
		{
			case NEW:
				stack.push(i);
				colours[colour_id].state = OPEN;
				break;
			case OPEN:
			{
				while (colours_line[stack.top()] != colour_id)
				{
					colours_ids.push_back(colours_line[stack.top()]);
					colours[colours_line[stack.top()]].state = CLOSE;
					stack.pop();
				}

				break;
			}
			case CLOSE:
				cout << -1 << endl;
				return 0;
		}
	}

	while (!stack.empty())
	{
		colours_ids.push_back(colours_line[stack.top()]);
		stack.pop();
	}

	cout << colours_ids.size() << endl;
	reverse(colours_ids.begin(), colours_ids.end());
	for (auto colour_id : colours_ids)
	{
		cout << colour_id + 1 << " " << colours[colour_id].left_border + 1 << " " << colours[colour_id].right_border + 1 << endl;
	}

	return 0;
}
