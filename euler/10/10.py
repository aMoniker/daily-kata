def main():
    n = 2000000
    ints = [False, False, True]

    for i in range(len(ints), n + 1):
        ints.append(True)

    prime = 2
    while prime < n:
        for i in range(prime * 2, len(ints), prime):
            ints[i] = False
        for i in range(prime + 1, len(ints)):
            if ints[i]:
                prime = i
                break
        else:
            break

    sum = 0
    for i in range(len(ints)):
        sum += i if ints[i] else 0

    print(sum)


if __name__ == "__main__":
    main()
