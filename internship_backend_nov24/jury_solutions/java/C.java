import java.util.Scanner;
import java.util.stream.Stream;

public class C {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        int n = scanner.nextInt();
        int k = scanner.nextInt();
        int q = scanner.nextInt();

        int[] heights = Stream.generate(scanner::nextInt)
                .limit(n)
                .mapToInt(Integer::intValue)
                .toArray();

        int left = 0, changes = 0, maxLength = 0;

        for (int right = 0; right < n; right++) {
            if (heights[right] >= k) {
                changes++;
            }

            while (changes > q) {
                if (heights[left] >= k) {
                    changes--;
                }
                left++;
            }

            maxLength = Math.max(maxLength, right - left + 1);
        }

        System.out.println(maxLength);
        scanner.close();
    }
}
