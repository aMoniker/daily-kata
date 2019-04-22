package main

import (
	"math"
	"strconv"
)

func main() {
	fmt.Println("Should be 1:", DigPow(89, 1))
}

func DigPow(n, p int) int {
	digits := getDigits(n)

	sum := float64(0)
	for i, v := range digits {
		sum += math.Pow(float64(v), float64(p+i))
	}

	floor := math.Floor(sum / float64(n))
	if floor == sum/float64(n) {
		return int(floor)
	}

	return -1
}

func getDigits(n int) []int {
	ret := make([]int, 0)
	nStr := strconv.Itoa(n)
	for i := range nStr {
		dig, err := strconv.Atoi(string(nStr[i]))
		if err == nil {
			ret = append(ret, dig)
		}
	}
	return ret
}
