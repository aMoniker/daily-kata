/**
 * Sudoku Solution Validator
 * Accepts a 2D array (9x9) representing a Sudoku board
 * https://www.codewars.com/kata/529bf0e9bdf7657179000008
 */
function validSolution(grid) {
  let s;
  let flat = [];
  for (let row of grid) { flat = [...flat, ...row]; }

  // check that there are no zeroes
  // and that each row contains all nine digits
  for (let i = 0; i < flat.length; i++) {
    if (i % 9 === 0) { s = new Set; }
    if (flat[i] === 0) { return false; } // a zero anywhere instantly fails
    s.add(flat[i]);
    if (i % 9 === 8 && s.size !== 9) { return false; } // missing a digit
  }

  // check that each column has all nine digits
  for (let i = 0; i < 9; i++) {
    s = new Set;
    for (let j = 0; j < 81; j += 9) {
      s.add(flat[i + j]);
    }
    if (s.size !== 9) { return false; }
  }

  // check that each 3x3 sub-grid has all nine digits
  for (let i = 0; i < 81; i += 3) {
    let s = new Set;
    for (let j = 0; j < 3; j++) {
      for (let k = 0; k < 3; k++) {
        s.add(flat[i + j + (k * 9)]);
      }
    }
    if (s.size !== 9) { return false; }
    if (i % 9 === 6) { i += 21; }
  }

  return true;
}

// Example
// let s  = validSolution([[5, 3, 4, 6, 7, 8, 9, 1, 2],
//                         [6, 7, 2, 1, 9, 5, 3, 4, 8],
//                         [1, 9, 8, 3, 4, 2, 5, 6, 7],
//                         [8, 5, 9, 7, 6, 1, 4, 2, 3],
//                         [4, 2, 6, 8, 5, 3, 7, 9, 1],
//                         [7, 1, 3, 9, 2, 4, 8, 5, 6],
//                         [9, 6, 1, 5, 3, 7, 2, 8, 4],
//                         [2, 8, 7, 4, 1, 9, 6, 3, 5],
//                         [3, 4, 5, 2, 8, 6, 1, 7, 9]]);
// console.log(s);
