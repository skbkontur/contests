import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class B {
    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        int n = Integer.parseInt(reader.readLine().trim());
        long throwsX = 0, throwsY = 0;

        for (int i = 0; i < n; i++) {
            String[] line = reader.readLine().trim().split(" ");
            long x = Long.parseLong(line[0]);
            long y = Long.parseLong(line[1]);
            throwsX -= x;
            throwsY -= y;
        }

        for (int i = 0; i < n; i++) {
            String[] line = reader.readLine().trim().split(" ");
            long x = Long.parseLong(line[0]);
            long y = Long.parseLong(line[1]);
            throwsX += x;
            throwsY += y;
        }

        long u = throwsX / n;
        long v = throwsY / n;

        System.out.println(u + " " + v);
    }
}
