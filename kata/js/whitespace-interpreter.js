/**
 * Whitespace Interpreter
 *
 * http://www.codewars.com/kata/whitespace-interpreter
 * https://en.wikipedia.org/wiki/Whitespace_(programming_language)
 */

function whitespace(code, input) { // codewars hook
  return (new WhitespaceInterpreter).execute(code, input);
};

class WhitespaceInterpreter { // If you're going to do it, do it right.
  /**
   * Adventurers wanted
   */
  constructor() {
    this.inscribeSymbols();
    this.drawMap();
    this.summonImps();
  }

  /**
   * Spell components
   */
  init = (code, input) => {
    this.program = code;
    this.output = '';
    this.input = (''+input || '');
    this.labels = {};
    this.stack = [];
    this.heap = {};
    this.i = 0; // instruction pointer
    this.running = true;
    this.retLoc = null;
  }

  /**
   * Begin the ritual
   */
  execute = (code, input) => {
    this.init(code, input);
    this.carveLabels();

    let imp; // who let that imp in here?!
    while (this.running && this.i < this.program.length) {
      imp = this.fetchImp();
      if (imp === undefined) {
        this.raze(`Could not find IMP`);
      }
      imp();
    }

    if (this.running) {
      this.raze(`Program did not end cleanly`);
    }

    return this.output;
  }

  /**
   * Push any number of values onto the stack, in sequence
   * i.e. push(a,b,c) means c will be at the top of the stack.
   */
  push = (...values) => {
    for (let v of values) {
      this.stack.push(v);
    }
  }

  /**
   * Pop and return the top value of the stack
   */
  pop = () => {
    if (this.stack.length === 0) {
      this.raze(`Tried to pop an empty stack`);
    }
    return this.stack.pop();
  }

  /**
   * Get the current value at the given heap address
   */
  get = (address) => {
    if (this.heap[address] === undefined) {
      this.raze(`Invalid heap address: ${address}`);
    }
    return this.heap[address];
  }

  /**
   * Set the given heap address to the given value
   */
  set = (address, value) => {
    this.heap[address] = value;
  }

  /**
   * Get an execution position from the labels index
   * @param  string label
   * @return int
   */
  getLabel = (label) => {
    if (this.labels[label] === undefined) {
      this.raze(`Label not found in index: ${label}`);
    }
    return this.labels[label];
  }

  /**
   * Associate an execution position with a label
   * @param  string label
   * @param  int    position
   */
  setLabel = (label, position) => {
    if (this.labels[label] !== undefined) {
      this.raze(`Tried to set duplicate label: ${label}`);
    }
    this.labels[label] = position;
  }

  /**
   * Whitespace will accept input either characters or integers.
   *
   * Reading a character means simply taking a character from the input stream.
   *
   * Reading an integer involves parsing a decimal or hexadecimal number
   * from the current position of the input stream, up to and terminated by
   * a line-feed character.
   *
   * An error should be thrown if the input ends before parsing is complete.
   *
   * @return mixed
   */
  readInput = (asNumber = false) => {
    if (this.input.length === 0) {
      this.raze(`Tried to get empty input`);
    }

    let i = 0;
    let s = '';
    if (asNumber) { // read a number until the line-feed
      for (i; i < this.input.length; i++) {
        if (this.input[i] === '\n') {
          i++;
          break;
        }
        s += this.input[i];
      }
    } else { // read a single character
      i = 1;
      s = this.input[0];
    }

    this.input = this.input.substr(i);
    return asNumber ? Number.parseInt(s) : s;
  }

  pushNum = () => {
    this.push(this.nextNumber());
  }

  duplicateN = () => {
    let num = this.nextNumber();
    let index = this.stack.length - num - 1;
    if (index < 0 || index >= this.stack.length) {
      this.raze(`Can't duplicate stack frame out of range: ${num} from top`);
    }
    this.push(this.stack[index]);
  }

  discardN = () => {
    let num = this.nextNumber();
    let indexEnd = this.stack.length - 1;
    let indexStart = indexEnd - num;
    if (indexStart < 0 || indexStart >= this.stack.length) {
      // discard everything but top
      this.stack = [this.stack[this.stack.length - 1]];
    } else {
      this.stack = [
        ...this.stack.slice(0, indexStart),
        ...this.stack.slice(indexEnd),
      ];
    }
  }

  duplicate = () => {
    let val = this.pop();
    this.push(val, val);
  }

  swap = () => {
    let a = this.pop();
    let b = this.pop();
    this.push(a, b);
  }

  discard = () => {
    this.pop();
  }

