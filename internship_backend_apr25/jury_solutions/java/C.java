import java.io.*;
import java.util.*;

public class C {
	public static void main(String[] args) throws Exception {
		new C().run();
	}

	enum State {
		New,
		Open,
		Close,
	}

	private class Colour {
		public Colour() {
			rightBorder = -1;
			leftBorder = Integer.MAX_VALUE;
			state = State.New;
		}
		
		public int rightBorder;
		public int leftBorder;
		public State state;
	}

	private final Map<Integer, Colour> colours = new HashMap<>();
	
	StreamTokenizer in;

	int nextInt() throws Exception {
		in.nextToken();
		return (int)in.nval;
	}

	public void run() throws Exception {
		in = new StreamTokenizer(new BufferedReader(new InputStreamReader(System.in)));
		int m = nextInt();
		int n = nextInt();
		int[] colorsLine = new int[m];
		for (int i = 0; i < m; i++)
			colorsLine[i] = nextInt() - 1;

		for (int i = 0; i < m; i++) {
			int colourId = colorsLine[i];
			Colour colour = new Colour();
			colours.putIfAbsent(colourId, colour);
			if (i < colours.get(colourId).leftBorder)
				colours.get(colourId).leftBorder = i;
			if (i > colours.get(colourId).rightBorder)
				colours.get(colourId).rightBorder = i;
		}

		Stack<Integer> stack = new Stack<>();
		List<Integer> coloursIds = new ArrayList<>(m);
		for (int i = 0; i < m; i++) {
			int colourId = colorsLine[i];
			switch (colours.get(colourId).state) {
				case New:
					stack.push(i);
					colours.get(colourId).state = State.Open;
					break;
				case Open:
					while (colorsLine[stack.peek()] != colourId) {
						coloursIds.add(colorsLine[stack.peek()]);
						colours.get(colorsLine[stack.pop()]).state = State.Close;
					}
					break;
				case Close:
					System.out.println(-1);
					return;
			}
		}

		while (!stack.isEmpty())
			coloursIds.add(colorsLine[stack.pop()]);

		System.out.println(coloursIds.size());
		Collections.reverse(coloursIds);
		for (int colourId : coloursIds)
			System.out.println((colourId + 1) + " " + (colours.get(colourId).leftBorder + 1) + " " + (colours.get(colourId).rightBorder + 1));
	}
}
