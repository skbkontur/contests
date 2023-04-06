package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	reader := bufio.NewReader(os.Stdin)

	const MAXN = 1400

	var n, m int
	fmt.Fscan(reader, &n)
	fmt.Fscan(reader, &m)
	reader.ReadString('\n')

	var f [MAXN + 2][MAXN + 2]byte

	for i := 1; i <= n; i++ {
		line, _ := reader.ReadString('\n')
		for j := 1; j <= m; j++ {
			f[i][j] = line[j-1] - '0'
		}
	}

	var ul [MAXN + 2][MAXN + 2]byte
	for i := 1; i <= n; i++ {
		for j := 1; j <= m; j++ {
			ul[i][j] = max(f[i][j], max(ul[i-1][j], ul[i][j-1]))
		}
	}

	var ur [MAXN + 2][MAXN + 2]byte
	for i := 1; i <= n; i++ {
		for j := m; j >= 1; j-- {
			ur[i][j] = max(f[i][j], max(ur[i-1][j], ur[i][j+1]))
		}
	}

	var dl [MAXN + 2][MAXN + 2]byte
	for i := n; i >= 1; i-- {
		for j := 1; j <= m; j++ {
			dl[i][j] = max(f[i][j], max(dl[i+1][j], dl[i][j-1]))
		}
	}

	var dr [MAXN + 2][MAXN + 2]byte
	for i := n; i >= 1; i-- {
		for j := m; j >= 1; j-- {
			dr[i][j] = max(f[i][j], max(dr[i+1][j], dr[i][j+1]))
		}
	}

	var ans byte = 10
	ans_x := -1
	ans_y := -1

	for i := 1; i <= n; i++ {
		for j := 1; j <= m; j++ {
			cur_max := max(
				max(ul[i-1][j-1], ur[i-1][j+1]),
				max(dl[i+1][j-1], dr[i+1][j+1]))

			if cur_max < ans {
				ans = cur_max
				ans_x = i
				ans_y = j
			}
		}
	}

	fmt.Println(ans_x, ans_y)
}

func max(x, y byte) byte {
	if x < y {
		return y
	}
	return x
}
