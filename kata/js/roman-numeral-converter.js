/**
 * Convert to and from Roman Numerals
 * https://www.codewars.com/kata/roman-numerals-helper
 */
class RomanNumerals {
  static toRoman(n) {
    let ret = '';
    let value = [1000, 900, 500, 100,  90,  50,  10,  9,   5,  4,   1];
    let order = ['M',  'CM', 'D', 'C','XC', 'L', 'X','IX','V','IV','I'];

    for (let i = 0; i < value.length; i++) {
      let v = value[i];
      if (n < v) { continue; }
      let r = Math.floor(n / v);
      n = n % v;
      ret += order[i].repeat(r);
    }

    return ret;
  }
  static fromRoman(str) {
    let ret = 0;
    let n = str.split('');
    let order = ['M', 'D', 'C', 'L', 'X', 'V', 'I'];
    let value = [1000, 500, 100, 50,  10,  5,   1];
    let prefix = { C: ['M'], X: ['C'], I: ['X', 'V'] };
    let indexes = n.map((v) => order.indexOf(v));

    for (let i = 0; i < n.length; i++) {
      let p = prefix[n[i]];
      if (i < n.length - 1 && p && p.indexOf(n[i + 1]) >= 0) {
          ret += (value[indexes[i + 1]] - value[indexes[i]]);
          i += 1;
      } else {
        ret += value[indexes[i]];
      }
    }

    return ret;
  }
}

// Example
// console.log(RomanNumerals.toRoman(2018));
// console.log(RomanNumerals.fromRoman('MIXIV'));
