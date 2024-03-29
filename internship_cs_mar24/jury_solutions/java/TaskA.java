import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.Scanner;

public class TaskA {

    public String testIfGivenRollsNumberSufficient() {
        return testIfGivenRollsNumberSufficient(System.in);
    }

    String testIfGivenRollsNumberSufficient(String input) {
        return testIfGivenRollsNumberSufficient(new ByteArrayInputStream(input.getBytes(StandardCharsets.UTF_8)));
    }

    private String testIfGivenRollsNumberSufficient(InputStream inputStream) {
        try (Scanner scanner = new Scanner(inputStream)) {
            int n = scanner.nextInt();
            long t = scanner.nextLong();

            long aNumbersSum = 0;
            for (int i = 0; i < n; i++) {
                aNumbersSum += scanner.nextInt();
            }

            long bNumbersSum = 0;
            for (int i = 0; i < n; i++) {
                bNumbersSum += scanner.nextInt();
            }

            return t >= aNumbersSum && t <= bNumbersSum ? "YES" : "NO";
        }
    }

    public static void main(String[] args) {
        String result = new TaskA().testIfGivenRollsNumberSufficient();
        System.out.print(result);
    }
}
