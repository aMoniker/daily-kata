from typing import Dict
from util import is_pandigital

products: Dict[int, bool] = {}


def main():
    process_ranges(1, 9, 1000, 9999)  # ones and fours
    process_ranges(10, 99, 100, 999)  # twos and threes
    print(sum(products))


def process_ranges(aLower: int, aUpper: int, bLower: int, bUpper: int):
    for a in range(aLower, aUpper + 1):
        for b in range(bLower, bUpper + 1):
            product = a * b
            if not product in products:
                y = int(str(a) + str(b) + str(product))
                if is_pandigital(y):
                    products[product] = True


if __name__ == "__main__":
    main()
