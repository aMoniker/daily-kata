from typing import List
from itertools import permutations


def main():
    total = 0
    primes: List[int] = [2, 3, 5, 7, 11, 13, 17]
    perms = permutations([0, 1, 2, 3, 4, 5, 6, 7, 8, 9])
    for p in perms:
        divisible = True
        for i in range(1, 8):
            x = int(f"{p[i]}{p[i+1]}{p[i+2]}")
            if x % primes[i - 1] != 0:
                divisible = False
                break
        if divisible:
            total += int("".join([str(x) for x in p]))
    print(total)


if __name__ == "__main__":
    main()
