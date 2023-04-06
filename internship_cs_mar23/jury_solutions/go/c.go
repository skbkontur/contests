package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	reader := bufio.NewReader(os.Stdin)

	var n, k int
	fmt.Fscan(reader, &n)
	fmt.Fscan(reader, &k)

	a := make([]int, n)
	isZero := make([]int, n)

	for i := 0; i < n; i++ {
		fmt.Fscan(reader, &a[i])
		if a[i] == 0 {
			isZero[i] = 1
		} else {
			isZero[i] = 0
		}
	}

	finish := -1
	zeroesCount := 0
	sum := 0
	ans := 0
	for start := 0; start < n; start++ {
		for (finish < n-1) && (a[finish+1]+sum <= k) && (isZero[finish+1]+zeroesCount <= 1) {
			sum += a[finish+1]
			zeroesCount += isZero[finish+1]
			finish++
		}

		ans += finish - start + 1
		sum -= a[start]
		zeroesCount -= isZero[start]

		if finish < start {
			sum += a[finish+1]
			zeroesCount += isZero[finish+1]
			finish++
		}
	}

	fmt.Println(ans)
}
