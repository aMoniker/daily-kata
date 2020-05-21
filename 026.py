import math
from typing import List, Tuple


def main():
    n = 1000
    largest_num: int = 0
    largest_cycle: int = 0
    for i in range(2, n + 1):
        (size, num) = get_cycle_size(i)
        if size > largest_cycle:
            largest_num = num
            largest_cycle = size
    print(f"largest: {largest_num} with cycle {largest_cycle}")


def get_cycle_size(d: int) -> Tuple[int, int]:
    n = 1  # numerator
    remainders: List[int] = []

    while True:
        x = math.floor(n / d)
        n = (n - d * x) * 10
        if n == 0:
            return (0, d)  # no cycle, evenly divisible
        remainders.append(n)
        for i in range(0, len(remainders) - 1):
            if remainders[i] == remainders[-1]:
                cycle = len(remainders) - i - 1
                return (cycle, d)

    return (0, d)


if __name__ == "__main__":
    main()
