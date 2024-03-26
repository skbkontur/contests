package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	var n, t int64
	fmt.Scan(&n, &t)

	in := bufio.NewReader(os.Stdin)

	var sum_a, sum_b int64
	var a, b int64
	for i := int64(0); i < n; i++ {
		fmt.Fscan(in, &a)
		sum_a += a
	}

	for i := int64(0); i < n; i++ {
		fmt.Fscan(in, &b)
		sum_b += b
	}

	if t >= sum_a && t <= sum_b {
		fmt.Println("YES")
	} else {
		fmt.Println("NO")
	}
}