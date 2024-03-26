package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

const charStates string = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ"

func min(a, b int64) int64 {
	if a > b {
		return b
	}
	return a
}

func max(a, b int64) int64 {
	if a < b {
		return b
	}
	return a
}

func update(tree []int64, v, tl, tr, l, r, add int64) {
	if l > r {
		return
	}
	if tl == l && tr == r {
		tree[v] += add
	} else {
		tm := (tl + tr) / 2
		update(tree, v*2, tl, tm, l, min(r, tm), add)
		update(tree, v*2+1, tm+1, tr, max(l, tm+1), r, add)
	}
}

func get(tree []int64, v, tl, tr, pos int64) int64 {
	if tl == tr {
		return tree[v]
	}
	tm := (tl + tr) / 2
	if pos <= tm {
		return tree[v] + get(tree, v*2, tl, tm, pos)
	} else {
		return tree[v] + get(tree, v*2+1, tm+1, tr, pos)
	}
}

func buildSegTree(n int64) []int64 {
	segTree := make([]int64, 4*n)
	return segTree
}

func getCode(char string) int {
	return strings.Index(charStates, char)
}

func main() {
	in := bufio.NewReader(os.Stdin)

	var s string
	var q, l, r, val int64

	fmt.Fscan(in, &s, &q)
	n := int64(len(s))

	segTree := buildSegTree(n)

	for i := int64(0); i < q; i++ {
		fmt.Fscan(in, &l, &r, &val)
		update(segTree, 1, 0, n-1, l-1, r-1, val)
	}

	for i := int64(0); i < n; i++ {
		initCode := getCode(s[i : i+1])
		toAdd := get(segTree, 1, 0, n-1, i)
		newCode := (int64(initCode) + toAdd) % int64(len(charStates))
		fmt.Print(charStates[newCode : newCode+1])
	}

}