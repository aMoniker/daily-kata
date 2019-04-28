package main

import (
	"fmt"
)

func main() {
	code := "*[s[e]*]"
	fmt.Println(code)
	test := Interpreter(code, 49, 5, 5)
	fmt.Println()
	fmt.Println("test is:")
	fmt.Println(test)
	fmt.Println("should be:")
	fmt.Println("11100\r\n11100\r\n11000\r\n11000\r\n11000")
}

type Grid = map[int]map[int]bool
type GridPointer struct {
	x int
	y int
}

func Interpreter(code string, iterations, width, height int) string {
	grid := makeGrid(width, height)
	ptr := GridPointer{x: 0, y: 0}
	iterCount := 0
	bracket := 0
	jump := false
	dir := 1

	for i := 0; iterCount < iterations; i += dir {
		if i >= len(code) {
			break
		}
		switch code[i] {
		case 'n':
			if !jump {
				ptr.y -= 1
				iterCount++
				if ptr.y < 0 {
					ptr.y = height - 1
				}
			}
		case 's':
			if !jump {
				ptr.y += 1
				iterCount++
				if ptr.y >= height {
					ptr.y = 0
				}
			}
		case 'e':
			if !jump {
				ptr.x += 1
				iterCount++
				if ptr.x >= width {
					ptr.x = 0
				}
			}
		case 'w':
			if !jump {
				ptr.x -= 1
				iterCount++
				if ptr.x < 0 {
					ptr.x = width - 1
				}
			}
		case '*':
			if !jump {
				grid[ptr.y][ptr.x] = !grid[ptr.y][ptr.x]
				iterCount++
			}
		case '[':
			if !jump {
				iterCount++
			}
			if jump {
				bracket++
				if bracket == 0 {
					jump = false
					dir = 1
				}
			} else if !grid[ptr.y][ptr.x] {
				bracket++
				jump = true
				dir = 1
			}
		case ']':
			if jump {
				bracket--
				if bracket == 0 {
					jump = false
					dir = 1
				}
			} else if grid[ptr.y][ptr.x] {
				bracket--
				jump = true
				dir = -1
				iterCount++
			}
		}
	}

	return outputGrid(grid, width, height)
}

func makeGrid(width, height int) Grid {
	grid := make(Grid)
	for i := 0; i < height; i++ {
		grid[i] = make(map[int]bool)
		for j := 0; j < width; j++ {
			grid[i][j] = false
		}
	}
	return grid
}

func outputGrid(grid Grid, width, height int) string {
	ret := ""
	for i := 0; i < height; i++ {
		for j := 0; j < width; j++ {
			if grid[i][j] {
				ret += "1"
			} else {
				ret += "0"
			}
		}
		ret += "\r\n"
	}
	return ret[:len(ret)-2]
}
