import java.util.Scanner;

public class TaskC {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        int k = sc.nextInt();
        sc.nextLine();
        int[] data = new int[n];
        for (int i = 0; i < n; i++) {
            data[i] = sc.nextInt();
        }
        long result = 0;
        int stopIndex = 0;
        int sum = 0;
        boolean hasZero = false;
        for (int i = 0; i < n; i++) {
            while (stopIndex < n && (data[stopIndex] != 0 || !hasZero)) {
                if (data[stopIndex] == 0) hasZero = true;
                sum += data[stopIndex];
                if (sum <= k) {
                    stopIndex++;
                }
                else
                {
                    sum -= data[stopIndex];
                    break;
                }
            }
            result += stopIndex - i;
            sum -= data[i];
            if (data[i] == 0) hasZero = false;
        }
        System.out.println(result);
    }
}