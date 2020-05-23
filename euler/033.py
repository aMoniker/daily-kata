from typing import Dict, Optional


def main():
    fractions: Dict[int, int] = {}
    for d in range(99, 9, -1):
        for n in range(d - 1, 9, -1):
            if d % 10 == 0 or n % 10 == 0:
                continue
            common = find_common_digit(n, d)
            if common == None:
                continue
            replacedN = str(n).replace(str(common), "")
            if len(replacedN) == 0:
                continue
            replacedD = str(d).replace(str(common), "")
            if len(replacedD) == 0:
                continue
            rN: int = int(replacedN)
            rD: int = int(replacedD)
            if rN == 0 or rD == 0:
                continue
            if rN / rD == n / d:
                fractions[n] = d

    print(fractions)

    product_n = 1
    product_d = 1
    for n, d in fractions.items():
        product_n *= n
        product_d *= d

    print(f"{product_n}/{product_d}")
    print(f"{product_d/product_n}")


def find_common_digit(x: int, y: int) -> Optional[int]:
    digits: Dict[str, bool] = {}
    for c in str(x):
        if not c in digits:
            digits[c] = True
    for c in str(y):
        if c in digits:
            return int(c)
    return None


if __name__ == "__main__":
    main()
