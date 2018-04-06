/**
 * Assembler Interpreter
 * https://www.codewars.com/kata/assembler-interpreter-part-ii
 */

function assemblerInterpreter(code) {
  return (new Assembler).execute(code);
}

class Assembler {
  init = () => { // reset state
    this.registers = {};
    this.program = [];
    this.labels = {};
    this.cmp = undefined;
    this.ret = undefined;
    this.jmp = undefined;
    this.output = '';
    this.ended = false;
  }

  execute = (code) => {
    this.init();
    this.loadProgram(code);

    program:
    for (let i = 0; i < this.program.length; i++) {

      if (this.jmp !== undefined) { // handle jumps
        i = this.jmp;
        this.jmp = undefined;
      }

      let cmd, x, y;
      [cmd, x, y] = this.program[i]; // command and arguments

      if (cmd[cmd.length - 1] === ':') { continue; } // skip labels

      switch (cmd) {
        // basic
        case 'mov': this.move(x, y);          break;
        case 'end':
          this.ended = true;
          break program;

        // mathematical
        case 'inc': this.increment(x);        break;
        case 'dec': this.decrement(x);        break;
        case 'add': this.add(x, y);           break;
        case 'sub': this.subtract(x, y);      break;
        case 'mul': this.multiply(x, y);      break;
        case 'div': this.divide(x, y);        break;

        // jumps
        case 'cmp': this.compare(x, y);       break;
        case 'jmp': this.jump(x);             break;
        case 'jne': this.jumpNotEqual(x);     break;
        case 'je' : this.jumpEqual(x);        break;
        case 'jge': this.jumpGreaterEqual(x); break;
        case 'jg' : this.jumpGreater(x);      break;
        case 'jle': this.jumpLessEqual(x);    break;
        case 'jl' : this.jumpLess(x);         break;

        // special
        case 'msg':
          this.message(this.program[i].slice(1));
          break;
        case 'call':
          if (this.ret === undefined) {
            this.ret = i;
          }
          this.jump(x);
          break;
        case 'ret':
          i = this.ret;
          this.ret = undefined;
          break;
        default: break;
      }
    }

    return (this.ended ? this.output : -1);
  }

  move      = (x, y) => { this.registers[x] = this.value(y); }
  increment = (x)    => { this.registers[x]++; }
  decrement = (x)    => { this.registers[x]--; }
  add       = (x, y) => { this.registers[x] += this.value(y); }
  subtract  = (x, y) => { this.registers[x] -= this.value(y); }
  multiply  = (x, y) => { this.registers[x] *= this.value(y); }
  divide    = (x, y) => {
    this.registers[x] = Math.floor(this.registers[x] / this.value(y));
  }

  compare = (x, y) => {
    x = this.value(x);
    y = this.value(y);
    if (x === y) {
      this.cmp = 0;
    } else {
      this.cmp = (x > y ? 1 : -1);
    }
  }

  jump = (label) => {
    this.jmp = this.labels[label];
  }
  jumpNotEqual = (label) => {
    if (this.cmp !== 0) { this.jump(label); }
  }
  jumpEqual = (label) => {
    if (this.cmp === 0) { this.jump(label); }
  }
  jumpGreaterEqual = (label) => {
    if (this.cmp === 0 || this.cmp === 1) { this.jump(label); }
  }
  jumpGreater = (label) => {
    if (this.cmp === 1) { this.jump(label); }
  }
  jumpLessEqual = (label) => {
    if (this.cmp === 0 || this.cmp === -1) { this.jump(label); }
  }
  jumpLess = (label) => {
    if (this.cmp === -1) { this.jump(label); }
  }

  message = (messages) => {
    for (let m of messages) {
      this.output += (m[0] === `'`) ? m.slice(1, m.length - 1) : this.value(m);
    }
  }

  value = (x) => {
    return /[a-z]/.test(x) ? this.registers[x] : Number.parseInt(x);
  }

