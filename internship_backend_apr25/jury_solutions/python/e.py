import sys
from collections import defaultdict

class Program:
    @staticmethod
    def main():
        Program.solve()

    @staticmethod
    def solve():
        input_data = sys.stdin.read().splitlines()
        clusters_count, limit, q = map(int, input_data[0].split())
        servers_init = list(map(int, input_data[1].split()))

        clusters = [None] * clusters_count
        clusters_limits = [0] * clusters_count
        clusters_queries = [FixedQueue() for _ in range(clusters_count)]

        for i in range(clusters_count):
            servers_count = servers_init[i]
            if servers_count != 0:
                clusters[i] = CartesianNode()

            for j in range(1, servers_count):
                clusters[i] = CartesianNode.add(clusters[i], j)

        for t in range(q):
            line = input_data[t + 2].split()
            type_query = line[0][0]
            cluster = int(line[1]) - 1
            server = int(line[2])
            s = line[3]

            clusters_limits[cluster] += 1
            clusters_queries[cluster].add(server)
            current_server = clusters[cluster][server]

            if type_query == '+':
                current_server.server.add_string(s)
            elif type_query == 'p':
                current_server.server = current_server.server.add_prefix(s)
            elif type_query == 'c':
                print(current_server.server.count_prefix(s))

            if clusters_limits[cluster] == limit:
                clusters_limits[cluster] = 0
                next_cluster = (cluster + 1) % clusters_count
                from_index = min(clusters_queries[cluster].first, clusters_queries[cluster].last) - 1
                to_index = max(clusters_queries[cluster].first, clusters_queries[cluster].last)
                
                left, middle, right = None, None, None
                
                splitted = CartesianNode.split(clusters[cluster], to_index)
                left, right = splitted

                splitted = CartesianNode.split(left, from_index)
                left, middle = splitted

                clusters[cluster] = CartesianNode.merge(left, right)

                if clusters[next_cluster] is None:
                    clusters[next_cluster] = middle
                else:
                    clusters[next_cluster] = CartesianNode.insert(clusters[next_cluster], clusters[next_cluster].count // 2, middle)

class TrieNode:
    def __init__(self):
        self.next = {}
        self.count = 0

    def add_string(self, s, to_add=1):
        current_node = self
        current_node.count += to_add

        for c in s:
            if c not in current_node.next:
                current_node.next[c] = TrieNode()
            current_node = current_node.next[c]
            current_node.count += to_add

        return current_node

    def count_prefix(self, prefix):
        current_node = self

        for c in prefix:
            if c not in current_node.next:
                return 0
            current_node = current_node.next[c]

        return current_node.count

    def add_prefix(self, prefix):
        if self.count == 0:
            return self

        new_root = TrieNode()
        leaf = new_root.add_string(prefix, self.count)

        for i in self.next.items():
            leaf.next[i[0]] = i[1]

        return new_root

class CartesianNode:
    rand = __import__('random').Random()

    def __init__(self):
        self.height = CartesianNode.rand.randint(0, 1000000)
        self.count = 1
        self.server = TrieNode()
        self.left = None
        self.right = None

    @staticmethod
    def get_count(node):
        return 0 if node is None else node.count

    @staticmethod
    def update_count(node):
        if node is not None:
            node.count = CartesianNode.get_count(node.left) + CartesianNode.get_count(node.right) + 1

    @staticmethod
    def merge(left, right):
        if left is None:
            return right
        if right is None:
            return left

        if left.height > right.height:
            left.right = CartesianNode.merge(left.right, right)
            CartesianNode.update_count(left)
            return left
        else:
            right.left = CartesianNode.merge(left, right.left)
            CartesianNode.update_count(right)
            return right

    @staticmethod
    def split(node, key, add=0):
        if node is None:
            return None, None

        current_key = add + CartesianNode.get_count(node.left)
        if key <= current_key:
            splitted = CartesianNode.split(node.left, key, add)
            new_left, new_right = splitted
            node.left = new_right
            CartesianNode.update_count(node)
            return new_left, node
        else:
            splitted = CartesianNode.split(node.right, key, add + 1 + CartesianNode.get_count(node.left))
            new_left, new_right = splitted
            node.right = new_left
            CartesianNode.update_count(node)
            return node, new_right

    @staticmethod
    def insert(node, position, insert):
        splitted = CartesianNode.split(node, position)
        left, right = splitted
        res = CartesianNode.merge(left, insert)
        return CartesianNode.merge(res, right)

    @staticmethod
    def add(node, position):
        return CartesianNode.insert(node, position, CartesianNode())

    def __getitem__(self, index):
        count = CartesianNode.get_count(self.left) + 1
        if count == index:
            return self
        return self.right[index - count] if count < index else self.left[index]

class FixedQueue:
    def __init__(self):
        self.first = 0
        self.last = 0

    def add(self, index):
        self.first = self.last
        self.last = index


Program.main()
