package main

import (
	"fmt"
	"math"
)

func main() {
	times := Movie(500, 15, 0.9)
	fmt.Println("Expected: 43")
	fmt.Println("Got:", times)
}

func Movie(card, ticket int, perc float64) int {
	sysA := 0
	sysB := float64(card)
	i := 1
	for ; ; i++ {
		sysA += ticket
		sysB += float64(ticket) * math.Pow(perc, float64(i))
		if math.Ceil(sysB) < float64(sysA) {
			break
		}
	}
	return i
}
