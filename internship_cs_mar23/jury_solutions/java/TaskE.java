import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        int m = sc.nextInt();
        sc.nextLine();
        int[][] data = new int[n][m];
        int[][] digitsInRow = new int[n][10];
        int[][] digitsInColumn = new int[m][10];
        int[] digitsInMatrix = new int[10];

        for (int i = 0; i < n; i++) {
            String s = sc.nextLine();
            for (int j = 0; j < m; j++) {
                int cur = s.charAt(j) - '0';
                data[i][j] = cur;
                digitsInRow[i][cur]++;
                digitsInColumn[j][cur]++;
                digitsInMatrix[cur]++;
            }
        }

        int ansDigit = 9;
        int ansRow = 0;
        int ansColumn = 0;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {
                int maxNotDeleted = -1;
                for (int k = 9; k >= 0; k--) {
                    int digitsInCell = data[i][j] == k ? 1 : 0;
                    if (digitsInMatrix[k] > digitsInRow[i][k] + digitsInColumn[j][k] - digitsInCell) {
                        maxNotDeleted = k;
                        break;
                    }
                }
                
                if (maxNotDeleted < ansDigit) {
                    ansDigit = maxNotDeleted;
                    ansRow = i;
                    ansColumn = j;
                }
           }
        }
        System.out.println(String.format("%d %d", ansRow + 1, ansColumn + 1));
    }
}