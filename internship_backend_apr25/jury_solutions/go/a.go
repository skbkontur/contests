package main

import (
	"fmt"
	"maps"
	"strconv"
)
import "os"
import "bufio"

var AllSuits = map[rune]struct{}{
	'C': {},
	'D': {},
	'H': {},
	'S': {},
}

var AllRanks = map[rune]struct{}{
	'2': {},
	'3': {},
	'4': {},
	'5': {},
	'6': {},
	'7': {},
	'8': {},
	'9': {},
	'T': {},
	'J': {},
	'Q': {},
	'K': {},
	'A': {},
}

var AllCardsCount = 52

func main() {
	scanner := bufio.NewScanner(os.Stdin)
	scanner.Buffer(make([]byte, 1000000), 1000000)

	var r1, s1, r2, s2 int
	scanner.Scan()
	fmt.Sscan(scanner.Text(), &r1, &s1, &r2, &s2)
	probability1 := CalculateProbability(scanner, r1, s1)
	probability2 := CalculateProbability(scanner, r2, s2)

	if probability1 > probability2 {
		fmt.Println(probability1)
	} else {
		fmt.Println(probability2)
	}
}

func CalculateProbability(scanner *bufio.Scanner, r int, s int) float64 {
	removedCards := make(map[string]struct{})
	for i := 0; i < r; i++ {
		scanner.Scan()
		group := scanner.Text()
		_ = group
		maps.Copy(removedCards, GetCards(group))
	}

	soughtCards := make(map[string]struct{})
	for i := 0; i < s; i++ {
		scanner.Scan()
		group := scanner.Text()
		_ = group
		maps.Copy(soughtCards, GetCards(group))
	}
	
	if len(removedCards) == AllCardsCount {
		return 0
	}
	var cardsIntersection int
	for card, _ := range removedCards {
		_, ok := soughtCards[card]
		if ok {
			cardsIntersection++
		}
	}
	return float64(len(soughtCards)-cardsIntersection) / float64(AllCardsCount-len(removedCards))
}

func GetCards(group string) map[string]struct{} {
	currentSuits := make(map[rune]struct{})
	for _, suit := range group {
		_, ok := AllSuits[suit]
		if ok {
			currentSuits[suit] = struct{}{}
		}
	}
	if len(currentSuits) == 0 {
		currentSuits = AllSuits
	}

	currentRanks := make(map[rune]struct{})
	for _, rank := range group {
		_, ok := AllRanks[rank]
		if ok {
			currentRanks[rank] = struct{}{}
		}
	}
	if len(currentRanks) == 0 {
		currentRanks = AllRanks
	}

	cards := make(map[string]struct{})
	for suit, _ := range currentSuits {
		for rank, _ := range currentRanks {
			var card = strconv.QuoteRune(suit) + strconv.QuoteRune(rank)
			cards[card] = struct{}{}
		}
	}

	return cards
}
