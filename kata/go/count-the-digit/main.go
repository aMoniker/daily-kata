// https://www.codewars.com/kata/566fc12495810954b1000030

package main

import (
	"fmt"
	"strconv"
)

func main() {
	fmt.Println("This should be 213:", NbDig(550, 5))
}

func NbDig(n int, d int) int {
	digit := strconv.Itoa(d)
	digits := ""
	count := 0

	for i := 0; i <= n; i++ {
		digits = strconv.Itoa(i * i)
		for _, d := range digits {
			if string(d) == digit {
				count++
			}
		}
	}

	return count
}
