package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	reader := bufio.NewReader(os.Stdin)

	var count int
	fmt.Fscan(reader, &count)

	x := make([]int, count)
	y := make([]int, count)
	pointsMap := make(map[int]map[int]bool)

	for i := 0; i < count; i++ {
		fmt.Fscan(reader, &x[i])
		fmt.Fscan(reader, &y[i])
		if pointsMap[x[i]] == nil {
			pointsMap[x[i]] = make(map[int]bool)
		}
		pointsMap[x[i]][y[i]] = true
	}

	max := 0
	for i := 0; i < count; i++ {
		for j := i + 1; j < count; j++ {
			if x[i] == x[j] || y[i] == y[j] {
				continue
			}

			if pointsMap[x[i]][y[j]] && pointsMap[x[j]][y[i]] {
				s := (x[i] - x[j]) * (y[i] - y[j])
				if s < 0 {
					s = -s
				}
				if s > max {
					max = s
				}
			}
		}
	}

	fmt.Println(max)
}
