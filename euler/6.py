def main():
    numbers = [x for x in range(101)]
    sum_of_squares = sum([x ** 2 for x in numbers])
    square_of_sums = sum(numbers) ** 2
    diff = square_of_sums - sum_of_squares
    print(diff)


if __name__ == "__main__":
    main()
