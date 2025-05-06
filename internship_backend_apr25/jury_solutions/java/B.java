import java.util.Arrays;
import java.util.Scanner;

public class B {
	public static void main(String[] args) throws Exception {
		new B().run();
	}

	public void run() {
		Scanner scanner = new Scanner(System.in);
		scanner.nextLine();
		int[] houses = Arrays.stream(scanner.nextLine().split(" ")).mapToInt(Integer::parseInt).toArray();
		int[] heaters = Arrays.stream(scanner.nextLine().split(" ")).mapToInt(Integer::parseInt).toArray();

		int[] minRadius = new int[houses.length];
		Arrays.fill(minRadius, Integer.MAX_VALUE);

		int j = 0;
		int k = 0;

		Arrays.sort(houses);
		Arrays.sort(heaters);

		while (j < houses.length) {
			minRadius[j] = Math.min(minRadius[j], Math.abs(houses[j] - heaters[k]));

			if (k > 0)
				minRadius[j] = Math.min(minRadius[j], Math.abs(houses[j] - heaters[k - 1]));

			if (heaters[k] < houses[j] && k < heaters.length - 1)
				k++;
			else
				j++;
		}

		System.out.println(Arrays.stream(minRadius).max().getAsInt());
	}
}

