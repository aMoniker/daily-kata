from typing import List
from util import is_prime_naive


def main():
    x = 11
    truncatable_primes: List[int] = []
    while len(truncatable_primes) < 11:
        if is_truncatable_prime(x):
            truncatable_primes.append(x)
        inc = 2
        while not is_prime_naive(x + inc):
            inc += 2
        x += inc
    print(sum(truncatable_primes))


def is_truncatable_prime(n: int) -> bool:
    s = str(n)
    for i in range(1, len(s)):
        if not is_prime_naive(int(s[i:])) or not is_prime_naive(int(s[:-i])):
            return False
    return True


if __name__ == "__main__":
    main()
