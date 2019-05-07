# https://www.codewars.com/kata/5870fa11aa0428da750000da

import re
from typing import Dict
from enum import Enum
import codewars_test as Test


class Cardinal(Enum):
    EAST = 0
    SOUTH = 1
    WEST = 2
    NORTH = 3

    def move(self, inc: int):
        return Cardinal((self.value + inc) % len(Cardinal))

    def right(self):
        return self.move(1)

    def left(self):
        return self.move(-1)


class Roboscript:
    def __init__(self, code: str):
        self.x = 0
        self.y = 0
        self.heading = Cardinal.EAST
        self.map: Dict[str, bool] = {"0:0": True}
        self.code = code

    def execute(self):
        self.x = 0
        self.y = 0
        i = 0
        while i < len(self.code):
            match = re.search(r"^([LRF][0-9]*)", self.code[i:])
            if not match or not match.group(1):
                i += 1  # skip invalid chars
                continue

            group = match.group(1)
            i += len(group)  # skip over the whole group next iteration

            mult = 1
            mult_match = re.search(r"([0-9]+)$", group)
            if mult_match:
                mult = int(mult_match.group(1))

            self.perform(group, mult)

    def perform(self, instruction: str, times: int):
        inst = instruction[0]
        if not len(inst) > 0:
            return

        self.map

        if inst == "F":
            x_inc = 0
            y_inc = 0
            if self.heading == Cardinal.EAST:
                x_inc = 1
            elif self.heading == Cardinal.SOUTH:
                y_inc = 1
            elif self.heading == Cardinal.WEST:
                x_inc = -1
            elif self.heading == Cardinal.NORTH:
                y_inc = -1
            for _ in range(times):
                self.x += x_inc
                self.y += y_inc
                self.map[f"{self.x}:{self.y}"] = True
        elif inst == "R":
            for _ in range(times):
                self.heading = self.heading.right()
        elif inst == "L":
            for _ in range(times):
                self.heading = self.heading.left()

    def output(self):
        # iterate once to find the min/max x/y values
        min_x = max_x = min_y = max_y = 0
        for key in self.map:
            x, y = self.getCoords(key)
            min_x = min(x, min_x)
            max_x = max(x, max_x)
            min_y = min(y, min_y)
            max_y = max(y, max_y)

        output: str = ""
        for y in range(min_y, max_y + 1):
            for x in range(min_x, max_x + 1):
                output += "*" if f"{x}:{y}" in self.map else " "
            output += "\r\n" if y < max_y else ""

        return output

    def getCoords(self, key: str):
        return list(map(lambda c: int(c), key.split(":")))


def execute(code: str):
    script = Roboscript(code)
    script.execute()
    return script.output()


if __name__ == "__main__":
    Test.describe("Your RS1 Interpreter")
    Test.it("should work for the example tests provided in the description")
    Test.assert_equals(execute(""), "*")
    Test.it("should work for FFFFF")
    Test.assert_equals(execute("FFFFF"), "******")
    Test.it("should work for FFFFFLFFFFFLFFFFFLFFFFFL")
    Test.assert_equals(
        execute("FFFFFLFFFFFLFFFFFLFFFFFL"),
        "******\r\n*    *\r\n*    *\r\n*    *\r\n*    *\r\n******",
    )
    print("******\r\n*    *\r\n*    *\r\n*    *\r\n*    *\r\n******")
    Test.it("should work for LFFFFFRFFFRFFFRFFFFFFF")
    Test.assert_equals(
        execute("LFFFFFRFFFRFFFRFFFFFFF"),
        "    ****\r\n    *  *\r\n    *  *\r\n********\r\n    *   \r\n    *   ",
    )
    print(
        "    ****\r\n    *  *\r\n    *  *\r\n********\r\n    *   \r\n    *   "
    )
    Test.it("should work for LF5RF3RF3RF7")
    Test.assert_equals(
        execute("LF5RF3RF3RF7"),
        "    ****\r\n    *  *\r\n    *  *\r\n********\r\n    *   \r\n    *   ",
    )
    print(
        "    ****\r\n    *  *\r\n    *  *\r\n********\r\n    *   \r\n    *   "
    )
    # Test.it("should work for LF5FFF56RF3RF3RF7")
    # Test.assert_equals(execute("LF5FFF56RF3RF3RF7"), "bar")
    # execute
