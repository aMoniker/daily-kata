def main():
    product = 1
    for i in range(100, 0, -1):
        product *= i
    s = sum([int(i) for i in list(str(product))])
    print(s)


if __name__ == "__main__":
    main()
