/**
 * Amazon Checkmate
 * https://www.codewars.com/kata/5897f30d948beb78580000b2
 */
const rows = 8;
const cols = 8;

function amazonCheckmate(king, amazon) {
  let board = makeNewBoard();

  // iterate over spaces around and including king, mark unsafe & invalid
  let kingCoords = getCoordsFromStandardNotation(king);
  board[getBoardIndex(kingCoords.col, kingCoords.row)].king = true;
  [-1,0,1].forEach((c) => {
    [-1,0,1].forEach((r) => {
      let col = kingCoords.col + c;
      let row = kingCoords.row + r;
      if (isValidSpace(col, row)) {
        let index = getBoardIndex(col, row);
        board[index].safe = false;
        board[index].valid = false;
      }
    });
  });

  // iterate over spaces around the amazon, marking them unsafe
  let amazonCoords = getCoordsFromStandardNotation(amazon);
  // the space which the amazon occupies
  board[getBoardIndex(amazonCoords.col, amazonCoords.row)].amazon = true;
  // the spaces directly around the amazon & her knight moves
  // form a grid two spaces out from the amazon's position
  [-2,-1,0,1,2].forEach((c) => {
    [-2,-1,0,1,2].forEach((r) => {
      let col = amazonCoords.col + c;
      let row = amazonCoords.row + r;
      if (isValidSpace(col, row)) {
        let index = getBoardIndex(col, row);
        board[index].safe = false;
      }
    });
  });

  // map the rows, cols, and diagonals extending outward from the amazon
  let count = 0;
  let validSpacesRemain = true;
  let kingBlockingPath = null;
  while (validSpacesRemain) {
    let hadValidSpace = false;
    [-1,0,1].forEach((c) => {
      [-1,0,1].forEach((r) => {
        if (c === 0 && r === 0) { return; }
        if (kingBlockingPath && kingBlockingPath === `${c}:${r}`) {
          return;
        }
        let col = amazonCoords.col + (count * c);
        let row = amazonCoords.row + (count * r);
        if (col === kingCoords.col && row === kingCoords.row) {
          kingBlockingPath = `${c}:${r}`;
          return;
        }
        if (isValidSpace(col, row)) {
          hadValidSpace = true;
          board[getBoardIndex(col, row)].safe = false;
        }
      });
    });
    validSpacesRemain = hadValidSpace;
    count++;
  }

  // iterate over every index in the board
  let checkmates = 0;
  let checks = 0;
  let stalemates = 0;
  let normals = 0;
  for (let c = 0; c < cols; c++) {
    for (let r = 0; r < rows; r++) {
      let space = board[getBoardIndex(c, r)];
      if (!space.valid || space.amazon) { continue; }
      if (!space.safe) {
        // determine if there is at least one safe move around the king (check)
        let hasSafeMove = false;
        [-1,0,1].forEach((ci) => {
          if (hasSafeMove) { return; }
          [-1,0,1].forEach((ri) => {
            if (hasSafeMove) { return; }
            if (ci === 0 && ri === 0) { return; }
            let col = c + ci;
            let row = r + ri;
            if (isValidSpace(col, row)) {
              let s = board[getBoardIndex(col, row)];
              if (s.valid && (s.safe || s.amazon)) {
                hasSafeMove = true;
              }
            }
          });
        });

        if (hasSafeMove) {
          checks++;
        } else { // otherwise it's checkmate
          checkmates++;
        }
      } else {
        // determine if there is at least one safe move (normal)
        let hasSafeMove = false;
        [-1,0,1].forEach((ci) => {
          if (hasSafeMove) { return; }
          [-1,0,1].forEach((ri) => {
            if (hasSafeMove) { return; }
            if (ci === 0 && ri === 0) { return; }
            let col = c + ci;
            let row = r + ri;
            if (isValidSpace(col, row)) {
              let s = board[getBoardIndex(col, row)];
              if (s.valid && s.safe) {
                hasSafeMove = true;
              }
            }
          });
        });
        if (hasSafeMove) {
          normals++;
        } else { // otherwise it's stalemate
          stalemates++;
        }
      }
    }
  }

  return [checkmates, checks, stalemates, normals];
}

function makeNewBoard() {
  let board = {};
  for (let col = 0; col < cols; col++) {
    for (let row = 0; row < rows; row++) {
      board[getBoardIndex(col, row)] = {safe:true, valid:true};
    }
  }
  return board;
}

function getBoardIndex(col, row) {
  return `${col}:${row}`;
}

function getCoordsFromStandardNotation(notation) {
  // since we have 8x8 grid, this is only ever two characters
  // if you wanted to extend the solution to a larger board,
  // you'd have to parse this differently.
  let split = notation.split('');
  return { // coords are 0-based
    row: split[1] - 1,
    col: Number.parseInt(split[0], 36) - 10,
  };
}

function isValidRow(row) {
  return (row >= 0 && row < rows);
}

function isValidCol(col) {
  return (col >= 0 && col < cols);
}

// is a valid space within the range of the board
// does not check whether the board state of the space is valid
function isValidSpace(col, row) {
  return isValidCol(col) && isValidRow(row);
}

function debugBoard(board) {
  for (let r = rows - 1; r >= 0; r--) {
    let rowDisplay = `${r}: `;
    for (let c = 0; c < cols; c++) {
      let index = getBoardIndex(c, r);
      if (board[index].king) {
        rowDisplay += 'K';
      } else if (board[index].amazon) {
        rowDisplay += 'A';
      } else if (!board[index].valid) {
        rowDisplay += '%';
      } else if (!board[index].safe) {
        rowDisplay += 'X';
      } else {
        rowDisplay += 'O';
      }
      rowDisplay += ' ';
    }
    console.log(rowDisplay);
  }
  console.log(
    '   ' + Array.apply(null, {length: cols}).map(Number.call, Number).join(' ')
  );
}
