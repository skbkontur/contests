import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.Comparator;
import java.util.Scanner;
import java.util.TreeSet;

public class TaskE {

    public String inspectRobotBabyWay() {
        return inspectRobotBabyWay(System.in);
    }

    String inspectRobotBabyWay(String input) {
        return inspectRobotBabyWay(new ByteArrayInputStream(input.getBytes(StandardCharsets.UTF_8)));
    }

    private String inspectRobotBabyWay(InputStream inputStream) {
        try (Scanner scanner = new Scanner(inputStream)) {
            return inspectRobotBabyWay(scanner);
        }
    }

    private String inspectRobotBabyWay(Scanner scanner) {

        int n = scanner.nextInt();

        TreeSet<Barrier> barriersXThenY = new TreeSet<>(Barrier.xThenYComparator);
        TreeSet<Barrier> barriersYThenX = new TreeSet<>(Barrier.yThenXComparator);
        for (int i = 0; i < n; i++) {
            long x = scanner.nextInt();
            long y = scanner.nextInt();

            Barrier barrier = new Barrier(x, y);
            barriersXThenY.add(barrier);
            barriersYThenX.add(barrier);
        }

        int q = scanner.nextInt();
        Position position = new Position();
        for (int i = 0; i < q; i++) {
            Direction direction = Direction.fromStringCode(scanner.next());
            int steps = scanner.nextInt();

            Position nextPosition = position.moveTo(direction, steps);

            if (direction.isVertical()) {
                Barrier highestOfCurrentAndNextPositions = new Barrier(
                        position.x(), Long.max(position.y(), nextPosition.y())
                );

                Barrier lowestOfCurrentAndNextPositions = new Barrier(
                        position.x(), Long.min(position.y(), nextPosition.y())
                );

                if (!barriersXThenY
                        .subSet(lowestOfCurrentAndNextPositions, true, highestOfCurrentAndNextPositions, true)
                        .isEmpty()
                ) {
                    return "Stop " + (i + 1);
                }

            } else {
                Barrier rightmostOfCurrentAndNextPosition = new Barrier(
                        Long.max(position.x(), nextPosition.x()), position.y()
                );

                Barrier leftmostOfCurrentAndNextPositions = new Barrier(
                        Long.min(position.x(), nextPosition.x()), position.y()
                );


                if (!barriersYThenX
                        .subSet(leftmostOfCurrentAndNextPositions, true, rightmostOfCurrentAndNextPosition, true)
                        .isEmpty()
                ) {
                    return "Stop " + (i + 1);
                }
            }

            position = nextPosition;
        }

        return "Complete";
    }

    private static final class Barrier {
        private final Position position;

        private Barrier(Position position) {
            this.position = position;
        }
        public Barrier(long x, long y) {
            this(new Position(x, y));
        }

        private long x() {
            return position.x();
        }

        private long y() {
            return position.y();
        }

        public static final Comparator<Barrier> xThenYComparator = Comparator
                .comparing(Barrier::x)
                .thenComparing(Barrier::y);

        public static final Comparator<Barrier> yThenXComparator = Comparator
                .comparing(Barrier::y)
                .thenComparing(Barrier::x);
    }

    private static final class Position {
        private final long x;
        private final long y;

        private Position(long x, long y) {
            this.x = x;
            this.y = y;
        }

        public Position() {
            this(0, 0);
        }

        public Position moveTo(Direction direction, long steps) {
            long x = this.x;
            long y = this.y;

            switch (direction) {
                case UP:
                    y += steps;
                    break;
                case DOWN:
                    y -= steps;
                    break;
                case LEFT:
                    x -= steps;
                    break;
                case RIGHT:
                    x += steps;
                    break;
                default:
                    throw new IllegalArgumentException();
            }

            return new Position(x, y);
        }

        public long x() {
            return x;
        }

        public long y() {
            return y;
        }
    }

    private enum Direction {
        UP, DOWN, LEFT, RIGHT;

        public boolean isVertical() {
            switch (this) {
                case UP:
                case DOWN:
                    return true;
                case LEFT:
                case RIGHT:
                    return false;
                default:
                    throw new IllegalArgumentException();
            }
        }

        public static Direction fromStringCode(String code) {
            switch (code) {
                case "U":
                    return UP;
                case "D":
                    return DOWN;
                case "L":
                    return LEFT;
                case "R":
                    return RIGHT;
                default:
                    throw new IllegalArgumentException();
            }
        }
    }

    public static void main(String[] args) {
        String result = new TaskE().inspectRobotBabyWay();
        System.out.print(result);
    }
}
