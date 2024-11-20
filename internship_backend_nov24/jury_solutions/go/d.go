package main

import (
	"bufio"
	"cmp"
	"fmt"
	"os"
	"slices"
	"strings"
)

func main() {
	err := taskD()
	if err != nil {
		fmt.Printf("err: %s\n", err.Error())
	}
}

func taskD() error {
	scanner := bufio.NewScanner(os.Stdin)
	buf := make([]byte, 0, 64*1024)
	scanner.Buffer(buf, 20*1024*1024)

	n, m := Read2Int(scanner)
	_, _ = n, m

	u, v := Read2Int(scanner)
	us := ReadArray(scanner, u)
	vs := ReadArray(scanner, v)

	slices.Sort[[]int](us)
	slices.Sort[[]int](vs)

	q := ReadInt(scanner)

	for _ = range q {
		x_1, y_1, x_2, y_2 := Read4Int(scanner)

		rx_1 := GetRegion(us, x_1)
		ry_1 := GetRegion(vs, y_1)

		rx_2 := GetRegion(us, x_2)
		ry_2 := GetRegion(vs, y_2)

		if rx_1 == rx_2 && ry_1 == ry_2 {
			fmt.Printf("YES\n")
		} else {
			fmt.Printf("NO\n")
		}
	}

	return nil
}

func GetRegion(x []int, target int) int {
	n := len(x)
	i, j := 0, n
	for i < j {
		h := int(uint(i+j) >> 1)
		if cmp.Less(x[h], target) {
			i = h + 1
		} else {
			j = h
		}
	}
	return i
}

func Read4Int(scanner *bufio.Scanner) (x_1, y_1, x_2, y_2 int) {
	scanner.Scan()
	fmt.Sscanf(scanner.Text(), "%d %d %d %d", &x_1, &y_1, &x_2, &y_2)
	return
}

func ReadArray(scanner *bufio.Scanner, n int) []int {
	scanner.Scan()
	arr := strings.Split(scanner.Text(), " ")
	result := make([]int, 0, len(arr))
	for _, str := range arr {
		result = append(result, toInt(str))
	}
	return result
}

func ReadInt(scanner *bufio.Scanner) int {
	scanner.Scan()
	return toInt(scanner.Text())
}

func Read2Int(scanner *bufio.Scanner) (a int, b int) {
	scanner.Scan()
	fmt.Sscanf(scanner.Text(), "%d %d", &a, &b)
	return
}

func toInt(buf string) int {
	n := 0
	for _, v := range buf {
		n = n*10 + int(v-'0')
	}
	return n
}
