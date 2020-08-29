from util import generate_primes_naive


def main():
    n = 1000000
    max_seq = 21
    max_prime = 953

    primes = generate_primes_naive(2, n)
    primeDict = {p: True for p in primes}

    for i in range(0, len(primes)):
        x = 0
        seq = 0
        while x < n and i + seq < len(primes):
            x += primes[i + seq]
            seq += 1
            if x in primeDict and seq > max_seq:
                max_seq = seq
                max_prime = x

    print(f"found max prime {max_prime} of sequence length {max_seq}")


if __name__ == "__main__":
    main()
