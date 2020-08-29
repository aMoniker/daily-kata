from util import get_prime_factors


def main():
    x = 2
    foundFour = False
    while not foundFour:
        for i in range(0, 4):
            primeFactors = get_prime_factors(x + i)
            primeDict = {j: True for j in primeFactors}
            if len(primeDict) != 4:
                x += i + 1
                break
            elif i == 3:
                foundFour = True

    print(f"first in sequence: {x}")


if __name__ == "__main__":
    main()
