def main():
    largest_seq = 1
    starting_num = 2
    for i in range(starting_num, 999999):
        count = collatz_count(i)
        if count > largest_seq:
            largest_seq = count
            starting_num = i
    print("starting num & sequence length", starting_num, largest_seq)


def collatz_count(n: int) -> int:
    steps = 1
    while n != 1:  # it's conjectured that this loop should end
        n = int(n / 2) if n % 2 == 0 else n * 3 + 1
        steps += 1
    return steps


if __name__ == "__main__":
    main()
