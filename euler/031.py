# blatantly stolen from SICP p.40

import math
from typing import List, Dict, Tuple

coins: List[int] = [1, 2, 5, 10, 20, 50, 100, 200]
memo_ways: Dict[Tuple[int, int], int] = {}


def main():
    ways = count_change(200)
    print(ways)


def count_change(amount: int):
    return cc(amount, len(coins))


def cc(amount: int, kinds: int):
    if (amount, kinds) in memo_ways:
        return memo_ways[(amount, kinds)]
    if amount == 0:
        return 1
    if amount < 0 or kinds == 0:
        return 0
    ways = cc(amount, kinds - 1) + cc(amount - first_denom(kinds), kinds)
    memo_ways[(amount, kinds)] = ways
    return ways


def first_denom(kinds: int):
    return coins[kinds - 1]


if __name__ == "__main__":
    main()
