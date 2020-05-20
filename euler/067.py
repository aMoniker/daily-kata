import math


def main():
    rows = parse_rows()

    for i in range(1, len(rows)):
        for j in range(0, len(rows[i])):
            prevRow = rows[i - 1]
            weight1 = rows[i][j] + prevRow[j - 1] if j - 1 >= 0 else -math.inf
            weight2 = rows[i][j] + prevRow[j] if j < len(prevRow) else -math.inf
            rows[i][j] = max(weight1, weight2)
    print(max(rows[-1]))


def parse_rows():
    file = open("./data/067.txt", mode="r")
    text = file.read()
    file.close()

    rows = text.split("\n")
    for i in range(0, len(rows)):
        rows[i] = rows[i].split(" ")
        for j in range(0, len(rows[i])):
            rows[i][j] = int(rows[i][j])

    return rows


if __name__ == "__main__":
    main()
