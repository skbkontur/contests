import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.Scanner;

public class TaskD {


    public String performStringAddition() {
        return performStringAddition(System.in);
    }

    String performStringAddition(String input) {
        return performStringAddition(new ByteArrayInputStream(input.getBytes(StandardCharsets.UTF_8)));
    }

    private String performStringAddition(InputStream inputStream) {
        try (Scanner scanner = new Scanner(inputStream)) {
            return performStringAddition(scanner);
        }
    }

    private String performStringAddition(Scanner scanner) {
        String sourceString = scanner.nextLine();
        int len = sourceString.length();

        int q = scanner.nextInt();

        long[] diff = new long[len];
        for (int i = 0; i < q; i++) {
            int l = scanner.nextInt() - 1;
            int r = scanner.nextInt() - 1;
            int x = scanner.nextInt();

            diff[r] += x;
            if (l > 0) {
                diff[l - 1] -= x;
            }
        }

        long[] compressedTransformations = new long[len];
        compressedTransformations[len - 1] = diff[len - 1];
        for (int i = len - 2; i >= 0; i--) {
            compressedTransformations[i] = compressedTransformations[i + 1] + diff[i];
        }

        char[] chars = sourceString.toCharArray();
        for (int i = 0; i < len; i++) {
            chars[i] = transform(chars[i], compressedTransformations[i]);
        }

        return new String(chars);
    }

    private char transform(char c, long x) {
        long shiftValue = x / 2;
        c = shift(c, shiftValue);

        if (x % 2 == 1) {
            c = transform(c);
        }

        return c;
    }

    private char shift(char c, long positionsNumber) {
        char startChar = Character.isLowerCase(c) ? 'a' : 'A';
        int startPosition = c - startChar ;
        int targetPosition = (int) ((startPosition + positionsNumber) % 26);
        return (char) (startChar  + targetPosition);
    }

    private char transform(char c) {
        if (Character.isLowerCase(c)) {
            return Character.toUpperCase(c);
        }

        return Character.toLowerCase(shift(c, 1));
    }

    public static void main(String[] args) {
        String result = new TaskD().performStringAddition();
        System.out.print(result);
    }
}
