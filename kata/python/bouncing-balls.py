# https://www.codewars.com/kata/bouncing-balls


def bouncingBall(h: float, bounce: float, window: float) -> int:
    if not validateParams(h, bounce, window):
        return -1

    count = 0
    while True:
        count += 1
        h *= bounce
        if h <= window:
            break
        count += 1

    return count


def validateParams(h: float, bounce: float, window: float) -> bool:
    valid = True
    valid = False if h <= 0 else valid
    valid = False if bounce >= 1 or bounce <= 0 else valid
    valid = False if window >= h else valid
    return valid


def main():
    pass


if __name__ == "__main__":
    main()
