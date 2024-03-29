import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.Scanner;

public class TaskB {

    public int calculateMinChanges() {
        return calculateMinChanges(System.in);
    }

    int calculateMinChanges(String input) {
        return calculateMinChanges(new ByteArrayInputStream(input.getBytes(StandardCharsets.UTF_8)));
    }

    private int calculateMinChanges(InputStream inputStream) {
        try (Scanner scanner = new Scanner(inputStream)) {
            return calculateMinChangesForString(scanner.next());
        }
    }

    private int calculateMinChangesForString(String sourceString) {
        int length = sourceString.length();
        int candidate = Integer.MAX_VALUE;
        for (int l = 1; l <= length; l ++) {
            if (length % l == 0) {
                int minChanges = calculateMinChangesForLen(l, sourceString);
                candidate = Integer.min(candidate, minChanges);
            }
        }

        return candidate;
    }

    private int calculateMinChangesForLen(int rangeLen, String sourceString) {
        int rangesCount = sourceString.length() / rangeLen;

        int changesWithZeroesStart = 0;
        int changesWithOnesStart = 0;
        for (int i = 0; i < rangesCount; i++) {
            int zeros = 0;
            int ones = 0;

            for (int j = 0; j < rangeLen; j++) {
                char c = sourceString.charAt(j + i * rangeLen);
                if (c == '0') {
                    zeros++;
                }

                if (c == '1') {
                    ones++;
                }
            }

            changesWithOnesStart += (i % 2 == 0 ? ones : zeros);
            changesWithZeroesStart += (i % 2 == 0 ? zeros : ones);
        }

        return Math.min(changesWithZeroesStart, changesWithOnesStart);
    }

    public static void main(String[] args) {
        int result = new TaskB().calculateMinChanges();
        System.out.print(result);
    }
}
