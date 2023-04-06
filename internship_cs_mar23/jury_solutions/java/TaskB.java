import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.Set;

public class TaskB {
    static Map<Integer, List<Integer>> verticals = new HashMap<>();
    static Map<Integer, Set<Integer>> horizontals = new HashMap<>();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        sc.nextLine();
        for (int i = 0; i < n; i++) {
            String[] point = sc.nextLine().split(" ");
            int x = Integer.parseInt(point[0]);
            int y = Integer.parseInt(point[1]);
            if (!verticals.containsKey(x)) {
                verticals.put(x, new ArrayList<>());
            }
            verticals.get(x).add(y);
            if (!horizontals.containsKey(y)) {
                horizontals.put(y, new HashSet<>());
            }
            horizontals.get(y).add(x);
        }
        solve(verticals, horizontals);
    }

    private static void solve(Map<Integer, List<Integer>> verticals, Map<Integer, Set<Integer>> horizontals) {
        long maxSquare = 0;
        for (int startX: verticals.keySet()) {
            List<Integer> startRow = verticals.get(startX);
            if (startRow.size() > 1) {
                for (int i = 0; i < startRow.size(); i++) {
                    int startY = startRow.get(i);
                    for (int j = i + 1; j < startRow.size(); j++) {
                        int endY = startRow.get(j);
                        for (int endX: horizontals.get(startY)) {
                            if (horizontals.get(endY).contains(endX)) {
                                long square = getSquare(startX, endX, startY, endY);
                                maxSquare = Math.max(square, maxSquare);
                            }
                        }
                    }
                }
            }
        }
        System.out.println(maxSquare);
    }
    private static long getSquare(int startX, int endX, int startY, int endY) {
        long dX = Math.abs(startX - endX);
        long dY = Math.abs(startY - endY);
        return dX * dY;
    }
}