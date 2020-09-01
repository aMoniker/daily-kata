from typing import List

# obtained from https://en.wikipedia.org/wiki/Letter_frequency
frequencies = {
    'a': 0.082,
    'e': 0.13,
    'i': 0.07,
    'o': 0.075,
    'u': 0.028,
}

def main():
    enc_ascii = get_encrypted_ascii()
    pwd_len = 3
    password = find_password(enc_ascii, pwd_len)
    print(f"found password: {password}")

    dec_ascii = decrypt_ascii(enc_ascii, password)
    message = "".join([chr(c) for c in dec_ascii])
    print(f"decrypted message:\n{message}")

    print(f"sum of decrypted ascii values: {sum(dec_ascii)}")


def decrypt_ascii(enc_ascii: List[int], password: str) -> List[int]:
    dec_ascii = [c for c in enc_ascii]
    pwd_ascii = [ord(c) for c in password]
    for i in range(0, len(dec_ascii)):
        dec_ascii[i] = pwd_ascii[i % len(pwd_ascii)] ^ dec_ascii[i]
    return dec_ascii

def find_password(enc_ascii: List[int], pwd_len: int) -> str:
    password = ""

    for i in range(0, pwd_len):
        enc_chars = enc_ascii[i::pwd_len]
        variances = {}
        avg_variance = {}
        for cp in range(97, 123):
            dec_test = [cp ^ c for c in enc_chars]
            for char, freq in frequencies.items():
                char_freq = dec_test.count(ord(char)) / len(dec_test)
                variances[ord(char)] = abs(char_freq - freq) / freq
            total_variance = 0
            for char, var in variances.items():
                total_variance += var
            avg_variance[cp] = total_variance / len(variances)
        lowest_var = 1
        lowest_char = 0
        for char, var in avg_variance.items():
            if var < lowest_var:
                lowest_var = var
                lowest_char = char
        password += chr(lowest_char)

    return password


def get_encrypted_ascii():
    file = open("./data/059.txt", mode="r")
    text = file.read()
    file.close()
    return [int(i) for i in text.split(',')]

if __name__ == '__main__':
    main()
