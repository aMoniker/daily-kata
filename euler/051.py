from itertools import permutations
from util import generate_primes_naive
from sympy.utilities.iterables import multiset_permutations
from typing import Dict, List, Optional

def main():
    size = 8
    digits = 2
    family = None
    while family == None:
        family = find_family(size, digits)
        digits += 1
    print(f"found family: {family}")

def generate_digit_permutations(digits: int) -> List[List[bool]]:
    perms = []
    for i in range(1, digits):
        perms += multiset_permutations(
            [True] * i + [False] * (digits - i), digits
        )
    return perms

def get_permutation_family(
    x: int, size: int, perm: List[bool], primes: Dict[int,bool]
) -> Optional[List[int]]:
    if not validate_permutation(x, perm): return None

    strikes = 0
    possible_strikes = 10 - size
    family = []

    for a in range(0, 10):
        xl = [int(i) for i in str(x)]
        skip_leading_zero = False
        for i,v in enumerate(perm):
            if i == 0 and a == 0 and v:
                skip_leading_zero = True
                break
            if v: xl[i] = a
        if skip_leading_zero:
            strikes += 1
            continue
        t = int("".join([str(i) for i in xl]))
        if t in primes:
            family.append(t)
        else:
            strikes += 1
        if strikes > possible_strikes:
            return None

    return family

def validate_permutation(x: int, perm: List[bool]) -> bool:
    n = None
    s = str(x);
    for i,v in enumerate(perm):
        if not v: continue
        if n == None: n = s[i]
        if s[i] != n: return False
    return True

def find_family(size: int, digits: int) -> Optional[List[int]]:
    start = pow(10, digits - 1)
    end = pow(10, digits) - 1
    print(f"search from {start} to {end} for family of size {size}")

    primes = generate_primes_naive(start, end)
    primeMap = {i : True for i in primes}
    perms = generate_digit_permutations(digits)

    for x in primes:
        for p in perms:
            family = get_permutation_family(x, size, p, primeMap)
            if family: return family

    return None

if __name__ == "__main__":
    main()
