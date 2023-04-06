package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	reader := bufio.NewReader(os.Stdin)

	var n int
	fmt.Fscan(reader, &n)

	possible := make(map[int]bool)
	possible[0] = true

	for i := 0; i < n; i++ {
		newPossible := make(map[int]bool)

		for j := 0; j < 6; j++ {
			var next int
			fmt.Fscan(reader, &next)

			for k := range possible {
				newPossible[k+next] = true
			}
		}

		possible = newPossible
	}

	fmt.Println(len(possible))
}
