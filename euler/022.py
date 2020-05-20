from typing import List


def main():
    name_sum = 0
    names = sorted(get_names())

    for i in range(0, len(names)):
        value = 0
        for char in names[i]:
            value += ord(char) - 64
        name_sum += value * (i + 1)

    print(name_sum)


def get_names() -> List[str]:
    file = open("./data/022.txt", mode="r")
    text = file.read()
    file.close()
    text = text[1:-1]
    return text.split('","')


if __name__ == "__main__":
    main()
