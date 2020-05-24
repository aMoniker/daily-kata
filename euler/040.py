import math


def main():
    product = 1
    for i in range(0, 7):
        product *= get_champernowne_digit(pow(10, i))
    print(product)


def get_champernowne_digit(n: int) -> int:
    n += 1
    d = 0
    count = 0
    while n > count:
        d += 1
        count += get_num_chars(d)
    pivot = count - get_num_chars(d)
    offset = math.ceil((n - pivot) / d)
    number = str(offset + (0 if d == 1 else pow(10, d - 1)) - 1)
    numOffset = n - ((pivot + 1) + (offset - 1) * d)
    return int(number[numOffset])


def get_num_chars(n: int) -> int:
    r = pow(10, n)
    if n > 1:
        r -= pow(10, n - 1)
    return r * n


if __name__ == "__main__":
    main()
