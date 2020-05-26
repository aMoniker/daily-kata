from typing import List
from itertools import permutations
from util import is_prime_naive


def main():
    print(find_largest_pandigital_prime())


def find_largest_pandigital_prime() -> int:
    for n in range(9, 1, -1):
        digits: List[int] = []
        for i in range(1, n + 1):
            digits.append(i)
        perms = permutations(reversed(digits))
        for p in perms:
            x = int("".join([str(x) for x in p]))
            if is_prime_naive(x):
                return x
    return 7


if __name__ == "__main__":
    main()
