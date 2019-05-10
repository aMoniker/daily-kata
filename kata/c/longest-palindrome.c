// https://www.codewars.com/kata/longest-palindrome/c

#include <stdio.h>
#include <string.h>
#include <math.h>

int check_palindrome(const char *s, int start, int end) {
	int half = start + floor((end - start) / 2);
	for (int i = start, j = end; i <= half && j >= half; i++, j--) {
		if (s[i] != s[j]) return 0;
	}
	return 1;
}

int longest_palindrome(const char *s) {
	if (strlen(s) == 0) return 0;

	int longest = 1;
	for (int i = 0; i < strlen(s); i++) {
		for (int j = strlen(s) - 1; j > i; j--) {
			if (check_palindrome(s, i, j) == 1 && j - i + 1 > longest) {
				longest = j - i + 1;
			}
		}
	}

	return longest;
}

int main() {
	int len = longest_palindrome("I like racecars that go fast"); // 7
	printf("%d\n", len);
	return 0;
}
