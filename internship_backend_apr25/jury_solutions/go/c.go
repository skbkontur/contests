package main

import (
	"fmt"
	"os"
	"slices"
	"strconv"
	"strings"
)
import "bufio"

type Colour struct {
	rightBorder int
	leftBorder  int
	state       State
}

type State int

const (
	New State = iota
	Open
	Close
)

func main() {
	scanner := bufio.NewScanner(os.Stdin)
	scanner.Buffer(make([]byte, 5000000), 5000000)

	var m, n int
	scanner.Scan()
	fmt.Sscan(scanner.Text(), &m, &n)
	coloursLine := make([]int, 0)
	scanner.Scan()
	line := scanner.Text()
	for _, colourString := range strings.Split(line, " ") {
		colourId, _ := strconv.Atoi(colourString)
		coloursLine = append(coloursLine, colourId-1)
	}

	colours := make(map[int]*Colour)
	for i := 0; i < m; i++ {
		colourId := coloursLine[i]
		if _, exists := colours[colourId]; !exists {
			colours[colourId] = &Colour{rightBorder: -1, leftBorder: 1000000000, state: New}
		}

		colour, _ := colours[colourId]
		if i < colour.leftBorder {
			colour.leftBorder = i
		}
		if i > colour.rightBorder {
			colour.rightBorder = i
		}
	}

	stack := make([]int, 0)
	coloursIds := make([]int, 0)

	for i := 0; i < m; i++ {
		colourIndex := coloursLine[i]
		switch colour, _ := colours[colourIndex]; colour.state {
		case New:
			stack = append(stack, i)
			colour.state = Open
		case Open:
			for coloursLine[stack[len(stack)-1]] != colourIndex {
				coloursIds = append(coloursIds, coloursLine[stack[len(stack)-1]])
				colour2, _ := colours[coloursLine[stack[len(stack)-1]]]
				colour2.state = Close
				stack = stack[:len(stack)-1]
			}
		case Close:
			fmt.Println(-1)
			return
		}
	}

	for len(stack) > 0 {
		coloursIds = append(coloursIds, coloursLine[stack[len(stack)-1]])
		stack = stack[:len(stack)-1]
	}
	fmt.Println(len(coloursIds))
	slices.Reverse(coloursIds)
	for _, colourId := range coloursIds {
		colour, _ := colours[colourId]
		id := strconv.Itoa(colourId + 1)
		leftBoarder := strconv.Itoa(colour.leftBorder + 1)
		rightBorder := strconv.Itoa(colour.rightBorder + 1)
		fmt.Println(id + " " + leftBoarder + " " + rightBorder)
	}
}
