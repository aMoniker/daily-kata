from typing import List
from util import get_factors


def main():
    primes_up_to = 1000
    primes = generate_primes(2, primes_up_to)
    b_primes = primes.copy()

    max_primes = 0
    max_coefficients = (0, 0)

    for b in b_primes:
        for a in range(-999, 1000):
            n = 0
            while True:
                e = pow(n, 2) + (a * n) + b
                prime = False
                if e > primes[-1]:
                    primes += generate_primes(primes[-1] + 1, e)
                for p in primes:
                    if p > e:
                        break
                    if e == p:
                        prime = True
                        break
                if not prime:
                    break
                n += 1
            if n > max_primes:
                max_primes = n
                max_coefficients = (a, b)

    (x, y) = max_coefficients
    print(f"max {max_primes} primes using coefficients {x} & {y}")
    print(f"product of coefficients: {x * y}")


def generate_primes(start: int, end: int) -> List[int]:
    primes: List[int] = []

    n = start
    last_num_prime = False
    while n <= end or not last_num_prime:
        f = get_factors(n)
        if len(f) == 2:
            primes.append(n)
            last_num_prime = True
        else:
            last_num_prime = False
        n += 1

    return primes


if __name__ == "__main__":
    main()
