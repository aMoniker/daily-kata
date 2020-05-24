from util import generate_primes_naive, is_prime_naive


def main():
    circular_primes = []
    primes = generate_primes_naive(2, 1000000)
    for prime in primes:
        s = str(prime)
        if len(s) == 1:
            circular_primes.append(prime)
            continue
        is_circular_prime = True
        for _ in range(1, len(s)):
            s = s[1:] + s[0]
            if not is_prime_naive(int(s)):
                is_circular_prime = False
                break
        if is_circular_prime:
            circular_primes.append(prime)

    print(len(circular_primes))


if __name__ == "__main__":
    main()
