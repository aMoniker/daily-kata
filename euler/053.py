import math

def permutations_count(n: int, r: int) -> int:
    return int(factorial(n) / (factorial(r) * factorial(n - r)))

def factorial(x):
    product = 1
    for i in range(2, x + 1):
        product *= i
    return product

def main():
    minimum = 1
    maximum = 100
    threshold = 1000000

    count = 0
    for n in range(maximum, minimum, -1):
        r = math.floor(n / 2)
        first = True
        odd_n = n % 2 != 0
        while r >= 1 and permutations_count(n, r) > threshold:
            count += 1 if (first and not odd_n) else 2
            r -= 1
            first = False
        if first:
            break

    print(count)

if __name__ == '__main__':
    main()
