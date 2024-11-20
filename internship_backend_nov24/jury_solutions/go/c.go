package main

import (
	"fmt"
	"math"
)

func main() {
	err := TaskC()
	if err != nil {
		fmt.Printf("err: %s\n", err.Error())
	}
}

func TaskC() error {
	n := ReadInt()
	k := ReadInt()
	q := ReadInt()

	array, count := readArray(n, k)

	if q >= len(array) {
		fmt.Printf("%d\n", n)
		return nil
	}

	array = append(array, count+1)

	sumBuildings := 0
	for i := range q {
		sumBuildings += array[i]
	}

	maxSum := math.MinInt
	for i := q; i < len(array); i++ {
		sumBuildings += array[i] - 1

		if sumBuildings > maxSum {
			maxSum = sumBuildings
		}
		sumBuildings -= array[i-q]
		sumBuildings += 1
	}

	fmt.Printf("%d\n", maxSum)

	return nil
}

func readArray(n, k int) ([]int, int) {
	array := make([]int, 0, n)
	count := 0

	for _ = range n {
		h := ReadInt()

		count += 1

		if h >= k {
			array = append(array, count)
			count = 0
		}
	}

	return array, count
}

func ReadInt() int {
	var input string
	fmt.Scan(&input)
	return toInt(input)
}

func toInt(buf string) int {
	n := 0
	for _, v := range buf {
		n = n*10 + int(v-'0')
	}
	return n
}
