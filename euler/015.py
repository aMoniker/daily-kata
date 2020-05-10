from typing import List
import math


def main():
    paths = paths_through_cube(20)
    print("paths:", paths)


def paths_through_cube(n: int) -> int:
    if n == 1:
        return 2

    tri = generate_pascal_triangle(2 * n - 2)
    s = 0
    for i in range(0, n):
        s += tri[n + i - 1][i]
    return s * 2


def generate_pascal_triangle(n: int) -> List[List[int]]:
    tri = [[1], [1, 1]]
    for i in range(len(tri) - 1, n):
        prev = tri[-1]
        level = [1]
        for x in range(0, len(prev) - 1):
            level.append(prev[x] + prev[x + 1])
        level.append(1)
        tri.append(level)
    return tri


if __name__ == "__main__":
    main()
