import math
from typing import List, Tuple


def main():
    j = 1
    found = False
    while not found:
        j += 1
        a = generate_pentagonal_number(j)
        for k in range(j, 0, -1):
            b = generate_pentagonal_number(k)
            if is_pentagonal(a + b) and is_pentagonal(a - b):
                print(f"found pair: {a},{b} ({j},{k})")
                print(f"difference: {abs(a-b)}")
                return


def generate_pentagonal_number(n: int) -> int:
    return int((3 * pow(n, 2) - n) / 2)


def is_pentagonal(x: int) -> bool:
    n = (math.sqrt(24 * x + 1) + 1) / 6
    return n % 1 == 0


if __name__ == "__main__":
    main()
