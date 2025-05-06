allCardsCount = 52
allSuits = set("CDHS")
allRanks = set("23456789TJQKA")


def solve():
    groups_count = list(map(int, input().split()))

    probability1 = calculate_probability(groups_count[0], groups_count[1])
    probability2 = calculate_probability(groups_count[2], groups_count[3])

    if probability1 > probability2:
        print(probability1)
    else:
        print(probability2)


def calculate_probability(removed_groups_count, sought_groups_count):
    removed_cards = set()
    for i in range(removed_groups_count):
        group = set(input())
        removed_cards.update(get_cards(group))

    sought_cards = set()
    for i in range(sought_groups_count):
        group = set(input())
        sought_cards.update(get_cards(group))

    if len(removed_cards) == allCardsCount:
        return 0.0
    removed_cards_count = len(removed_cards)
    removed_cards.intersection_update(sought_cards)
    cards_intersection = len(removed_cards)
    return (len(sought_cards) - cards_intersection) / (allCardsCount - removed_cards_count)


def get_cards(group):
    current_suits = set(allSuits)
    current_suits.intersection_update(group)
    if len(current_suits) == 0:
        current_suits = allSuits

    current_ranks = group.difference(current_suits)
    if len(current_ranks) == 0:
        current_ranks = allRanks

    cards = set()
    for rank in current_ranks:
        for suit in current_suits:
            cards.add(rank + suit)

    return cards


if __name__ == '__main__':
    solve()
