from typing import List
from util import is_prime_naive

def main():
    total = 0
    primes = 0
    percent = 1
    threshold = 0.1
    layer = 0
    while percent >= threshold or layer == 1:
        layer += 1
        for x in diagonals(layer):
            total += 1
            if is_prime_naive(x): primes += 1
        percent = primes / total
    print(f"first lower than {threshold} at side length {side_len(layer)}")

def side_len(layer: int) -> int:
    return 2 * layer - 1

def diagonals(layer: int) -> List[int]:
    if layer < 1: return []
    if layer == 1: return [1]
    step = (layer - 1) * 2
    last_end = pow(side_len(layer - 1), 2)
    return [last_end + (i * step) for i in [1,2,3,4]]

if __name__ == '__main__':
    main()
