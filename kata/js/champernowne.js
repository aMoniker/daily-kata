/**
 * Find the nth digit of the Champernowne constant (in base 10)
 * i.e. the string: "0.12345678910111213141516..."
 *
 * https://en.wikipedia.org/wiki/Champernowne_constant
 *
 * We consider digit number one to be the first zero before the decimal place,
 * and we do not count the decimal itself, since it is not a digit.
 *
 * Speed test indicates this algorithm finds each one of
 * the first 10 million positions in roughly 2.6 seconds.
 *
 * Runs in O(1) constant time.
 */
class Champernowne {
  /**
   * Get the nth digit of the Champernowne constant,
   * counting the first zero before the decimal place as digit one.
   *
   * @param  int n must be 1 or greater
   * @return int
   */
  getDigit(n) {
    if (typeof n !== 'number' || n % 1 !== 0 || n <= 0) {
      return Infinity;
    }

    let d = 0; // digit count of the number at position n
    let count = 0; // running tally of total characters needed
    do {
      d++;
      count += this.getNumChars(d);
    } while(n > count);

    let pivotChar = count - this.getNumChars(d);
    let offset = Math.ceil((n - pivotChar) / d);
    let number = offset + (d === 1 ? 0 : 10 ** (d - 1)) - 1;
    let numOffset = n - ((pivotChar + 1) + ((offset - 1) * d));
    return ('' + number)[numOffset];
  }

  /**
   * Get the number of characters that are needed
   * to represent the entire range of n-digit numbers.
   *
   * e.g. for 3-digit numbers, 2700 characters are needed
   *
   * Assumes base 10.
   *
   * @param  int n number of digits
   * @return int   number of characters needed
   */
  getNumChars(n) {
    let range = (10 ** n);
    if (n > 1) { range -= (10 ** (n - 1)); }
    return range * n;
  }

  /**
   * Naively build the Champernowne string to test
   *
   * @param  int n
   * @return int
   */
  testDigit(n) {
    let s = '';
    let i = -1;
    while(s.length < n) { s += ++i; }
    return s.slice(n - 1, n);
  }

  /**
   * Compare the digit returned by naively building the string
   * vs the digit returned from the algorithm.
   *
   * Tests the first n digits.
   *
   * @param  int n number of digits to test
   * @return void
   */
  testCorrectness(n) {
    console.log(`Running tests for first ${n} digits...`);
    let testDigit, getDigit;
    for (let i = 1; i < n; i++) {
      testDigit = this.testDigit(i);
      getDigit = this.getDigit(i);
      if (testDigit !== getDigit) {
        throw `Digit ${i} should be ${testDigit} but got ${getDigit}`;
      }
    }
    console.log('All tests pass!');
  }

  /**
   * Test the speed of the algorithm alone.
   * Processes the first n digits and returns the time it took.
   *
   * @param  int n the number of digits to test
   * @return void
   */
  testSpeed(n) {
    console.log(`Speed test. Processing ${n} positions.`);
    console.time('took');
    for (let i = 1; i < n; i++) {
      let d = C.getDigit(i);
      console.log(`Digit ${i} is ${d}`);
    }
    console.timeEnd('took');
  }
}

let C = new Champernowne;
console.log(C.getDigit(0));