  addition       = () => { this.dyad((a,b) => b+a); }
  subtraction    = () => { this.dyad((a,b) => b-a); }
  multiplication = () => { this.dyad((a,b) => b*a); }
  division = () => {
    this.dyad((a,b) => {
      if (a === 0) { this.raze(`Division by zero`); }
      return Math.floor(b/a);
    });
  }
  modulo = () => {
    this.dyad((a,b) => {
      if (a === 0) { this.raze(`Modulo zero`); }
      return (b - Math.floor(b / a) * a); // knuth floor division
    });
  }

  popAndStore = () => {
    let a = this.pop();
    let b = this.pop();
    this.set(b, a);
  }
  popAndStack = () => {
    let a = this.pop();
    this.push(this.get(a));
  }

  writeChar = () => {
    this.output += String.fromCharCode(this.pop());
  }
  writeNum = () => {
    this.output += this.pop().toString();
  }

  readChar = () => {
    let a = this.readInput().charCodeAt();
    let b = this.pop();
    this.set(b, a);
  }
  readNum = () => {
    let a = this.readInput(true);
    let b = this.pop();
    this.set(b, a);
  }

  markLabel = () => { // noop - handled by carveLabels
    this.nextLabel(); // must read label to keep execution position
  }
  callLabel = () => {
    this.jumpLabel(true, this.retLoc === null);
  }
  jumpLabel = (cond = true, setRetLoc = false) => {
    let label = this.nextLabel();
    if (cond) { // jump
      let jump = this.getLabel(label);
      if (setRetLoc) { this.retLoc = this.i; }
      this.i = jump;
    }
  }
  jumpLabelZero = () => {
    this.jumpLabel(this.pop() === 0);
  }
  jumpLabelNegative = () => {
    this.jumpLabel(this.pop() < 0);
  }
  returnExecution = () => {
    if (this.retLoc === null) {
      this.raze(`Tried to return execution to null position`);
    }
    this.i = this.retLoc;
    this.retLoc = null;
  }

  endProgram = () => {
    this.running = false; // so long, friend
  }

  /**
   * Pops two values (a,b) and pushes the result of the fn(a,b)
   * @param  function fn
   */
  dyad = (fn) => {
    this.push(fn(this.pop(), this.pop()));
  }

  /**
   * Get the instruction at the current pointer location,
   * then advance the pointer. Skips everything that isn't a whitespace symbol.
   * @return Symbol
   */
  nextInstruction = () => {
    let sym = null;
    while (sym === null) {
      if (this.i < 0 || this.i >= this.program.length) {
        this.raze(`Instruction pointer escaped program`);
      }
      let char = this.program[this.i];
      if (this.map[char] !== undefined) {
        sym = this.map[char];
      }
      this.i++;
    }
    return sym;
  }

  /**
   * Get the number at the current pointer location,
   * while advancing the pointer.
   *
   * Numbers begin with a [sign] symbol.
   * The sign symbol is either [tab] -> negative, or [space] -> positive.
   *
   * Numbers end with a [terminal] symbol: [line-feed].
   *
   * Between the sign symbol and the terminal symbol are binary digits
   * [space] -> binary-0, or [tab] -> binary-1.
   *
   * A number expression [sign][terminal] will be treated as zero.
   *
   * The expression of just [terminal] should throw an error. (The Haskell implementation is inconsistent about this.)
   *
   * @return number
   */
  nextNumber = () => {
    let sym;
    let sign;
    let value = 0;

    while (true) { // live dangerously
      sym = this.nextInstruction();

      if (sign === undefined) {
        if (sym === this.sym.l) { // sign is either tab or space
          this.raze(`Bad number format - no sign`);
        } else {
          sign = (sym === this.sym.s) ? 1 : -1;
          continue;
        }
      }

      if (sym === this.sym.l) { break; } // terminate

      value = value << 1;
      value += (sym === this.sym.t ? 1 : 0);
    }

    return value * sign;
  }

  /**
   * Get the label at the current pointer position,
   * while advancing the pointer.
   *
   * Labels begin with any number of [tab] and [space] characters.
   *
   * Labels end with a terminal symbol: [line-feed].
   *
   * Unlike with numbers, the expression of just [terminal] is valid.
   *
   * @return string
   */
  nextLabel = () => {
    let sym;
    let label = '';
    while (true) {
      sym = this.nextInstruction();
      label += this.labelMap[sym];
      if (sym === this.sym.l) {
        break; // break on line-feed
      }
    }
    return label;
  }

  /**
   * ELBERETH
   */
  inscribeSymbols = () => {
    this.sym = {
      s: Symbol('space'),
      t: Symbol('tab'),
      l: Symbol('line'),
    };
  }

