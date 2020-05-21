from typing import List


def main():
    n = 1000000
    count = 1
    p = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
    while count < n:
        count += 1
        p = next_permutation(p)

    print("".join([str(x) for x in p]))


def next_permutation(p: List[int]) -> List[int]:
    # Find the largest x such that P[x]<P[x+1].
    # (If there is no such x, P is the last permutation.)
    x = 0
    for i in range(len(p) - 2, 0, -1):
        if p[i] < p[i + 1]:
            x = i
            break

    # Find the largest y such that P[x]<P[y].
    y = 0
    for i in range(len(p) - 1, x, -1):
        if p[x] < p[i]:
            y = i
            break

    # Swap P[x] and P[y].
    swp = p[x]
    p[x] = p[y]
    p[y] = swp

    # Reverse P[x+1 .. n].
    p = p[0 : x + 1] + p[-1:x:-1]

    return p


if __name__ == "__main__":
    main()
