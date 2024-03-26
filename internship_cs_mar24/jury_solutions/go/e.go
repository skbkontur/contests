package main

import (
	"bufio"
	"fmt"
	"os"
	"sort"
)

func binarySearchOnRange(arr []int, a, b int) bool {
	low, high := 0, len(arr)-1

	for low <= high {
		mid := (low + high) / 2

		if arr[mid] >= a && arr[mid] <= b {
			return true
		} else if arr[mid] < a {
			low = mid + 1
		} else {
			high = mid - 1
		}
	}

	return false
}

// O(n * log(n))
func main() {
	in := bufio.NewReader(os.Stdin)

	var n int
	fmt.Fscan(in, &n)

	x := make(map[int][]int, n)
	y := make(map[int][]int, n)

	for i := 0; i < n; i++ {
		var xi, yi int
		fmt.Fscan(in, &xi, &yi)

		arr := x[xi]
		arr = append(arr, yi)
		x[xi] = arr

		arr = y[yi]
		arr = append(arr, xi)
		y[yi] = arr
	}

	for _, arr := range x {
		sort.Slice(arr, func(i, j int) bool {
			return arr[i] < arr[j]
		})
	}

	for _, arr := range y {
		sort.Slice(arr, func(i, j int) bool {
			return arr[i] < arr[j]
		})
	}

	var q int
	fmt.Fscan(in, &q)

	var x0, y0 int

	for i := 0; i < q; i++ {
		var dir string
		var k int

		fmt.Fscan(in, &dir, &k)

		switch dir {
		case "U":
			if yi, inX := x[x0]; inX && binarySearchOnRange(yi, y0, y0+k) {
				fmt.Println("Stop ", i+1)
				return
			}

			y0 += k

		case "D":
			if yi, inX := x[x0]; inX && binarySearchOnRange(yi, y0-k, y0) {
				fmt.Println("Stop ", i+1)
				return
			}

			y0 -= k

		case "R":
			if xi, inY := y[y0]; inY && binarySearchOnRange(xi, x0, x0+k) {
				fmt.Println("Stop ", i+1)
				return
			}

			x0 += k

		case "L":
			if xi, inY := y[y0]; inY && binarySearchOnRange(xi, x0-k, x0) {
				fmt.Println("Stop ", i+1)
				return
			}

			x0 -= k
		}
	}

	fmt.Println("Complete")
}