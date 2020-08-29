def main():
    n = 0
    for i in range(1, 1001):
        n += pow(i, i)
    print(str(n)[-10:])


if __name__ == "__main__":
    main()
