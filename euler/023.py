from typing import List, Dict
from util import get_factors_sum


def main():
    n = 28123

    abundant = get_abundant_numbers(n)
    abundant_pair_sums: Dict[int, bool] = {}
    for i in range(0, len(abundant)):
        for j in range(i, len(abundant)):
            pair_sum = abundant[i] + abundant[j]
            if pair_sum <= n:
                abundant_pair_sums[pair_sum] = True

    ret_sum = 0
    for x in range(1, n + 1):
        if x not in abundant_pair_sums:
            ret_sum += x

    print(ret_sum)


def get_abundant_numbers(n: int) -> List[int]:
    abundant = []
    for x in range(12, n + 1):
        fsum = get_factors_sum(x)
        if fsum > x:
            abundant.append(x)
    return abundant


if __name__ == "__main__":
    main()
