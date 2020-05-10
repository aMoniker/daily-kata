from itertools import count


def main():
    n = 10001
    primes = [2, 3, 5, 7, 11, 13]
    for x in count(17):
        for p in primes:
            if x % p == 0:
                break
        else:
            primes.append(x)
        if len(primes) == n:
            break
    print(primes[-1])


if __name__ == "__main__":
    main()
