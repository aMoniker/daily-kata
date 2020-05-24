from util import is_palindrome


def main():
    total = 0
    for x in range(1, 1000000):
        if is_palindrome(str(x)) and is_palindrome(bin(x)[2:]):
            total += x
    print(total)


if __name__ == "__main__":
    main()
