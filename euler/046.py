from util import generate_primes_naive
from typing import Dict


def main():
    pStart = 2
    pExtend = 1000
    pDict = generate_more_primes(pStart, pStart + pExtend, dict())
    pStart += pExtend

    x = 3
    while True:
        x += 2
        if x in pDict:
            continue
        s = 1
        sumFound = False
        while True:
            ds = pow(s, 2) * 2
            if ds >= x:
                break
            if x - ds in pDict:
                sumFound = True
                break
            s += 1
        if not sumFound:
            break
        if x >= pStart:
            pDict = generate_more_primes(pStart, pStart + pExtend, pDict)
            pStart += pExtend

    print(f"lowest composite: {x}")


def generate_more_primes(s: int, e: int, d: Dict[int, bool]) -> Dict[int, bool]:
    primes = generate_primes_naive(s, e)
    for p in primes:
        d[p] = True
    return d


if __name__ == "__main__":
    main()
