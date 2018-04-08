/**
 * BrainLUCK interpreter
 *
 * http://www.codewars.com/kata/my-smallest-code-interpreter-aka-brainf-star-star-k/train/javascript
 */

function brainLuck(code, input) {
  return (new BrainLuckInterpreter).execute(code, input);
}

class BrainLuckInterpreter {
  init(code, input) {
    this.pointer = 0;
    this.cells = [];
    this.output = '';
    this.program = code;
    this.input = input;
    this.overflow = 256;
  }

  execute(code, input) {
    this.init(code.replace(/\s/g, ''), input);
    for (let i = 0; i < this.program.length; i++) {
      switch(this.program[i]) {
        case '>': this.pointer++;        break;
        case '<': this.pointer--;        break;
        case '+': this.increment();      break;
        case '-': this.decrement();      break;
        case '.': this.write();          break;
        case ',': this.read();           break;
        case '[': i = this.jumpAhead(i); break;
        case ']': i = this.jumpBack(i);  break;
        default:                         break;
      }
    }
    return this.output;
  }

  get() {
    let value = this.cells[this.pointer];
    return (value === undefined) ? 0 : value;
  }

  set(value) {
    value %= this.overflow;
    if (value < 0) { value += this.overflow; }
    this.cells[this.pointer] = value;
  }

  increment() {
    this.set(this.get() + 1);
  }

  decrement() {
    this.set(this.get() - 1);
  }

  write() {
    this.output += String.fromCharCode(this.get());
  }

  read() {
    this.set(this.input[0].charCodeAt());
    this.input = this.input.slice(1);
  }

  jumpAhead(i) {
    if (this.get() !== 0) { return i; }
    let open = 1; // count open brackets
    for (let j = i + 1; j < this.program.length; j++) {
      if      (this.program[j] === '[') { open++; }
      else if (this.program[j] === ']') { open--; }
      if (open === 0) { return j; }
    }
    throw `No matching ending bracket for position ${i}!`;
  }

  jumpBack(i) {
    if (this.get() === 0) { return i; }
    let open = 1; // count open brackets
    for (let j = i - 1; j >= 0; j--) {
      if      (this.program[j] === ']') { open++; }
      else if (this.program[j] === '[') { open--; }
      if (open === 0) { return j; }
    }
    throw `No matching starting bracket for position ${i}!`;
  }
}

// tests
// console.log(brainLuck(',+[-.,+]', 'Codewars'+String.fromCharCode(255)));
// console.log(brainLuck(',[.[-],]','Codewars'+String.fromCharCode(0)));
// console.log(brainLuck(',>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.', String.fromCharCode(8,9)));
