import java.util.*;
import java.util.stream.Collectors;

public class A {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        int n = scanner.nextInt();

        int maxUniqueCount = 0;
        var mostImpressiveString = "";

        for (int i = 0; i < n; i++) {
            String currentString = scanner.next();

            var uniqueChars = currentString.chars()
                    .mapToObj(ch -> (char) ch)
                    .collect(Collectors.toCollection(HashSet::new));

            if (uniqueChars.size() > maxUniqueCount) {
                maxUniqueCount = uniqueChars.size();
                mostImpressiveString = currentString;
            }
        }

        System.out.println(maxUniqueCount + " " + mostImpressiveString);
        scanner.close();
    }
 }
