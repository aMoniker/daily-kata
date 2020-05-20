import math


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
