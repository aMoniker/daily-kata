import math


def main():
    file = open("./data/042.txt", mode="r")
    text = file.read()
    file.close()
    words = text[1:-1].split('","')

    count = 0
    for word in words:
        value = word_value(word)
        n = math.floor(math.sqrt(value * 2))
        tri = (pow(n, 2) + n) / 2
        if tri == value:
            count += 1

    print(count)


def word_value(s: str) -> int:
    value = 0
    for c in s:
        value += ord(c.lower()) - 96
    return value


if __name__ == "__main__":
    main()
