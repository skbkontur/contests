#include <iostream>
#include <set>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

const int allCardsCount = 52;
const set<char> all_suits = { 'C', 'D', 'H', 'S' };
const set<char> all_ranks = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

vector<string> read_cardsets (int size)
{
	vector<string> cardsets;
	string cardset;
	for (int i = 0; i < size; i++)
	{
		cin >> cardset;
		cardsets.push_back(cardset);
	}
	return cardsets;
}

bool cardset_contains_card (string cardset, char suit, char rank)
{
	set<char> suits_in_set;
	set<char> ranks_in_set;
	for (char c : cardset)
	{
		if (all_suits.count(c) != 0)
			suits_in_set.insert(c);
		else
			ranks_in_set.insert(c);
	}
	if (suits_in_set.empty())
		suits_in_set = all_suits;
	if (ranks_in_set.empty())
		ranks_in_set = all_ranks;

	return suits_in_set.count(suit) != 0 && ranks_in_set.count(rank) != 0;
}

bool cardsets_contains_card (vector<string> cardsets, char suit, char rank)
{
	for (auto cardset : cardsets)
	{
		if (cardset_contains_card(cardset, suit, rank))
			return true;
	}

	return false;
}

double calculate_probability (vector<string> removed_cardsets, vector<string> checked_cardsets)
{
	int all_not_removed_count = 0;
	int checked_not_removed_count = 0;
	for (char suit : all_suits)
	{
		for (char rank : all_ranks)
		{
			if (!cardsets_contains_card(removed_cardsets, suit, rank))
			{
				all_not_removed_count++;

				if (cardsets_contains_card(checked_cardsets, suit, rank))
					checked_not_removed_count++;
			}
		}
	}

	return all_not_removed_count == 0 ? 0.0 : checked_not_removed_count / (double)(all_not_removed_count);
}

int main ()
{
	int r1, s1, r2, s2;
	cin >> r1 >> s1 >> r2 >> s2;

	auto removed_cardsets1 = read_cardsets(r1);
	auto checked_cardsets1 = read_cardsets(s1);

	auto removed_cardsets2 = read_cardsets(r2);
	auto checked_cardsets2 = read_cardsets(s2);

	double probability1 = calculate_probability(removed_cardsets1, checked_cardsets1);
	double probability2 = calculate_probability(removed_cardsets2, checked_cardsets2);

	cout << max(probability1, probability2);

	return 0;
}
