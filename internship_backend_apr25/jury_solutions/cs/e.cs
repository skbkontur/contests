using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        Solve();
    }

    public static void Solve()
    {
        var input = Console.ReadLine().Split();
        var clustersCount = int.Parse(input[0]);
        var limit = int.Parse(input[1]);
        var q = int.Parse(input[2]);
        
        var clusters = new List<CartesianNode>(clustersCount);
        var clustersLimits = new List<int>(clustersCount);
        var clustersQueries = new List<FixedQueue>(clustersCount);
        var serversInit = Console.ReadLine().Split().Select(int.Parse).ToArray();
        
        
        for (int i = 0; i < clustersCount; i++)
        {
            clustersQueries.Add(new FixedQueue());
            clustersLimits.Add(0);
            clusters.Add(null);

            var serversCount = serversInit[i];
            if (serversCount != 0)
                clusters[i] = new CartesianNode();

            for (int j = 1; j < serversCount; j++) clusters[i] = CartesianNode.Add(clusters[i], j);
        }
        
        for (int t = 0; t < q; t++)
        {
            var line = Console.ReadLine().Split();
            var type = line[0][0];
            var cluster = int.Parse(line[1]) - 1;
            var server = int.Parse(line[2]);
            var s = line[3];

            clustersLimits[cluster] += 1;
            clustersQueries[cluster].Add(server);
            var currentServer = clusters[cluster][server];

            switch (type)
            {
                case '+':
                    currentServer.Server.AddString(s);
                    break;
                case 'p':
                    currentServer.Server = currentServer.Server.AddPrefix(s);
                    break;
                case 'c':
                    Console.WriteLine(currentServer.Server.CountPrefix(s));
                    break;
            }

            if (clustersLimits[cluster] == limit)
            {
                clustersLimits[cluster] = 0;
                var nextCluster = (cluster + 1) % clustersCount;
                var from = Math.Min(clustersQueries[cluster].First, clustersQueries[cluster].Last) - 1;
                var to = Math.Max(clustersQueries[cluster].First, clustersQueries[cluster].Last);
                CartesianNode left, middle, right;
                
                var splitted = CartesianNode.Split(clusters[cluster], to);
                left = splitted.Item1;
                right = splitted.Item2;
                
                splitted = CartesianNode.Split(left, from);
                left = splitted.Item1;
                middle = splitted.Item2;
                
                clusters[cluster] = CartesianNode.Merge(left, right);

                if (clusters[nextCluster] == null)
                    clusters[nextCluster] = middle;
                else
                    clusters[nextCluster] =
                        CartesianNode.Insert(clusters[nextCluster], clusters[nextCluster].Count / 2, middle);
            }
        }
    }

    class TrieNode
    {
        Dictionary<char, TrieNode> Next = new Dictionary<char, TrieNode>();
        public long Count { get; set; } = 0;

        public TrieNode AddString(string s, long toAdd = 1)
        {
            var currentNode = this;
            currentNode.Count += toAdd;

            foreach (var c in s)
            {
                if (!currentNode.Next.TryGetValue(c, out var value))
                {
                    value = new TrieNode();
                    currentNode.Next[c] = value;
                }
                currentNode = value;
                currentNode.Count += toAdd;
            }
            
            return currentNode;
        }
        
        public long CountPrefix(string prefix)
        {
            var currentNode = this;

            foreach (var c in prefix)
            {
                if (!currentNode.Next.TryGetValue(c, out var value))
                    return 0;
                
                currentNode = value;
            }
            
            return currentNode.Count;
        }
        
        public TrieNode AddPrefix(string prefix)
        {
            if (Count == 0)
                return this;
            
            var newRoot = new TrieNode();
            TrieNode leaf = newRoot.AddString(prefix, Count);

            foreach (var i in Next) leaf.Next[i.Key] = i.Value;
            
            return newRoot;
        }
    }

    class CartesianNode
    {
        public long Height = rand.Next();
        public int Count = 1;
        public TrieNode Server = new TrieNode();
        private CartesianNode Left, Right;

        private static Random rand = new Random();
        
        public static int GetCount(CartesianNode node) => node == null ? 0 : node.Count;

        public static void UpdateCount(CartesianNode node)
        {
            if (node != null)
                node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;
        }

        public static CartesianNode Merge(CartesianNode left, CartesianNode right)
        {
            if (left == null) return right;
            if (right == null) return left;

            if (left.Height > right.Height)
            {
                left.Right = Merge(left.Right, right);
                UpdateCount(left);
                return left;
            }
            else
            {
                right.Left = Merge(left, right.Left);
                UpdateCount(right);
                return right;
            }
        }

        public static Tuple<CartesianNode, CartesianNode> Split(CartesianNode node, int key, int add = 0)
        {
            if (node == null) return Tuple.Create<CartesianNode, CartesianNode>(null, null);

            var currentKey = add + GetCount(node.Left);
            if (key <= currentKey)
            {
                var splitted = Split(node.Left, key, add);
                var newLeft = splitted.Item1;
                var newRight = splitted.Item2;
                node.Left = newRight;
                UpdateCount(node);
                return Tuple.Create<CartesianNode, CartesianNode>(newLeft, node);
            }
            else
            {
                var splitted = Split(node.Right, key, add + 1 + GetCount(node.Left));
                var newLeft = splitted.Item1;
                var newRight = splitted.Item2;
                node.Right = newLeft;
                UpdateCount(node);
                return Tuple.Create<CartesianNode, CartesianNode>(node, newRight);
            }
        }

        public static CartesianNode Insert(CartesianNode node, int position, CartesianNode insert)
        {
            var splitted = Split(node, position);
            var left = splitted.Item1;
            var right = splitted.Item2;
            var res = Merge(left, insert);
            return Merge(res, right);
        }

        public static CartesianNode Add(CartesianNode node, int position) =>
            Insert(node, position, new CartesianNode());

        public CartesianNode this[int index]
        {
            get
            {
                var count = GetCount(Left) + 1;
                if (count == index)
                    return this;
                return count < index ? Right[index - count] : Left[index];
            }
        }
    }


    class FixedQueue
    {
        public int First => first;
        public int Last => last;
        private int first, last;

        public void Add(int index)
        {
            first = last;
            last = index;
        }
    }
}
