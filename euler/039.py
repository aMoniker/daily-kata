from typing import List, Dict
import math
import time


def main():
    solutions: Dict[int, Dict[int, bool]] = {}

    p_limit = 1000
    m_limit = math.ceil(math.sqrt(p_limit / 2))

    for m in range(1, m_limit + 1):
        for n in range(1, m + 1):
            m_2 = pow(m, 2)
            n_2 = pow(n, 2)
            a = m_2 - n_2
            b = m * n * 2
            c = m_2 + n_2
            if a <= 0 or b <= 0 or c <= 0:
                continue

            k = 0
            while True:
                k += 1
                ka = k * a
                kb = k * b
                kc = k * c
                p = ka + kb + kc
                if p > p_limit or p < 12:
                    break
                if not p in solutions:
                    solutions[p] = {}
                solutions[p][ka + kb] = True

    max_perimeter = 0
    max_solutions = 0
    for p, s in solutions.items():
        if len(s) >= max_solutions:
            max_solutions = len(s)
            max_perimeter = p

    print(f"perimeter {max_perimeter} has {max_solutions} solutions")


if __name__ == "__main__":
    main()
