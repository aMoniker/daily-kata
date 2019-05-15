from math import floor


def main():
    n = 1000
    max_a = 332
    for a in range(max_a + 1):
        rem = n - a
        max_b = int(floor(rem / 2) if rem % 2 != 0 else rem / 2 - 1)
        for b in range(max_b + 1):
            c = n - a - b
            if a ** 2 + b ** 2 == c ** 2:
                print(a * b * c)
                return


if __name__ == "__main__":
    main()
