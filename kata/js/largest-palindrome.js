/**
 * Find the largest palindromic product between any two numbers
 * in a given range of numbers.
 */
const checkPalindrome = (n) => {
  let numString = ''+n;
  let pivot = numString.length / 2;
  for (let i = 0; i < pivot; i++) {
    if (numString[i] !== numString[numString.length - i - 1]) {
      return false;
    }
  }
  return true;
};

let count = 0;
let largestPalindrome = 0;
let largestInnerLoop = 0;
let upperBound = 999;
let lowerBound = 99;

console.log(`finding palindromic products between ${lowerBound} and ${upperBound}`);
for (let i = upperBound; i > lowerBound; i--) {
  if (i <= largestInnerLoop) { break; }

  if (largestInnerLoop > 0) {
    largestInnerLoop++;
  }

  let largestPalindromeInner = 0;
  let innerBottom = (largestInnerLoop || lowerBound);
  for (let j = i; j > innerBottom; j--) {
    count++;
    let prod = i * j;

    if (checkPalindrome(prod) && prod > largestPalindrome) {
      console.log(`palindrome found: ${i} * ${j} = ${i * j}`);
      largestPalindromeInner = prod;
      if (j > largestInnerLoop) {
        largestInnerLoop = j;
      }
      break;
    }
  }

  if (largestPalindrome < largestPalindromeInner) {
    largestPalindrome = largestPalindromeInner;
  }
}

console.log(`greatest 3-digit palindrome: ${largestPalindrome}`);
console.log(`number of checks: ${count}`);
