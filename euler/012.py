from typing import List


def main():
    i = 1
    while True:
        n = get_triangular_number(i)
        f = get_factors(n)
        if f >= 500:
            print("First over 500 factors:", i, n)
            break
        i += 1


def get_triangular_number(n: int) -> int:
    return int((n ** 2 + n) / 2)


def get_factors(n: int) -> int:
    if n == 1:
        return 1

    factors = 2  # itself and one
    smallest = 0

    for i in range(2, n):
        if n % i == 0:
            smallest = i
            factors += 2  # smallest and its quotient
            break

    if smallest == 0:
        return factors

    step = 1 if smallest == 2 else 2
    i = smallest + step
    limit = int(n / smallest)
    while i < limit:
        if n % i == 0:
            factors += 2
            limit = int(n / i)
        i += step

    return factors


if __name__ == "__main__":
    main()
