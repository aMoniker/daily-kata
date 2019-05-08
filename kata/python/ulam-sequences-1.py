# https://www.codewars.com/kata/ulam-sequences/train/python

import math
from typing import List, Dict
import codewars_test as Test


def ulam_sequence(u0: int, u1: int, n: int):
    """
    u0 = first number
    u1 = second numberr
    n  = final number of elements in the sequence
    """

    ulam: List[int] = [u0, u1]
    uMap: Dict[int, int]
    jMap: Dict[int, int] = {0: 1}

    for m in range(2, n):
        uMap = {}
        jMap[len(ulam) - 1] = len(ulam)

        for i in range(len(ulam)):
            jMapUpdated: bool = False
            for j in range(jMap[i], len(ulam)):
                x = ulam[i] + ulam[j]
                if not x > ulam[-1]:
                    continue
                uMap[x] = uMap[x] + 1 if x in uMap else 1
                if not jMapUpdated:
                    jMapUpdated = True
                    jMap[i] = j

        min_x = math.inf
        for x, count in uMap.items():
            if count == 1 and x < min_x:
                min_x = x
        ulam.append(int(min_x))

    return ulam


if __name__ == "__main__":
    Test.assert_equals(ulam_sequence(1, 2, 5), [1, 2, 3, 4, 6])
    Test.assert_equals(ulam_sequence(3, 4, 5), [3, 4, 7, 10, 11])
    Test.assert_equals(ulam_sequence(5, 6, 8), [5, 6, 11, 16, 17, 21, 23, 26])
    Test.assert_equals(ulam_sequence(3, 4, 5), [3, 4, 7, 10, 11])

    a = [
        1,
        2,
        3,
        4,
        6,
        8,
        11,
        13,
        16,
        18,
        26,
        28,
        36,
        38,
        47,
        48,
        53,
        57,
        62,
        69,
    ]
    Test.assert_equals(ulam_sequence(1, 2, 20), a)

    a = [
        1,
        3,
        4,
        5,
        6,
        8,
        10,
        12,
        17,
        21,
        23,
        28,
        32,
        34,
        39,
        43,
        48,
        52,
        54,
        59,
        63,
        68,
        72,
        74,
        79,
        83,
        98,
        99,
        101,
        110,
        114,
        121,
        125,
        132,
        136,
        139,
        143,
        145,
        152,
        161,
        165,
        172,
        176,
        187,
        192,
        196,
        201,
        205,
        212,
        216,
        223,
        227,
        232,
        234,
        236,
        243,
        247,
        252,
        256,
        258,
    ]
    Test.assert_equals(ulam_sequence(1, 3, 60), a)
