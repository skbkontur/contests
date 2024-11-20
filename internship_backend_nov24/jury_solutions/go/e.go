package main

import (
	"bufio"
	"fmt"
	"os"
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

	m, n, q := Read3Int(scanner)

	matrix := make([]int, m*n)
	for i := range m {
		scanner.Scan()
		for j, char := range scanner.Text() {
			if char == '.' {
				matrix[i*n+j] = 0
			} else {
				matrix[i*n+j] = -1
			}
		}
	}

	ships := make([]int, 0)
	for y := range m {
		for x := range n {
			if matrix[y*n+x] == -1 {
				shipIdx := len(ships) + 1
				ships = append(ships, FillIdx(matrix, m, n, x, y, shipIdx))
			}
		}
	}

	for _ = range q {
		y, x := Read2Int(scanner)
		x -= 1
		y -= 1
		idx := y*n + x
		if matrix[idx] != 0 {
			ships[matrix[idx]-1] -= 1
			ship := ships[matrix[idx]-1]
			matrix[idx] = 0

			if ship == 0 {
				fmt.Printf("DESTROY\n")
			} else {
				fmt.Printf("HIT\n")
			}
		} else {
			fmt.Printf("MISS\n")
		}
	}

	return nil
}

func Print(matrix []int, m, n int) {
	for i := range m {
		for j := range n {
			fmt.Printf("%v", matrix[n*i+j])
		}
		fmt.Printf("\n")
	}
	fmt.Printf("\n")
}

func FillIdx(matrix []int, m, n, x, y, shipIdx int) int {
	if x < 0 || x >= n || y < 0 || y >= m {
		return 0
	}

	idx := y*n + x

	if matrix[idx] != -1 {
		return 0
	}

	matrix[idx] = shipIdx

	return FillIdx(matrix, m, n, x+1, y, shipIdx) +
		FillIdx(matrix, m, n, x, y+1, shipIdx) +
		FillIdx(matrix, m, n, x-1, y, shipIdx) +
		FillIdx(matrix, m, n, x, y-1, shipIdx) + 1
}

func Read3Int(scanner *bufio.Scanner) (a, b, c int) {
	scanner.Scan()
	fmt.Sscanf(scanner.Text(), "%d %d %d", &a, &b, &c)
	return
}

func Read2Int(scanner *bufio.Scanner) (a int, b int) {
	scanner.Scan()
	fmt.Sscanf(scanner.Text(), "%d %d", &a, &b)
	return
}
