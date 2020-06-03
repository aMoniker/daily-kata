import math
from typing import List

# get sum of factors not including n
def get_factors_sum(n: int) -> int:
    if n == 1:
        return 1

    factor_sum = 0
    step = 1 if n % 2 == 0 else 2
    limit = int(math.ceil(n / 2) + 1)
    for i in range(1, limit, step):
        if n % i == 0:
            factor_sum += i

    return factor_sum


# get list of all factors including n
def get_factors(n: int) -> List[int]:
    factors: List[int] = []
    step = 1 if n % 2 == 0 else 2
    limit = int(math.ceil(n / 2) + 1)
    for i in range(1, limit, step):
        if n % i == 0:
            factors.append(i)
    factors.append(n)
    return factors


def factorial(n: int) -> int:
    if n == 1:
        return 1
    product = 1
    for x in range(2, n + 1):
        product *= x
    return product


def generate_primes_naive(start: int, end: int) -> List[int]:
    primes: List[int] = []
    if end <= 1:
        return primes
    if start == 2:
        primes.append(2)
        start += 1
    if start % 2 == 0:
        start += 1
    for x in range(start, end + 1, 2):
        if is_prime_naive(x):
            primes.append(x)
    return primes


def is_prime_naive(n: int) -> bool:
    if n < 2:
        return False
    if n == 2:
        return True
    for x in range(2, math.ceil(math.sqrt(n)) + 1):
        if n % x == 0:
            return False
    return True


def is_palindrome(s: str) -> bool:
    length = len(s)
    for i in range(0, math.floor(length / 2)):
        if s[i] != s[length - i - 1]:
            return False
    return True


def is_pandigital(x: int, n: int) -> bool:
    if n < 1 or n > 9:
        return False
    s = str(x)
    if len(s) != n:
        return False
    counts: List[int] = [0] * n
    for c in s:
        y = int(c)
        if y < 1 or y > n:
            return False
        counts[int(c) - 1] += 1
    for count in counts:
        if count != 1:
            return False
    return True


def get_pentagonal_number(n: int) -> int:
    return int((3 * pow(n, 2) - n) / 2)


def get_triangular_number(n: int) -> int:
    return n * (n + 1) / 2


def get_hexagonal_number(n: int) -> int:
    return n * (2 * n - 1)


def is_triangular(x: int) -> bool:
    n = (math.sqrt(1 + 8 * x) - 1) / 2
    return n % 1 == 0


def is_pentagonal(x: int) -> bool:
    n = (math.sqrt(24 * x + 1) + 1) / 6
    return n % 1 == 0
