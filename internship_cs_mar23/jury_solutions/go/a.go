package main

import (
	"bufio"
	"fmt"
	"os"
)

func main() {
	var count int

	reader := bufio.NewReader(os.Stdin)
	fmt.Fscan(reader, &count)
	brNums := make([]int, count)

	for i := 0; i < count; i++ {
		var br int
		fmt.Fscan(reader, &br)
		brNums[i] = br
	}

	iMin := 0
	iMax := 0
	min := brNums[0]
	max := brNums[0]

	for i, num := range brNums {
		if num < min {
			min = num
			iMin = i
		}
		if num >= max {
			max = num
			iMax = i
		}
	}

	fmt.Println(iMax + 1, iMin + 1)
}
