def main():
    n = 1000
    fib_2 = 1
    fib_1 = 1

    index = 2
    while True:
        index += 1
        fib = fib_1 + fib_2
        if len(str(fib)) >= n:
            break
        fib_2 = fib_1
        fib_1 = fib

    print(index)


if __name__ == "__main__":
    main()