  /**
   * You are standing in a grimy room, holding a dim lamp.
   * There are three doors in front of you.
   */
  drawMap = () => {
    this.map = {};
    this.map[String.fromCharCode(32)] = this.sym.s;
    this.map[String.fromCharCode(9)]  = this.sym.t;
    this.map[String.fromCharCode(10)] = this.sym.l;

    this.labelMap = {};
    this.labelMap[this.sym.s] = 's';
    this.labelMap[this.sym.t] = 't';
    this.labelMap[this.sym.l] = 'l';
  }

  /**
   * All labels must be known before program execution,
   * since commands may refer to labels which come both before and after them.
   *
   * Parse through the program, adding every label to the index.
   * When the program executes, encountering a label will perform a noop.
   *
   * Labels must be unique.
   */
  carveLabels = () => {
    let imp;
    let requiresNumber = [this.pushNum, this.duplicateN, this.discardN];
    let requiresLabel = [this.callLabel, this.jumpLabel, this.jumpLabelZero, this.jumpLabelNegative];

    while (this.i < this.program.length) {
      try {
        imp = this.fetchImp();
      } catch (e) { // finished reading instructions
        break;
      }

      if (imp === undefined) {
        this.raze(`Could not find IMP while labelling`);
      }

      if (imp === this.markLabel) {
        this.setLabel(this.nextLabel(), this.i); // add to label index
      } else if (requiresNumber.indexOf(imp) !== -1) {
        this.nextNumber(); // have to advance past numbers
      } else if (requiresLabel.indexOf(imp) !== -1) {
        this.nextLabel(); // have to advance past label calls
      }
    }

    this.i = 0; // reset instruction pointer
  }

  /**
   * Traverse the tree of imps, and snatch one from its branches.
   * Returns undefined if no imp is to be found along the instruction path.
   * @return mixed
   */
  fetchImp = () => {
    let p; // path
    let imp = null;
    let tree = this.imps;
    while (imp === null) {
      p = this.nextInstruction();
      if (tree[p] === undefined) {
        this.raze(`Invalid instruction encountered (${p})`);
      } else if (typeof tree[p] === 'function') {
        imp = tree[p];
      } else {
        tree = tree[p];
      }
    }
    return imp;
  }

  /**
   * Irizarry watched as the tiny screeching creatures poured from the portal.
   * Their numbers were immense, and he knew it would be difficult to enthrall
   * such mischievous devils. Leaning on his staff, he began the incantation.
   */
  summonImps = () => {
    let {s,t,l} = this.sym;

    // initialize imps
    let imps = {};
    imps[s] = {};
    imps[t] = {};
    imps[l] = {};

    // stack manipulation
    imps[s][t] = {};
    imps[s][l] = {};
    imps[s][s]    = this.pushNum;
    imps[s][t][s] = this.duplicateN;
    imps[s][t][l] = this.discardN;
    imps[s][l][s] = this.duplicate;
    imps[s][l][t] = this.swap;
    imps[s][l][l] = this.discard;

    // arithmetic
    imps[t][s] = {};
    imps[t][s][s] = {};
    imps[t][s][t] = {};
    imps[t][s][s][s] = this.addition;
    imps[t][s][s][t] = this.subtraction;
    imps[t][s][s][l] = this.multiplication;
    imps[t][s][t][s] = this.division;
    imps[t][s][t][t] = this.modulo;

    // heap access
    imps[t][t] = {};
    imps[t][t][s] = this.popAndStore;
    imps[t][t][t] = this.popAndStack;

    // input/output
    imps[t][l] = {};
    imps[t][l][s] = {};
    imps[t][l][t] = {};
    imps[t][l][s][s] = this.writeChar;
    imps[t][l][s][t] = this.writeNum;
    imps[t][l][t][s] = this.readChar;
    imps[t][l][t][t] = this.readNum;

    // flow control
    imps[l][s] = {};
    imps[l][t] = {};
    imps[l][l] = {};
    imps[l][s][s] = this.markLabel;
    imps[l][s][t] = this.callLabel;
    imps[l][s][l] = this.jumpLabel;
    imps[l][t][s] = this.jumpLabelZero;
    imps[l][t][t] = this.jumpLabelNegative;
    imps[l][t][l] = this.returnExecution;
    imps[l][l][l] = this.endProgram;

    this.imps = imps; // good luck, winged ones
  }

  /**
   * Raises an error, razes execution.
   * @param  string message
   */
  raze = (message) => {
    throw `${message} (instruction: ${this.i - 1})`;
  }
}
