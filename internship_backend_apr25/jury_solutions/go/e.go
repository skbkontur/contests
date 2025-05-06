package main

import (
	"bufio"
	"fmt"
	"math/rand"
	"os"
	"strconv"
	"strings"
)

type TrieNode struct {
	next  map[rune]*TrieNode
	count int64
}

func NewTrieNode() *TrieNode {
	return &TrieNode{next: make(map[rune]*TrieNode)}
}

func (node *TrieNode) addString(s string, toAdd int64) *TrieNode {
	currentNode := node
	currentNode.count += toAdd

	for _, c := range s {
		if _, exists := currentNode.next[c]; !exists {
			currentNode.next[c] = NewTrieNode()
		}
		currentNode = currentNode.next[c]
		currentNode.count += toAdd
	}

	return currentNode
}

func (node *TrieNode) countPrefix(prefix string) int64 {
	currentNode := node

	for _, c := range prefix {
		if _, exists := currentNode.next[c]; !exists {
			return 0
		}
		currentNode = currentNode.next[c]
	}

	return currentNode.count
}

func (node *TrieNode) addPrefix(prefix string) *TrieNode {
	if node.count == 0 {
		return node
	}

	newRoot := NewTrieNode()
	leaf := newRoot.addString(prefix, node.count)

	for key, entry := range node.next {
		leaf.next[key] = entry
	}

	return newRoot
}

type CartesianNode struct {
	height int64
	count  int
	server *TrieNode
	left   *CartesianNode
	right  *CartesianNode
}

func NewCartesianNode() *CartesianNode {
	return &CartesianNode{height: rand.Int63(), count: 1, server: NewTrieNode()}
}

func getCount(node *CartesianNode) int {
	if node == nil {
		return 0
	}
	return node.count
}

func updateCount(node *CartesianNode) {
	if node != nil {
		node.count = getCount(node.left) + getCount(node.right) + 1
	}
}

func merge(left, right *CartesianNode) *CartesianNode {
	if left == nil {
		return right
	}
	if right == nil {
		return left
	}

	if left.height > right.height {
		left.right = merge(left.right, right)
		updateCount(left)
		return left
	}
	right.left = merge(left, right.left)
	updateCount(right)
	return right
}

func split(node *CartesianNode, key, add int) (*CartesianNode, *CartesianNode) {
	if node == nil {
		return nil, nil
	}

	currentKey := add + getCount(node.left)
	if key <= currentKey {
		left, right := split(node.left, key, add)
		node.left = right
		updateCount(node)
		return left, node
	}
	left, right := split(node.right, key, add+1+getCount(node.left))
	node.right = left
	updateCount(node)
	return node, right
}

func insert(node *CartesianNode, position int, insert *CartesianNode) *CartesianNode {
	left, right := split(node, position, 0)
	res := merge(left, insert)
	return merge(res, right)
}

func add(node *CartesianNode, position int) *CartesianNode {
	return insert(node, position, NewCartesianNode())
}

func (node *CartesianNode) get(index int) *CartesianNode {
	count := getCount(node.left) + 1
	if count == index {
		return node
	}
	if count < index {
		return node.right.get(index - count)
	}
	return node.left.get(index)
}

type FixedQueue struct {
	first int
	last  int
}

func (q *FixedQueue) add(index int) {
	q.first = q.last
	q.last = index
}

func main() {
	reader := bufio.NewReader(os.Stdin)
	line, _ := reader.ReadString('\n')
	ints := strings.Fields(line)
	clustersCount, _ := strconv.Atoi(ints[0])
	limit, _ := strconv.Atoi(ints[1])
	q, _ := strconv.Atoi(ints[2])

	clusters := make([]*CartesianNode, clustersCount)
	clustersLimits := make([]int, clustersCount)
	clustersQueries := make([]*FixedQueue, clustersCount)
	serversInit := make([]int, clustersCount)

	line, _ = reader.ReadString('\n')
	serversInitStrings := strings.Fields(line)
	for i := 0; i < clustersCount; i++ {
		serversInit[i], _ = strconv.Atoi(serversInitStrings[i])
		clustersQueries[i] = &FixedQueue{}
		if serversInit[i] != 0 {
			clusters[i] = NewCartesianNode()
		}
		for j := 1; j < serversInit[i]; j++ {
			clusters[i] = add(clusters[i], j)
		}
	}

	for t := 0; t < q; t++ {
		line, _ = reader.ReadString('\n')
		tokens := strings.Fields(line)
		typeChar := tokens[0][0]
		cluster, _ := strconv.Atoi(tokens[1])
		server, _ := strconv.Atoi(tokens[2])
		s := tokens[3]

		cluster--
		clustersLimits[cluster]++
		clustersQueries[cluster].add(server)
		currentServer := clusters[cluster].get(server)

		switch typeChar {
		case '+':
			currentServer.server.addString(s, 1)
		case 'p':
			currentServer.server = currentServer.server.addPrefix(s)
		case 'c':
			fmt.Println(currentServer.server.countPrefix(s))
		}

		if clustersLimits[cluster] == limit {
			clustersLimits[cluster] = 0
			nextCluster := (cluster + 1) % clustersCount
			from := min(clustersQueries[cluster].first, clustersQueries[cluster].last) - 1
			to := max(clustersQueries[cluster].first, clustersQueries[cluster].last)
			var left, middle, right *CartesianNode

			left, right = split(clusters[cluster], to, 0)
			left, middle = split(left, from, 0)

			clusters[cluster] = merge(left, right)

			if clusters[nextCluster] == nil {
				clusters[nextCluster] = middle
			} else {
				clusters[nextCluster] = insert(clusters[nextCluster], clusters[nextCluster].count/2, middle)
			}
		}
	}
}
