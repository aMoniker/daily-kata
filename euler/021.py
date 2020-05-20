import math
from typing import Dict, List

sums: Dict[int, List[int]] = {}


def main():
    amicable_sum = 0
    for i in range(1, 10000):
        fsum = get_factors_sum(i)
        if not fsum in sums:
            sums[fsum] = []
        sums[fsum].append(i)
    for key, items in sums.items():
        for item in items:
            if item == key:
                continue
            if item in sums and key in sums[item]:
                amicable_sum += item
    print(amicable_sum)


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


if __name__ == "__main__":
    main()
