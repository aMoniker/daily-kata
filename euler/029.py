from typing import Set


def main():
    lower = 2
    upper = 100
    products: Set[int] = set()

    for a in range(lower, upper + 1):
        for b in range(lower, upper + 1):
            products.add(pow(a, b))

    terms = sorted(list(products))
    print(len(terms))


if __name__ == "__main__":
    main()
