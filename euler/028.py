from typing import List


def main():
    n = 1001
    step = 0
    num = 1
    total = 1
    cell: (int, int) = (0, 0)
    dirs: List[(int, int)] = [(1, 0), (0, -1), (-1, 0), (0, 1)]
    limit: List[int] = [1, -1, -1, 1]

    while num < pow(n, 2):
        m = step % len(dirs)
        d = dirs[m]
        l = limit[m]
        while abs(d[0] * cell[0]) <= abs(l) and abs(d[1] * cell[1]) <= abs(l):
            num += 1
            cell = (cell[0] + d[0], cell[1] + d[1])
            if abs(d[0] * cell[0]) == abs(l) or abs(d[1] * cell[1]) == abs(l):
                if num != 2:
                    add = num if d[0] != 1 else num - 1
                    total += add
                limit[m] += d[0] + d[1]
                break
        step += 1

    print(f"total: {total}")


if __name__ == "__main__":
    main()
