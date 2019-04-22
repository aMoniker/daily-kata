// https://www.codewars.com/kata/5663f5305102699bad000056

package main

import (
	"fmt"
)

func main() {
	s1 := []string{"hoqq", "bbllkw", "oox", "ejjuyyy", "plmiis", "xxxzgpsssa", "xxwwkktt", "znnnnfqknaz", "qqquuhii", "dvvvwz"}
	s2 := []string{"cccooommaaqqoxii", "gggqaffhhh", "tttoowwwmmww"}
	fmt.Println("this should be 13", MxDifLg(s1, s2))
}

func MxDifLg(a1 []string, a2 []string) int {
	if len(a1) == 0 || len(a2) == 0 {
		return -1
	}
	a1min, a1max := getMinMax(a1)
	a2min, a2max := getMinMax(a2)
	return maxInt(a2max-a1min, a1max-a2min)
}

func getMinMax(s []string) (int, int) {
	if len(s) == 0 {
		return 0, 0
	}
	min := len(s[0])
	max := min

	for i := 1; i < len(s); i++ {
		if len(s[i]) < min {
			min = len(s[i])
		}
		if len(s[i]) > max {
			max = len(s[i])
		}
	}

	return min, max
}

func minInt(a int, b int) int {
	if a > b {
		return b
	}
	return a
}

func maxInt(a int, b int) int {
	if a > b {
		return a
	}
	return b
}
