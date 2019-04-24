// https://www.codewars.com/kata/58678d29dbca9a68d80000d7

package main

import (
	"fmt"
)

func main() {
	fmt.Println("code:", ">*>*")
	fmt.Println("input: ", "00101100")
	test1 := Interpreter(">*>*", "00101100")
	fmt.Println("output:", test1)

	fmt.Println("code:", "*>*>>*>>>*>*")
	fmt.Println("input: ", "00101100")
	test2 := Interpreter("*>*>>*>>>*>*", "00101100")
	fmt.Println("output:", test2)

	fmt.Println("code:", "*[>*]")
	fmt.Println("input: 00000000000000")
	test3 := Interpreter("*[>*]", "00000000000000")
	fmt.Println("output:", test3)
}

func Interpreter(code, tape string) string {
	ptr := 0
	dir := 1
	bracket := 0
	jump := false
	mem := inputMemory(tape)

	for i := 0; i < len(code); i += dir {
		if ptr < 0 || ptr >= len(mem) {
			return outputMemory(mem)
		}
		sym := code[i]
		switch sym {
		case '>':
			if !jump {
				ptr++
			}
		case '<':
			if !jump {
				ptr--
			}
		case '*':
			if !jump {
				mem[ptr] = !mem[ptr]
			}
		case '[':
			if jump {
				bracket++
				if bracket == 0 {
					jump = false
					dir = 1
				}
			} else if !mem[ptr] {
				bracket++
				jump = true
				dir = 1
			}
		case ']':
			if jump {
				bracket--
				if bracket == 0 {
					jump = false
				}
			} else if mem[ptr] {
				bracket--
				jump = true
				dir = -1
			}
		}
	}

	return outputMemory(mem)
}

func inputMemory(tape string) []bool {
	mem := make([]bool, len(tape))
	for i, sym := range tape {
		if sym == '1' {
			mem[i] = true
		}
	}
	return mem
}

func outputMemory(memory []bool) string {
	ret := make([]rune, len(memory))
	for i, bit := range memory {
		if bit {
			ret[i] = '1'
		} else {
			ret[i] = '0'
		}
	}
	return string(ret)
}
