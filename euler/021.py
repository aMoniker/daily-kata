from typing import Dict, List
from util import get_factors_sum

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


if __name__ == "__main__":
    main()
