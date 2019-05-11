from itertools import count
from typing import List


def main():
    n = 600851475143
    primes: List[int] = [2, 3, 5, 7, 11, 17, 23, 29]  # primed primes
    factors: List[int] = []

    done = False
    while not done:
        for i in range(len(primes)):
            if i == len(primes) - 1:
                primes.append(find_next_prime(primes))
            if n == primes[i]:
                factors.append(n)
                done = True
            elif n % primes[i] == 0:
                n = int(n / primes[i])
                factors.append(primes[i])

    print("largest factor", factors[-1])


def find_next_prime(primes: List[int]) -> int:
    for j in count(primes[-1] + 1):
        for p in primes:
            if j % p == 0:
                break
        else:
            return j
    return 1  # stop linter complaining


if __name__ == "__main__":
    main()
