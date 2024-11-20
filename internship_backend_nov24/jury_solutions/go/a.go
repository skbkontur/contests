package main

import (
	"fmt"
	"strconv"
)

func main() {
	err := TaskA()
	if err != nil {
		fmt.Printf("err: %s", err.Error())
	}
}

func TaskA() error {
	var input string
	_, err := fmt.Scanln(&input)
	if err != nil {
		return err
	}

	n, err := strconv.Atoi(input)
	if err != nil {
		return err
	}

	var impressiveLine string
	charCount := 0
	for _ = range n {
		_, err := fmt.Scanln(&input)
		if err != nil {
			return err
		}

		set := make(map[rune]struct{}, len(input))
		for _, ch := range input {
			set[ch] = struct{}{}
		}

		if len(set) > charCount {
			charCount = len(set)
			impressiveLine = input
		}
	}

	fmt.Printf("%d %s\n", charCount, impressiveLine)
	return nil
}
