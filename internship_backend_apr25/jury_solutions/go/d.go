package main

import (
	"fmt"
	"math"
	"bufio"
	"os"
)

func main() {
	solve()
}

func solve() {
    reader := bufio.NewReader(os.Stdin)
    
	var n, m, k int
	fmt.Fscan(reader, &n, &m, &k)
	k++
	graph := make([][]struct {
		v int
		c int
	}, n)
	mx := 0

	for i := 0; i < m; i++ {
		var u, v, c int
		fmt.Fscan(reader, &u, &v, &c)
		u--
		v--
		graph[u] = append(graph[u], struct {
			v int
			c int
		}{v, c})
		mx = int(math.Max(float64(mx), float64(c)))
	}

	l, r := 1, mx+2
	ans := -1

	for l <= r {
		mid := (l + r) / 2

		if check(graph, k, mid) {
			r = mid - 1
			ans = mid
		} else {
			l = mid + 1
		}
	}

	fmt.Println(ans)
}

func checkGraph(graph [][]struct {
	v int
	c int
}, border int, order *[]int) bool {
	colors := make([]int, len(graph))
	res := false
	color := 1

	for i := range graph {
		if colors[i] != 0 {
			continue
		}
		res = res || topSortNoRec(graph, i, border, colors, order, color)
		color++
	}

	return res
}

func topSortNoRec(graph [][]struct {
	v int
	c int
}, v, border int, colors []int, order *[]int, curColor int) bool {
	dfs := []struct {
		visited bool
		node    int
	}{{false, v}}
	visited := make(map[int]struct{})
	visited[v] = struct{}{}

	for len(dfs) > 0 {
		node := dfs[len(dfs)-1]
		dfs = dfs[:len(dfs)-1]

		if node.visited {
			*order = append(*order, node.node)
			colors[node.node] = math.MinInt32
			continue
		}

		if colors[node.node] != 0 {
			continue
		}

		colors[node.node] = curColor
		dfs = append(dfs, struct {
			visited bool
			node    int
		}{true, node.node})

		for _, u := range graph[node.node] {
			if u.c > border {
				continue
			}

			if colors[u.v] == curColor {
				return true
			}

			if colors[u.v] == 0 {
				if _, ok := visited[u.v]; !ok {
					dfs = append(dfs, struct {
						visited bool
						node    int
					}{false, u.v})
					visited[u.v] = struct{}{}
				}
			}
		}
	}

	return false
}

func checkKLenPath(graph [][]struct {
	v int
	c int
}, k, border int, order []int) bool {
	if len(order) == 0 {
		return false
	}
	reverse(order)

	dp := make([]int, len(graph))
	maxLen := 0

	for _, v := range order {
		if dp[v] == 0 {
			dp[v] = 1
			maxLen = int(math.Max(float64(maxLen), 1))
		}

		for _, u := range graph[v] {
			if u.c > border {
				continue
			}
			dp[u.v] = int(math.Max(float64(dp[u.v]), float64(dp[v]+1)))
			maxLen = int(math.Max(float64(maxLen), float64(dp[u.v])))
		}
	}

	return maxLen >= k
}

func check(graph [][]struct {
	v int
	c int
}, k, border int) bool {
	var order []int
	return checkGraph(graph, border, &order) || checkKLenPath(graph, k, border, order)
}

func reverse(s []int) {
	for i, j := 0, len(s)-1; i < j; i, j = i+1, j-1 {
		s[i], s[j] = s[j], s[i]
	}
}
