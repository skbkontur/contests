import java.io.*;
import java.util.*;

public class E {
	public static void main(String[] args) throws Exception {
		new E().run();
	}
	
	public void run() throws Exception {
		BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
		int[] ints = Arrays.stream(reader.readLine().split(" ")).mapToInt(Integer::parseInt).toArray();
		int clustersCount = ints[0];
		int limit = ints[1];
		int q = ints[2];

		List<CartesianNode> clusters = new ArrayList<>(clustersCount);
		List<Integer> clustersLimits = new ArrayList<>(Collections.nCopies(clustersCount, 0));
		List<FixedQueue> clustersQueries = new ArrayList<>(clustersCount);
		int[] serversInit = Arrays.stream(reader.readLine().split(" ")).mapToInt(Integer::parseInt).toArray();

		for (int i = 0; i < clustersCount; i++) {
			clustersQueries.add(new FixedQueue());
			clusters.add(null);

			int serversCount = serversInit[i];
			if (serversCount != 0)
				clusters.set(i, new CartesianNode());

			for (int j = 1; j < serversCount; j++) clusters.set(i, CartesianNode.add(clusters.get(i), j));
		}

		for (int t = 0; t < q; t++) {
			String[] tokens = reader.readLine().split(" ");
			char type = tokens[0].charAt(0);
			int cluster = Integer.parseInt(tokens[1]) - 1;
			int server = Integer.parseInt(tokens[2]);
			String s = tokens[3];

			clustersLimits.set(cluster, clustersLimits.get(cluster) + 1);
			clustersQueries.get(cluster).add(server);
			CartesianNode currentServer = clusters.get(cluster).get(server);

			switch (type) {
				case '+':
					currentServer.server.addString(s, 1);
					break;
				case 'p':
					currentServer.server = currentServer.server.addPrefix(s);
					break;
				case 'c':
					System.out.println(currentServer.server.countPrefix(s));
					break;
			}

			if (clustersLimits.get(cluster) == limit) {
				clustersLimits.set(cluster, 0);
				int nextCluster = (cluster + 1) % clustersCount;
				int from = Math.min(clustersQueries.get(cluster).first, clustersQueries.get(cluster).last) - 1;
				int to = Math.max(clustersQueries.get(cluster).first, clustersQueries.get(cluster).last);
				CartesianNode left, middle, right;

				var splitted = CartesianNode.split(clusters.get(cluster), to, 0);
				left = splitted[0];
				right = splitted[1];

				splitted = CartesianNode.split(left, from, 0);
				left = splitted[0];
				middle = splitted[1];

				clusters.set(cluster, CartesianNode.merge(left, right));

				if (clusters.get(nextCluster) == null)
					clusters.set(nextCluster, middle);
				else
					clusters.set(nextCluster, CartesianNode.insert(clusters.get(nextCluster), clusters.get(nextCluster).count / 2, middle));
			}
		}
	}

	static class TrieNode {
		Map<Character, TrieNode> next = new HashMap<>();
		long count = 0;

		public TrieNode addString(String s, long toAdd) {
			TrieNode currentNode = this;
			currentNode.count += toAdd;

			for (char c : s.toCharArray()) {
				currentNode.next.putIfAbsent(c, new TrieNode());
				currentNode = currentNode.next.get(c);
				currentNode.count += toAdd;
			}

			return currentNode;
		}

		public long countPrefix(String prefix) {
			TrieNode currentNode = this;

			for (char c : prefix.toCharArray()) {
				if (!currentNode.next.containsKey(c))
					return 0;

				currentNode = currentNode.next.get(c);
			}

			return currentNode.count;
		}

		public TrieNode addPrefix(String prefix) {
			if (count == 0)
				return this;

			TrieNode newRoot = new TrieNode();
			TrieNode leaf = newRoot.addString(prefix, count);

			for (var entry : next.entrySet())
				leaf.next.put(entry.getKey(), entry.getValue());

			return newRoot;
		}
	}

	static class CartesianNode {
		public long height = new Random().nextLong();
		public int count = 1;
		public TrieNode server = new TrieNode();
		private CartesianNode left, right;

		public static int getCount(CartesianNode node) {
			return node == null ? 0 : node.count;
		}

		public static void updateCount(CartesianNode node) {
			if (node != null)
				node.count = getCount(node.left) + getCount(node.right) + 1;
		}

		public static CartesianNode merge(CartesianNode left, CartesianNode right) {
			if (left == null) return right;
			if (right == null) return left;

			if (left.height > right.height) {
				left.right = merge(left.right, right);
				updateCount(left);
				return left;
			} else {
				right.left = merge(left, right.left);
				updateCount(right);
				return right;
			}
		}

		public static CartesianNode[] split(CartesianNode node, int key, int add) {
			if (node == null) return new CartesianNode[]{null, null};

			int currentKey = add + getCount(node.left);
			if (key <= currentKey) {
				CartesianNode[] splitted = split(node.left, key, add);
				CartesianNode newLeft = splitted[0];
				CartesianNode newRight = splitted[1];
				node.left = newRight;
				updateCount(node);
				return new CartesianNode[]{newLeft, node};
			} else {
				CartesianNode[] splitted = split(node.right, key, add + 1 + getCount(node.left));
				CartesianNode newLeft = splitted[0];
				CartesianNode newRight = splitted[1];
				node.right = newLeft;
				updateCount(node);
				return new CartesianNode[]{node, newRight};
			}
		}

		public static CartesianNode insert(CartesianNode node, int position, CartesianNode insert) {
			CartesianNode[] splitted = split(node, position, 0);
			CartesianNode left = splitted[0];
			CartesianNode right = splitted[1];
			CartesianNode res = merge(left, insert);
			return merge(res, right);
		}

		public static CartesianNode add(CartesianNode node, int position) {
			return insert(node, position, new CartesianNode());
		}

		public CartesianNode get(int index) {
			int count = getCount(left) + 1;
			if (count == index)
				return this;
			return count < index ? right.get(index - count) : left.get(index);
		}
	}

	static class FixedQueue {
		public int first;
		public int last;

		public void add(int index) {
			first = last;
			last = index;
		}
	}
}