  loadProgram = (code) => {
    for (let line of code.split('\n')) {
      line = line.trim();
      if (line === '' || line[0] === ';') { continue; } // skip empty/comments
      this.program.push(this.parseLine(line));
      if (line[line.length - 1] === ':') { // load labels
        this.labels[line.substr(0, line.length - 1)] = this.program.length - 1;
      }
    }
  }

  parseLine = (line) => {
    if (line.substr(0, 3) === 'msg') { // special parsing for msg
      let inString = false;
      let terminateIndex = line.length;
      for (let i = 3; i < line.length; i++) { // handle comments not in strings
        if (line[i] === `'`) { inString = !inString; continue; }
        if (line[i] === ';' && !inString) { terminateIndex = i; break; }
      }
      line = line.substr(3, terminateIndex - 3);
      // match everything between quotes, and single letter register names
      let args = line.match(/(\'[^\']*\'|[a-z]{1})/g).map((a) => a.trim());
      args.unshift('msg');
      return args;
    }

    line = line.replace(/,?\s+/g, ' ');
    return line.split(' ');
  }
}

let program_first = `
; My first program
mov  a, 5
inc  a
call function
msg  '(5+1)/2 = ', a    ; output message
end

function:
    div  a, 2
    ret
`;
console.log(assemblerInterpreter(program_first));

let program_factorial = `
mov   a, 5
mov   b, a
mov   c, a
call  proc_fact
call  print
end

proc_fact:
    dec   b
    mul   c, b
    cmp   b, 1
    jne   proc_fact
    ret

print:
    msg   a, '! = ', c ; output text
    ret
`;
console.log(assemblerInterpreter(program_factorial));

let program_fibonacci = `
mov   a, 8            ; value
mov   b, 0            ; next
mov   c, 0            ; counter
mov   d, 0            ; first
mov   e, 1            ; second
call  proc_fib
call  print
end

proc_fib:
    cmp   c, 2
    jl    func_0
    mov   b, d
    add   b, e
    mov   d, e
    mov   e, b
    inc   c
    cmp   c, a
    jle   proc_fib
    ret

func_0:
    mov   b, c
    inc   c
    jmp   proc_fib

print:
    msg   'Term ', a, ' of Fibonacci series is: ', b        ; output text
    ret
`;
console.log(assemblerInterpreter(program_fibonacci));

let program_mod = `
mov   a, 11           ; value1
mov   b, 3            ; value2
call  mod_func
msg   'mod(', a, ', ', b, ') = ', d        ; output
end

; Mod function
mod_func:
    mov   c, a        ; temp1
    div   c, b
    mul   c, b
    mov   d, a        ; temp2
    sub   d, c
    ret
`;
console.log(assemblerInterpreter(program_mod));

let program_gcd = `
mov   a, 81         ; value1
mov   b, 153        ; value2
call  init
call  proc_gcd
call  print
end

proc_gcd:
    cmp   c, d
    jne   loop
    ret

loop:
    cmp   c, d
    jg    a_bigger
    jmp   b_bigger

a_bigger:
    sub   c, d
    jmp   proc_gcd

b_bigger:
    sub   d, c
    jmp   proc_gcd

init:
    cmp   a, 0
    jl    a_abs
    cmp   b, 0
    jl    b_abs
    mov   c, a            ; temp1
    mov   d, b            ; temp2
    ret

a_abs:
    mul   a, -1
    jmp   init

b_abs:
    mul   b, -1
    jmp   init

print:
    msg   'gcd(', a, ', ', b, ') = ', c
    ret
`;
console.log(assemblerInterpreter(program_gcd));

let program_fail = `
call  func1
call  print
end

func1:
    call  func2
    ret

func2:
    ret

print:
    msg 'This program should return -1'
`;
console.log(assemblerInterpreter(program_fail));

let program_power = `
mov   a, 2            ; value1
mov   b, 10           ; value2
mov   c, a            ; temp1
mov   d, b            ; temp2
call  proc_func
call  print
end

proc_func:
    cmp   d, 1
    je    continue
    mul   c, a
    dec   d
    call  proc_func

continue:
    ret

print:
    msg a, '^', b, ' = ', c
    ret
`;
console.log(assemblerInterpreter(program_power));
