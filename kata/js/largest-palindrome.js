/**
 * Find the largest palindromic product between any two numbers
 * in a given range of numbers, or return NaN if no palindromic product exists.
 *
 * Available as a Codewars kata at:
 * https://www.codewars.com/kata/5aca2aceb4f6352a4100004f
 */
let checks = 0;
function largestPalindromicProduct(lower, upper) {
  console.log(`finding palindromic products between ${lower} and ${upper}`);
  let largest = 0;
  let product = 0;
  for (let i = upper; i >= lower; i--) {
    for (let j = i; j >= lower; j--) {
      checks++;
      product = i * j;
      if (!isPalindrome(product)) {
        product = 0;
        continue;
      }
      console.log(`palindrome found: ${i} * ${j} = ${product}`);
      if (j > lower) { lower = j; }
      break;
    }
    if (product > largest) { largest = product; }
  }
  let ret = (largest || NaN);
  console.log(`largest palindrome between ${lower} & ${upper}: ${ret}`);
  return ret;
}

function isPalindrome(n) {
  let str = n.toString();
  let pivot = Math.floor(str.length / 2);
  for (let i = 0; i < pivot; i++) {
    if (str[i] !== str[str.length - i - 1]) {
      return false;
    }
  }
  return true;
}

console.time('took');
largestPalindromicProduct(100, 999);
console.log(`number of palindrome checks required: ${checks}`);
console.timeEnd('took');
