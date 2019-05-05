/**
 * Data Munging - Part Two
 *
 * http://codekata.com/kata/kata04-data-munging/
 */

import fs from 'fs';

let footballFile = `${__dirname}/football.dat`;
let footballData = fs.readFileSync(footballFile).toString();

let rows = footballData.split(/\n/);
rows = rows.slice(1)
           .filter((row) => !row.match(/---/) && row !== '')
           .map((row) => row.split(/\s+/).slice(1))
           ;

let minSpread = rows.reduce((values, current) => {
    let goalSpread = Math.abs(parseInt(current[6]) - parseInt(current[8]));
    if (goalSpread < values.spread) {
        values.team = current[1];
        values.spread = goalSpread;
    }
    return values;
}, { day: 0, spread: Infinity });

console.log(`Smallest goal spread is ${minSpread.spread} point(s) for team ${minSpread.team}`);
