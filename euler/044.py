import math
from util import get_pentagonal_number, is_pentagonal
from typing import List, Tuple


def main():
    j = 1
    found = False
    while not found:
        j += 1
        a = get_pentagonal_number(j)
        for k in range(j, 0, -1):
            b = get_pentagonal_number(k)
            if is_pentagonal(a + b) and is_pentagonal(a - b):
                print(f"found pair: {a},{b} ({j},{k})")
                print(f"difference: {abs(a-b)}")
                return


if __name__ == "__main__":
    main()
