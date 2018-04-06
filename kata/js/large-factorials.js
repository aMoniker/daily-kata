/**
 * Calculate large factorials without losing precision
 * https://www.codewars.com/kata/557f6437bf8dcdd135000010
 */
function factorial(n) {
  if (n === 1 || n === 0) { return '1'; }
  let product = n.toString();
  for (let i = n - 1; i > 1; i--) {
    let acc = product;
    for (let j = 1; j < i; j++) {
      acc = addStrings(acc, product);
    }
    product = acc;
  }
  return product;
}

function addStrings(str1, str2) {
  let ret = '';
  let len1 = str1.length;
  let len2 = str2.length;
  let maxLength = Math.max(len1, len2);
  let carry = 0;
  for (let i = 1; i <= maxLength; i++) {
    let digit1 = Number.parseInt(str1[len1 - i] || 0);
    let digit2 = Number.parseInt(str2[len2 - i] || 0);
    let sum = digit1 + digit2 + carry;
    if (sum >= 10) {
      sum -= 10;
      carry = 1;
    } else {
      carry = 0;
    }
    ret = `${sum}${ret}`;
  }
  if (carry) { ret = `${carry}${ret}`; }
  return ret;
}

// Test
console.log(factorial(30) === '265252859812191058636308480000000');
console.log(factorial(126) === '23721732428800468856771473051394170805702085973808045661837377170052497697783313457227249544076486314839447086187187275319400401837013955325179315652376928996065123321190898603130880000000000000000000000000000000');
