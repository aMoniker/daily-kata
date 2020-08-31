import math

def main():
    n = 10000
    cap = 50
    count = 0
    for x in range(0, n):
        if is_lychrel_number(x, cap): count += 1
    print(f"lychrel numbers below {n}: {count}")

def is_lychrel_number(x: int, cap: int) -> bool:
    for _ in range(0, cap-1):
        sx = str(x)
        x = x + int(sx[len(sx)::-1])
        if is_palindromic_number(x): return False
    return True

def is_palindromic_number(x: int) -> bool:
    s = str(x)
    for i in range(0, math.floor(len(s) / 2)):
        if s[i] != s[-i-1]: return False
    return True

if __name__ == '__main__':
    main()
