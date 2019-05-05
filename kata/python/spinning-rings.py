# https://www.codewars.com/kata/59afff65f1c8274f270020f5


def spinning_rings(inner_max: int, outer_max: int):
    inner = 0
    outer = 0

    spins = 0
    while True:
        spins += 1
        inner -= 1
        if inner < 0:
            inner = inner_max
        outer += 1
        if outer > outer_max:
            outer = 0
        if inner == outer:
            break

    return spins


if __name__ == "__main__":
    assert spinning_rings(2, 3) == 5
    assert spinning_rings(3, 2) == 2
    assert spinning_rings(1, 1) == 1
    assert spinning_rings(2, 2) == 3
    assert spinning_rings(3, 3) == 2
