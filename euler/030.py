from typing import List, Tuple


def main():
    digits = 2
    within_range = True
    matches: List[int] = []
    while within_range:
        (within_range, m) = try_digits(digits)
        digits += 1
        matches += m
    print(f"matches: {matches}, sum: {sum(matches)}")


def try_digits(n: int) -> Tuple[bool, List[int]]:
    lower = int("1" + "0" * (n - 1))
    upper = pow(10, n)
    print(f"{lower} -> {upper}")

    matches: List[int] = []
    within_range = False
    for i in range(lower, upper):
        total = sum([pow(int(x), 5) for x in str(i)])
        if total == i:
            matches.append(i)
        if total >= lower and total < upper:
            within_range = True
        pass

    return (within_range, matches)


if __name__ == "__main__":
    main()
