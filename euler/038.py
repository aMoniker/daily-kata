from typing import List
from util import is_pandigital


def main():
    largest = 0
    x = 9
    w = 5
    while w > 1:
        s = ""
        for i in range(1, w + 1):
            s += str(x * i)
        x += 1
        if len(s) > 9:
            w -= 1
            continue
        y = int(s)
        if is_pandigital(y) and y > largest:
            largest = y
    print(largest)


if __name__ == "__main__":
    main()
