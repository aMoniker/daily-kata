/**
 * Binary Search
 */

let max = 10000;
let array = [];
for (let i = 0; i < max; i++) {
    array.push(i);
}

const chop = (int, array, start, end) => {
    start = (start === undefined) ? 0 : start;
    end = (end === undefined) ? array.length - 1 : end;

    if (start === end) {
        return (array[start] === int) ? start : -1;
    }

    let diff = end - start;
    let chopIndex = Math.ceil(diff / 2) + start;

    if (int > array[chopIndex]) {
        return chop(int, array, array[chopIndex + 1], end);
    } else {
        return chop(int, array, start, array[chopIndex - 1]);
    }
};

let chopped = chop(0, array);
console.log(chopped, array[chopped]);
