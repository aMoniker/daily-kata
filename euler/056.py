def main():
    maximum = 0
    for a in range(0, 100):
        for b in range(0, 100):
            ds = get_digital_sum(pow(a, b))
            if ds > maximum: maximum = ds
    print(f"maximum digital sum: {maximum}")

def get_digital_sum(x: int) -> int:
    total = 0
    for c in str(x): total += int(c)
    return total

if __name__ == '__main__':
    main()
