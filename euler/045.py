from util import get_hexagonal_number, is_triangular, is_pentagonal


def main():
    x = 143
    tph = 0
    while tph == 0:
        x += 1
        h = get_hexagonal_number(x)
        if is_triangular(h) and is_pentagonal(h):
            tph = h
    print(h)


if __name__ == "__main__":
    main()
