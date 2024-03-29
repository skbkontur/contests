import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.function.Consumer;

public class TaskC {

    public void solveWithOutput() {
        solveWithOutput(System.in, System.out::println);
    }

    List<Long> solveWithOutput(String input) {
        List<Long> result = new ArrayList<>();
        solveWithOutput(new ByteArrayInputStream(input.getBytes(StandardCharsets.UTF_8)), result::add);

        return result;
    }

    private void solveWithOutput(InputStream inputStream, Consumer<Long> resultConsumer) {
        try (Scanner scanner = new Scanner(inputStream)) {
            solveWithOutput(scanner, resultConsumer);
        }
    }

    private void solveWithOutput(Scanner scanner, Consumer<Long> resultConsumer) {
        int n = scanner.nextInt();
        int q = scanner.nextInt();
        int[] rowsAttacks = new int[n + 2];
        int[] columnsAttacks = new int[n + 2];
        rowsAttacks[0] = 1;
        rowsAttacks[n + 1] = 1;
        columnsAttacks[0] = 1;
        columnsAttacks[n + 1] = 1;
        int verticalSplits = 2;
        int horizontalSplits = 2;

        for (var i = 0; i < q; i++) {
            char op = extractOperation(scanner);
            int x = scanner.nextInt();
            int y = scanner.nextInt();

            horizontalSplits = updateAttacksPerWay(rowsAttacks, horizontalSplits, x, op == '+');
            verticalSplits = updateAttacksPerWay(columnsAttacks, verticalSplits, y, op == '+');

            long currentComponents = ((long) horizontalSplits - 1) * (verticalSplits - 1);
            resultConsumer.accept(currentComponents);
        }
    }

    private int updateAttacksPerWay(int[] attacks, int currentSplit, int index, boolean isAdding) {
        if (isAdding) {
            int newSplit = updateSplits(attacks, currentSplit, index, true);
            attacks[index] ++;
            return newSplit;
        } else {
            attacks[index] --;
            return updateSplits(attacks, currentSplit, index, false);
        }
    }

    private int updateSplits(int[] attacks, int currentSplit, int index, boolean isAdding) {
        if (attacks[index] > 0) {
            return currentSplit;
        }
        boolean prevAttacked = attacks[index - 1] != 0;
        boolean nextAttacked = attacks[index + 1] != 0;

        if (prevAttacked != nextAttacked) {
            return currentSplit;
        }

        if (prevAttacked) {
            return currentSplit + (isAdding ? -1 : 1);
        } else {
            return currentSplit + (isAdding ? 1 : -1);
        }
    }

    private char extractOperation(Scanner scanner) {
        String input = scanner.next();
        if (!input.isEmpty()) {
            return input.charAt(0);
        } else {
            throw new IllegalArgumentException("Invalid operation");
        }
    }

    public static void main(String[] args){
        new TaskC().solveWithOutput();
    }
}