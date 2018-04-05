/**
 * Modified Befunge Interpreter
 *
 * https://www.codewars.com/kata/befunge-interpreter/train/javascript
 */

function interpret(code) {
  return (new BefungeInterpreter).execute(code);
}

class BefungeInterpreter {
  constructor() {
    this.DIR = {
      right : Symbol('right'),
      left  : Symbol('left'),
      down  : Symbol('down'),
      up    : Symbol('up'),
    };
  }

  init = () => { // reset values to default
    this.stack = [];              // call stack
    this.curDir = this.DIR.right; // current direction
    this.stringMode = false;      // are we in "string mode?"
    this.trampoline = false;      // trampoline to skip next char
    this.position = {x:0,y:0};    // current execution position
    this.output = '';             // output string is built during execution
    this.program = [];            // 2d array representing planar program
    this.rows = 0;                // rows in the program
    this.cols = 0;                // columns in the program
  }

  loadProgram = (code) => { // store code as array of arrays
    let rows = code.split('\n');
    this.rows = rows.length;
    this.cols = rows.reduce((a, r) => r.length > a ? r.length : a, -Infinity);
    rows = rows.map((r) => r.padEnd(this.cols, ' '));
    this.program = rows.map((r) => r.split(''));
  }

  execute = (code) => {
    this.init(); // reset state
    this.loadProgram(code);

    program:
    while (true) {
      let inst = this.program[this.position.y][this.position.x];

      if (this.stringMode && inst !== '"') {
        this.push(inst.charCodeAt());
        this.advancePosition();
        continue;
      }

      if (this.trampoline) {
        this.trampoline = false;
        this.advancePosition();
        continue;
      }

      switch (inst) {
        // push all integers directly onto the stack
        case '0': case '1': case '2': case '3': case '4':
        case '5': case '6': case '7': case '8': case '9':
                  this.push(inst);         break;

        // mathematical operations
        case '+': this.addition();         break;
        case '-': this.subtraction();      break;
        case '*': this.multiplication();   break;
        case '/': this.division();         break;
        case '%': this.modulo();           break;

        // logical operations
        case '!': this.logicalNot();       break;
        case '`': this.greaterThan();      break;

        // movement operations
        case '>': this.directionRight();   break;
        case '<': this.directionLeft();    break;
        case 'v': this.directionDown();    break;
        case '^': this.directionUp();      break;
        case '?': this.directionRandom();  break;
        case '_': this.horizontalSwitch(); break;
        case '|': this.verticalSwitch();   break;

        // stack operations
        case ':': this.duplicate();        break;
        case '\\':this.swap();             break;
        case '$': this.discard();          break;

        // output operations
        case '.': this.outputInteger();    break;
        case ',': this.outputCharacter();  break;

        // metaprogramming
        case 'p': this.putCall();          break;
        case 'g': this.getCall();          break;

        // special
        case '"': this.toggleStringMode(); break;
        case '#': this.trampoline = true;  break;
        case '@': /* halt execution */     break program;
        default:  /* advance to next */    break;
      }

      this.advancePosition();
    }

    return this.output;
  }

  push = (...values) => {
    values.forEach((val) => { // the stack may only contain integers
      this.stack.push(Number.parseInt(val));
    });
  }

  pop = () => {
    return this.stack.pop();
  }

  // pop two values off the stack, then push the result of a callback
  dyad = (callback) => {
    this.push(callback(this.pop(), this.pop()));
  }

  addition = () => {
    this.dyad((a, b) => a + b);
  }

  subtraction = () => {
    this.dyad((a, b) => b - a);
  }

  multiplication = () => {
    this.dyad((a, b) => a * b);
  }

  division = () => {
    this.dyad((a, b) => a === 0 ? a : Math.floor(b / a));
  }

  modulo = () => {
    this.dyad((a, b) => a === 0 ? a : b % a);
  }

  logicalNot = () => {
    this.push(this.pop() === 0 ? 1 : 0);
  }

  greaterThan = () => {
    this.dyad((a, b) => b > a ? 1 : 0);
  }

  directionRight = () => { this.curDir = this.DIR.right; }
  directionLeft  = () => { this.curDir = this.DIR.left; }
  directionDown  = () => { this.curDir = this.DIR.down; }
  directionUp    = () => { this.curDir = this.DIR.up; }

  directionRandom = () => {
    let dirs = ['Right', 'Left', 'Up', 'Down'];
    let rand = Math.floor(Math.random() * dirs.length);
    this[`direction${dirs[rand]}`]();
  }

  horizontalSwitch = () => {
    this.pop() === 0 ? this.directionRight() : this.directionLeft();
  }

  verticalSwitch = () => {
    this.pop() === 0 ? this.directionDown() : this.directionUp();
  }

  toggleStringMode = () => {
    this.stringMode = !this.stringMode;
  }

  duplicate = () => {
    if (this.stack.length === 0) { return this.push(0); }
    let val = this.pop();
    this.push(val, val);
  }

  swap = () => {
    if (this.stack.length === 0) { return; }
    let a = this.pop();
    let b = this.pop();
    if (b === undefined) { b = 0; }
    this.push(a, b);
  }

  discard = () => {
    this.pop();
  }

  outputInteger = () => {
    this.output += this.pop();
  }

  outputCharacter = () => {
    this.output += String.fromCharCode(this.pop());
  }

  putCall = () => {
    let y = this.pop();
    let x = this.pop();
    this.program[y][x] = String.fromCharCode(this.pop());
  }

  getCall = () => {
    let y = this.pop();
    let x = this.pop();
    this.push(this.program[y][x].charCodeAt());
  }

  advancePosition = () => {
    switch (this.curDir) {
      case this.DIR.right:
        this.adjustPosition({x:1,y:0}); break;
      case this.DIR.left:
        this.adjustPosition({x:-1,y:0}); break;
      case this.DIR.down:
        this.adjustPosition({x:0,y:1}); break;
      case this.DIR.up:
        this.adjustPosition({x:0,y:-1}); break;
    }
  }

  adjustPosition = (amt = {}) => {
    let x = this.position.x + amt.x;
    let y = this.position.y + amt.y;

    if (x < 0) {
      y--;
      x = this.cols - Math.abs(x);
    } else if (x >= this.cols) {
      y++;;
      x = x % this.cols;
    }

    if (y < 0) {
      x--;
      y = this.rows - Math.abs(y);
    } else if (y >= this.rows) {
      x++;
      y = y % this.rows;
    }

    if (x >= this.cols && y >= this.rows) {
      x = 0;
      y = 0;
    } else if (x < 0 && y < 0) {
      x = this.cols - 1;
      y = this.rows - 1;
    }

    this.position.x = x;
    this.position.y = y;
  }
}

// https://github.com/uxitten/polyfill/blob/master/string.polyfill.js
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/padEnd
if (!String.prototype.padEnd) {
  String.prototype.padEnd = function padEnd(targetLength,padString) {
    targetLength = targetLength>>0; //floor if number or convert non-number to 0;
    padString = String((typeof padString !== 'undefined' ? padString : ' '));
    if (this.length > targetLength) {
      return String(this);
    }
    else {
      targetLength = targetLength-this.length;
      if (targetLength > padString.length) {
        padString += padString.repeat(targetLength/padString.length); //append to original to ensure we are longer than needed
      }
      return String(this) + padString.slice(0,targetLength);
    }
  };
}

// example
// console.log(interpret('>25*"!dlroW olleH":v\n                v:,_@\n                >  ^'));
