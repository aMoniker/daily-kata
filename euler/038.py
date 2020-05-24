from typing import List


def main():
    largest = 0
    x = 9
    w = 5
    while w > 1:
        s = ""
        for i in range(1, w + 1):
            s += str(x * i)
        x += 1
        if len(s) > 9:
            w -= 1
            continue
        if is_pandigital(s) and int(s) > largest:
            largest = int(s)
    print(largest)


def is_pandigital(s: str) -> bool:
    if len(s) != 9:
        return False
    counts: List[int] = [0, 0, 0, 0, 0, 0, 0, 0, 0]
    for c in s:
        counts[int(c) - 1] += 1
    for count in counts:
        if count != 1:
            return False
    return True


if __name__ == "__main__":
    main()
