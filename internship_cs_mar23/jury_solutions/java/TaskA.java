import java.util.Scanner;

public class TaskA {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        sc.nextLine();
        String[] data = sc.nextLine().split(" ");
        printMaxContrastIndexPair(data);
    }
    private static void printMaxContrastIndexPair(String[] data) {
        int min = Integer.MAX_VALUE;
        int max = Integer.MIN_VALUE;
        int minIndex = -1;
        int maxIndex = -1;
        int index = 1;
        for (String s: data) {
            int cur = Integer.parseInt(s);
            if (cur >= max) {
                max = cur;
                maxIndex = index;
            }
            if (cur < min){
                min = cur;
                minIndex = index;
            }
            index++;
        }
        System.out.println(String.format("%d %d", maxIndex, minIndex));
    }
}