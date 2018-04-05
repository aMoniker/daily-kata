/**
 * Data Munging - Part Three
 *
 * http://codekata.com/kata/kata04-data-munging/
 */

import fs from 'fs';

class SpreadFinder {
  loadDataFile = (file) => {
    return fs.readFileSync(`${__dirname}/../data/${file}`).toString();
  }

  findMinSpread = (rows, nameIndex, lowIndex, highIndex) => {
    return rows.reduce((val, row) => {
      let spread = Math.abs(
        Number.parseInt(row[highIndex]) - Number.parseInt(row[lowIndex])
      );
      if (spread < val.spread) {
        val.name = row[nameIndex];
        val.spread = spread;
      }
      return val;
    }, { name: null, spread: Infinity });
  }

  getMinSpread = (file, nameIndex, lowIndex, highIndex, rowSplitFunc) => {
    let data = this.loadDataFile(file);
    let rows = rowSplitFunc(data);
    return this.findMinSpread(rows, nameIndex, lowIndex, highIndex);
  }
}

let weather = new SpreadFinder().getMinSpread(
  'weather.dat', 0, 2, 1, (data) => {
    return data.split(/\n/).slice(2, -2)
               .map((row) => row.split(/\s+/).slice(1))
               ;
  }
);
console.log(`Smallest temperature spread is ${weather.spread} degrees on day ${weather.name}`);

let football = new SpreadFinder().getMinSpread(
  'football.dat', 1, 6, 8, (data) => {
    return data.split(/\n/).slice(1)
               .filter((row) => !row.match(/---/) && row !== '')
               .map((row) => row.split(/\s+/).slice(1))
               ;
});
console.log(`Smallest goal spread is ${football.spread} point(s) for team ${football.name}`);
