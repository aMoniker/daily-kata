from typing import List
from util import factorial

# a very naive brute-force solution that happens to be correct
def main():
    found: List[int] = []
    for n in range(3, 6):
        find_digit_factorials(n, found)
    print(found)


def find_digit_factorials(n: int, numbers: List[int]):
    for x in range(pow(10, n - 1), pow(10, n)):
        total = 0
        for c in str(x):
            total += factorial(int(c))
        if total == x:
            numbers.append(x)


if __name__ == "__main__":
    main()
