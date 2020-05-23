from typing import Dict

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
                print(f"checking {str(a) + str(b) + str(product)}")
                if is_pandigital(str(a) + str(b) + str(product)):
                    products[product] = True


def is_pandigital(s: str) -> bool:
    counts: Dict[int, int] = {
        1: 0,
        2: 0,
        3: 0,
        4: 0,
        5: 0,
        6: 0,
        7: 0,
        8: 0,
        9: 0,
    }
    if len(s) != 9:
        return False
    for c in s:
        if c == "0":
            return False
        counts[int(c)] += 1
    for _, val in counts.items():
        if val != 1:
            return False
    return True


if __name__ == "__main__":
    main()
