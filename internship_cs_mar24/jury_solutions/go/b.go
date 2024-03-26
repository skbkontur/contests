package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
)

const inf int = 10e6

func getFactors(n int) []int {
	factors := make([]int, 0, int(math.Sqrt(float64(n))))

	for i := 1; i*i <= n; i++ {
		if n%i == 0 {
			factors = append(factors, i)
			if i != n/i {
				factors = append(factors, n/i)
			}
		}
	}

	return factors
}

func getMinChanges(str string, prev byte, factor int) int {
	var curMinChanges, countZero, countOne, i int
	for ; i < len(str); i++ {
		if i%factor == 0 {
			if prev == '0' {
				curMinChanges += countZero
			} else {
				curMinChanges += countOne
			}

			countZero = 0
			countOne = 0

			if i > 0 {
				if prev == '0' {
					prev = '1'
				} else {
					prev = '0'
				}
			}
		}

		if str[i] == '0' {
			countZero++
		} else {
			countOne++
		}
	}

	if i%factor == 0 {
		if prev == '0' {
			curMinChanges += countZero
		} else {
			curMinChanges += countOne
		}
	}

	return curMinChanges
}

func minInt(x, y int) int {
	if x < y {
		return x
	}
	return y
}

func main() {
	in := bufio.NewReader(os.Stdin)

	var str string
	fmt.Fscan(in, &str)

	minChanges := inf

	for _, factor := range getFactors(len(str)) {
		minChanges = minInt(minChanges, getMinChanges(str, '0', factor))
		minChanges = minInt(minChanges, getMinChanges(str, '1', factor))
	}

	fmt.Println(minChanges)
}