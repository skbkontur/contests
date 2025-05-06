import java.util.HashSet;
import java.util.Scanner;

public class A {
	public static void main(String[] args) throws Exception {
		new A().run();
	}

	private final int AllCardsCount = 52;

	private final HashSet<Character> AllSuits = new HashSet<>() {{
		add('C');
		add('D');
		add('H');
		add('S');
	}};

	private final HashSet<Character> AllRanks = new HashSet<>() {{
		add('2');
		add('3');
		add('4');
		add('5');
		add('6');
		add('7');
		add('8');
		add('9');
		add('T');
		add('J');
		add('Q');
		add('K');
		add('A');
	}};

	private Scanner scanner;

	public void run() {
		scanner = new Scanner(System.in);
		String[] groupsCount = scanner.nextLine().split(" ");
		double probability1 = calculateProbability(Integer.parseInt(groupsCount[0]), Integer.parseInt(groupsCount[1]));
		double probability2 = calculateProbability(Integer.parseInt(groupsCount[2]), Integer.parseInt(groupsCount[3]));

		System.out.println(Math.max(probability1, probability2));
	}

	private double calculateProbability(int removedGroupsCount, int soughtGroupsCount) {
		HashSet<String> removedCards = new HashSet<>();
		for (int i = 0; i < removedGroupsCount; i++) {
			HashSet<Character> group = new HashSet<>();
			for (char c : scanner.nextLine().toCharArray())
				group.add(c);

			removedCards.addAll(getCards(group));
		}

		HashSet<String> soughtCards = new HashSet<>();
		for (int i = 0; i < soughtGroupsCount; i++) {
			HashSet<Character> group = new HashSet<>();
			for (char c : scanner.nextLine().toCharArray())
				group.add(c);
			soughtCards.addAll(getCards(group));
		}

		if (removedCards.size() == AllCardsCount)
			return 0.0;

		int removedCardsCount = removedCards.size();

		removedCards.retainAll(soughtCards);
		int cardsIntersection = removedCards.size();

		return (double) (soughtCards.size() - cardsIntersection) / (AllCardsCount - removedCardsCount);
	}

	private HashSet<String> getCards(HashSet<Character> group) {
		HashSet<Character> currentSuits = new HashSet<>(AllSuits);
		currentSuits.retainAll(group);
		if (currentSuits.isEmpty())
			currentSuits = new HashSet<>(AllSuits);

		group.removeAll(currentSuits);
		HashSet<Character> currentRanks = group;
		if (currentRanks.isEmpty())
			currentRanks = new HashSet<>(AllRanks);

		HashSet<String> cards = new HashSet<>();
		for (Character rank : currentRanks) {
			for (Character suit : currentSuits)
				cards.add(rank.toString() + suit);
		}

		return cards;
	}
}
