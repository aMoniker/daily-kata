from itertools import count


def main():
    s = smallest_multiple()
    print(s)


def smallest_multiple():
    for i in count(20, 20):
        for j in range(19, 1, -1):
            if i % j != 0:
                break
        else:
            return i


if __name__ == "__main__":
    main()
