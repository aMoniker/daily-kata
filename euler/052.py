import math
from typing import Dict

def main():
    d = 1
    m = 6
    found = None

    while not found:
        d += 1
        start = pow(10, d-1)
        end = pow(10, d) - 1
        cap = math.ceil(end / m)
        print(f"trying from {start} to {cap}")
        for x in range(start, cap):
            x_sig = get_signature(x)
            mismatch = False
            for n in range(2, m + 1):
                if get_signature(x * n) != x_sig:
                    mismatch = True
                    break
            if not mismatch:
                found = x
                break

    print(f"found {x}")


def get_signature(x: int) -> Dict[int, int]:
    d = dict()
    for i in [int(c) for c in str(x)]:
        if not i in d: d[i] = 0
        d[i] += 1
    return d


if __name__ == "__main__":
    main()
