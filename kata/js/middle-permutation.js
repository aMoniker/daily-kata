/**
 * Middle value of alphabetized permutations of a string
 * https://www.codewars.com/kata/58ad317d1541651a740000c5
 */
function middlePermutation(s) {
  let len = s.length;
  if (len <= 1) { return s; }

  let letters = s.split('').sort();
  let n = len + (len * (len - 1) ** 2); // permutations
  let mid = Math.ceil(n / 2);
  let first = Math.ceil(len / 2) - 1; // 1st char index
  let others = removeIndex(first, letters).reverse(); // all letters but 1st

  let ret = letters[first];
  if (len % 2 === 0) {
    ret += others.join('');
  } else {
    let second = Math.ceil(others.length / 2); // 2nd char index
    ret += `${others[second]}${removeIndex(second, others).join('')}`;
  }

  return ret;
}

function removeIndex(index, arr) {
  return [...arr.slice(0, index), ...arr.slice(index + 1)];
}

// Examples
// console.log(middlePermutation('abcd') === 'bdca');
// console.log(middlePermutation('abcdx') === 'cbxda');
// console.log(middlePermutation('abcdxgz') === 'dczxgba');
