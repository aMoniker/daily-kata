days_in_month = {
    0: 31,
    1: 28,
    2: 31,
    3: 30,
    4: 31,
    5: 30,
    6: 31,
    7: 31,
    8: 30,
    9: 31,
    10: 30,
    11: 31,
}


def main():
    sundays = 0

    day = 1
    month = 0
    year = 1900

    while year < 2001:
        addDays = days_in_month[month]
        if month == 1 and is_leap_year(year):
            addDays += 1
        month += 1
        if month > 11:
            month = 0
            year += 1
        day = (day + addDays) % 7
        if day == 0 and year >= 1901:
            sundays += 1

    print(sundays)


def is_leap_year(year: int) -> bool:
    if year % 100 == 0:
        return year % 400 == 0
    else:
        return year % 4 == 0


if __name__ == "__main__":
    main()
