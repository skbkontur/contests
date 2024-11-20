import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.List;
import java.util.stream.IntStream;
import java.util.stream.Stream;

public class D {
    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        String[] firstLine = reader.readLine().split(" ");
        long n = Long.parseLong(firstLine[0]);
        long m = Long.parseLong(firstLine[1]);

        String[] secondLine = reader.readLine().split(" ");
        int u = Integer.parseInt(secondLine[0]);
        int v = Integer.parseInt(secondLine[1]);

        long[] uLines = Stream.concat(
                Arrays.stream(reader.readLine().split(" ")).limit(u).mapToLong(Long::parseLong).boxed(),
                Stream.of(0, n)
            ).mapToLong(Number::longValue)
            .sorted()
            .toArray();

        long[] vLines = Stream.concat(
                Arrays.stream(reader.readLine().split(" ")).limit(v).mapToLong(Long::parseLong).boxed(),
                Stream.of(0, m)
            ).mapToLong(Number::longValue)
            .sorted()
            .toArray();

        int q = Integer.parseInt(reader.readLine());

        List<TouristsPoints> coordinatesTourists = IntStream.range(0, q)
                .mapToObj(i -> TouristsPoints.readFromBufferedReader(reader))
                .toList();


        for (TouristsPoints point : coordinatesTourists) {
            int regionX1 = findLineIndex(uLines, point.x1);
            int regionY1 = findLineIndex(vLines, point.y1);
            int regionX2 = findLineIndex(uLines, point.x2);
            int regionY2 = findLineIndex(vLines, point.y2);

            if (regionX1 == regionX2 && regionY1 == regionY2) {
                System.out.println("YES");
            } else {
                System.out.println("NO");
            }
        }
    }

    private static int findLineIndex(long[] lines, long coordinate) {
        int index = Arrays.binarySearch(lines, coordinate);
        if (index < 0) {
            index = -(index + 1);
        }
        return index;
    }

    static class TouristsPoints {
        long x1, y1, x2, y2;

        TouristsPoints(long x1, long y1, long x2, long y2) {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        static TouristsPoints readFromBufferedReader(BufferedReader reader) {
            try {
                String[] readLines = reader.readLine().split(" ");
                long x1 = Long.parseLong(readLines[0]);
                long y1 = Long.parseLong(readLines[1]);
                long x2 = Long.parseLong(readLines[2]);
                long y2 = Long.parseLong(readLines[3]);
                return new TouristsPoints(x1, y1, x2, y2);
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }
    }
}