import math

names = {
    1: "one",
    2: "two",
    3: "three",
    4: "four",
    5: "five",
    6: "six",
    7: "seven",
    8: "eight",
    9: "nine",
    10: "ten",
    11: "eleven",
    12: "twelve",
    13: "thirteen",
    14: "fourteen",
    15: "fifteen",
    16: "sixteen",
    17: "seventeen",
    18: "eighteen",
    19: "nineteen",
    20: "twenty",
    30: "thirty",
    40: "forty",
    50: "fifty",
    60: "sixty",
    70: "seventy",
    80: "eighty",
    90: "ninety",
}


def main():
    count = 0
    for i in range(1, 1001):
        number = spell_number(i).replace(" ", "").replace("-", "")
        count += len(number)
    print(count)


def spell_number(n: int) -> str:
    if n == 1000:
        return "one thousand"

    output = ""

    ones = n % 10
    tens = n % 100 - ones
    hundreds = math.floor((n - tens) / 100)

    if hundreds > 0:
        output += names.get(hundreds, "") + " hundred"
        if tens > 0 or ones > 0:
            output += " and "

    if ones + tens < 20:
        output += names.get(ones + tens, "")
    else:
        output += names.get(tens, "")
        if ones != 0:
            output += "-" + names.get(ones, "")

    return output


if __name__ == "__main__":
    main()
