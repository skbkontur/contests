import java.util.HashSet;
import java.util.Scanner;
import java.util.Set;
 
public class TaskD {
    private static HashSet<Integer> step = new HashSet<>();
    private static HashSet<Integer> nextStep = new HashSet<>();
    private static HashSet<Integer> temp;
 
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        sc.nextLine();
        for (int i = 0; i < 6; i++) {
            nextStep.add(sc.nextInt());
        }
        sc.nextLine();
        for (int i = 1; i < n; i++) {
            temp = step;
            step = nextStep;
            nextStep = temp;
            nextStep.clear();
            for (int j = 0; j < 6; j++) {
                int number = sc.nextInt();
                for (int cur: step) {
                    nextStep.add(cur + number);
                }
            }
            sc.nextLine();
        }
        System.out.println(nextStep.size());
    }
}