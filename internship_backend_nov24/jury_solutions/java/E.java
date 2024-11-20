import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.Stack;

public class E {
    static class Board {
        static class Cell {
            boolean isShip;
            boolean isHit;
            int shipId;

            Cell() {
                this.isShip = false;
                this.isHit = false;
                this.shipId = 0;
            }
        }

        private final Cell [][] cells;
        private final Map<Integer, Integer> shipHealth;
        int q;

        Board(Scanner scanner) {
            int n = scanner.nextInt();
            int m = scanner.nextInt();
            this.q = scanner.nextInt();

            Cell[][] board = new Cell[n][m];
            for (int i = 0; i < n; i++) {
                String line = scanner.next();
                for (int j = 0; j < m; j++) {
                    board[i][j] = new Cell();
                    if (line.charAt(j) == 'X') {
                        board[i][j].isShip = true;
                    }
                }
            }
            this.cells = board;
            this.shipHealth = calculateHealthShip(n, m, board);
        }

        Cell getCell(int x, int y) {
            return cells[x - 1][y - 1];
        }

        int getShipHealth(int shipId) {
            return shipHealth.get(shipId);
        }

        void updateShipHealth(int shipId, int remainingHealth) {
            shipHealth.put(shipId, remainingHealth);
        }


        private static Map<Integer, Integer> calculateHealthShip(int n, int m, Cell[][] board) {
            Map<Integer, Integer> shipHealth = new HashMap<>();

            int currentShipId = 1;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (board[i][j].isShip && board[i][j].shipId == 0) {
                        int size = markShipAndCount(board, i, j, currentShipId++, n, m);
                        shipHealth.put(currentShipId - 1, size);
                    }
                }
            }

            return shipHealth;
        }

        private static int markShipAndCount(Cell[][] board, int startX, int startY, int shipId, int N, int M) {
            Stack<int[]> stack = new Stack<>();
            stack.push(new int[]{startX, startY});
            int size = 0;

            while (!stack.isEmpty()) {
                int[] cell = stack.pop();
                int rowCell = cell[0];
                int columnCell = cell[1];

                if (rowCell < 0 || rowCell >= N || columnCell < 0 || columnCell >= M || !board[rowCell][columnCell].isShip || board[rowCell][columnCell].shipId != 0) {
                    continue;
                }

                board[rowCell][columnCell].shipId = shipId;
                size++;

                stack.push(new int[]{rowCell - 1, columnCell});
                stack.push(new int[]{rowCell + 1, columnCell});
                stack.push(new int[]{rowCell, columnCell - 1});
                stack.push(new int[]{rowCell, columnCell + 1});
            }

            return size;
        }
    }

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        Board loadedBoard = new Board(scanner);

        for (int i = 0; i < loadedBoard.q; i++) {
            int x = scanner.nextInt();
            int y = scanner.nextInt();

            Board.Cell cell = loadedBoard.getCell(x, y);
            if (cell.isHit) {
                System.out.println("MISS");
                continue;
            }

            cell.isHit = true;
            if (!cell.isShip) {
                System.out.println("MISS");
            } else {
                int shipId = cell.shipId;
                int remainingParts = loadedBoard.getShipHealth(shipId) - 1;
                loadedBoard.updateShipHealth(shipId, remainingParts);

                if (remainingParts == 0) {
                    System.out.println("DESTROY");
                } else {
                    System.out.println("HIT");
                }
            }
        }
    }
}
