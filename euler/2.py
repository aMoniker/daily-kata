def main():
    n_1: int = 2
    n_2: int = 1
    sum: int = 2

    while True:
        cur: int = n_1 + n_2
        if cur > 4000000:
            break
        n_2 = n_1
        n_1 = cur
        sum += cur if cur % 2 == 0 else 0

    print(sum)


if __name__ == "__main__":
    main()
