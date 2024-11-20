package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"slices"
)

func main() {
	err := TaskB()
	if err != nil {
		fmt.Printf("err: %s\n", err.Error())
	}
}

func TaskB() error {
	scanner := bufio.NewScanner(os.Stdin)

	var n int

	scanner.Scan()
	fmt.Sscanf(scanner.Text(), "%d", &n)

	firstX, firstY, err := ReadPointsFindMin(scanner, n)
	if err != nil {
		return err
	}

	secondX, secondY, err := ReadPointsFindMin(scanner, n)
	if err != nil {
		return err
	}

	fmt.Printf("%d %d\n", secondX-firstX, secondY-firstY)

	return nil
}

func ReadPointsFindMin(scanner *bufio.Scanner, n int) (int, int, error) {
	minX := math.MaxInt
	minY := math.MaxInt
	for _ = range n {
		scanner.Scan()

		bytes := scanner.Bytes()
		idx := slices.Index[[]byte](bytes, byte(' '))

		x := toInt(bytes[:idx])
		y := toInt(bytes[idx+1:])

		if x < minX {
			minX = x
			minY = y
		} else if x == minX && y < minY {
			minY = y
		}
	}

	return minX, minY, nil
}

func toInt(buf []byte) int {
	n := 0
	sign := 1
	if buf[0] == byte('-') {
		buf = buf[1:]
		sign = -1
	}
	for _, v := range buf {
		n = n*10 + int(v-'0')
	}
	return n * sign
}
