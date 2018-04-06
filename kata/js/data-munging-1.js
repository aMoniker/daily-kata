/**
 * Data Munging - Part One
 *
 * http://codekata.com/kata/kata04-data-munging/
 */

import fs from 'fs';

let weatherFile = `${__dirname}/../../data/weather.dat`;
let weatherData = fs.readFileSync(weatherFile).toString();

let rows = weatherData.split(/\n/);
let headings = rows[0].split(/\s+/).slice(1);
rows = rows.slice(2, -2).map((row) => row.split(/\s+/).slice(1));

const dayIndex = headings.indexOf('Dy');
const maxIndex = headings.indexOf('MxT');
const minIndex = headings.indexOf('MnT');

let minSpread = rows.reduce((values, current) => {
    let tempSpread = parseInt(current[maxIndex]) - parseInt(current[minIndex]);
    if (tempSpread < values.spread) {
        values.day = current[dayIndex];
        values.spread = tempSpread;
    }
    return values;
}, { day: 0, spread: Infinity });

console.log(`Smallest temperature spread is ${minSpread.spread} degrees on day ${minSpread.day}`);
