/**
 * Fast Array Stats
 *
 * Extend & Proxy native JS arrays to allow implementation of
 * running calculations for fast computation of standard deviation.
 *
 * https://en.wikipedia.org/wiki/Standard_deviation#Rapid_calculation_methods
 *
 * Captures array access methods in order to update the calculations.
 * a[0], a.push, a.pop, a.shift, a.unshift & a.delete are all handled.
 *
 * https://www.codewars.com/kata/fast-stats-on-an-array
 */

class StatsArray extends Array {
  constructor() {
    // allow constructing as StatsArray(1,2,3) & StatsArray([1,2,3])
    let args = [...arguments];
    if (args.length === 1 && Array.isArray(args[0])) { args = args[0]; }
    super(...args);

    // shift/unshift operations require special handling
    this.shifting = false;
    this.unshifting = false;
    this.shiftValue = undefined;

    // keep track of the running sum & sum of squares
    this.runningSum = null; // s1
    this.runningSumSquares = null; // s2

    // allow access to getter functions as properties
    let getters = ['sum', 'sumSquares', 'mean', 'standardDeviation'];
    for (let g of getters) {
      Object.defineProperty(this, g, { get: this[g] });
    }

    // proxy the instance as its own handler
    // in order to intercept array access (e.g. foo[0], foo.push(), etc.)
    return new Proxy(this, this);
  }

  /**
   * Calculate the sum if it hasn't already been stored,
   * otherwise return the cached sum.
   *
   * @return int
   */
  sum = () => {
    if (this.length === 0) { return NaN; }
    if (this.runningSum !== null) { return this.runningSum; }
    let sum = this.reduce((a,e) => a + e, 0);
    this.runningSum = sum;
    return sum;
  }

  /**
   * Calculate the sum of squares if it hasn't already been stored,
   * otherwise return the cached sum of squares.
   *
   * @return int
   */
  sumSquares = () => {
    if (this.length === 0) { return NaN; }
    if (this.runningSumSquares !== null) { return this.runningSumSquares; }
    let sumSquares = this.reduce((a,e) => a + e ** 2, 0);
    this.runningSumSquares = sumSquares;
    return sumSquares;
  }

  /**
   * Incrementally update the cached sum and sum of squares
   *
   * @param  int amount update by this amount, positive or negative
   */
  updateSums = (amount) => {
    let sum = this.sum || 0;
    sum += amount;
    this.runningSum = sum;

    let sign = (amount > 0 ? 1 : -1);
    let sumSquares = this.sumSquares || 0;
    sumSquares += (amount ** 2) * sign;
    this.runningSumSquares = sumSquares;
  }

  /**
   * Return the mean of the elements
   *
   * @return int
   */
  mean = () => {
    return this.sum / this.length;
  }

  /**
   * Calculate the standard deviation using the fast method
   * based on a sum & sum of squares
   *
   * @return int
   */
  standardDeviation = () => {
    if (this.length === 0) { return NaN; }
    let sum = this.sum;
    return Math.sqrt(this.length * this.sumSquares - sum ** 2) / this.length;
  }

  /**
   * The get function is handled by the Proxy object given by the constructor.
   *
   * It intercepts the calls to shift/unshift so further processing
   * can be done in the set & delete traps.
   *
   * @param  object target
   * @param  string name
   * @return mixed
   */
  get = (target, name) => {
    if (name === 'shift') {
      this.shifting = true;
      this.shiftValue = target[0];
    } else if (name === 'unshift') {
      this.unshifting = true;
    }
    return target[name];
  }

  /**
   * The set function is handled by the Proxy object given by the constructor.
   *
   * It intercepts the calls to set array values and updates the running sums.
   *
   * @param  object obj
   * @param  string prop
   * @param  mixed  value
   * @return bool
   */
  set = (obj, prop, value) => {
    if (!this.shifting && Number.isInteger(Number.parseInt(prop))) {
      if (this.unshifting) {
        if (prop === '0') { // last step in unshift is setting the 0 value
          this.unshifting = false;
          this.updateSums(value);
        }
      } else {
        let cur = obj[prop];
        if (cur) { this.updateSums(-cur); }
        this.updateSums(value);
      }
    }

    obj[prop] = value;
    return true;
  }

  /**
   * The delete function is handled by the Proxy given by the constructor.
   *
   * It intercepts calls to delete in order to update sums,
   * and performs special handling for shift/unshift operations.
   *
   * @param  object target
   * @param  string prop
   * @return bool
   */
  deleteProperty = (target, prop) => {
    if (prop in target) {
      if (!this.unshifting && Number.isInteger(Number.parseInt(prop))) {
        let cur = target[prop];
        if (this.shifting) { // the last step in a shift operation is a delete
          if (this.shiftValue !== undefined) {
            this.updateSums(-this.shiftValue);
          }
          this.shifting = false;
        } else {
          this.updateSums(-cur);
        }
      }
      delete target[prop];
    }
    return true;
  }
}
