from util import generate_primes_naive


def main():
    four_digit_primes = generate_primes_naive(1000, 9999)
    permutations = dict()
    for p in four_digit_primes:
        perm = "".join(sorted(str(p)))
        if not perm in permutations:
            permutations[perm] = []
        permutations[perm].append(p)

    for p in permutations.values():
        if len(p) < 3:
            continue
        for i in range(0, len(p) - 2):
            if p[i + 1] - p[i] == p[i + 2] - p[i + 1]:
                print(f"found sequence {p[i]}{p[i+1]}{p[i+2]}")
                return


if __name__ == "__main__":
    main()
