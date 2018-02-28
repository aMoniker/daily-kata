/**
 * Fizzbuzz
 */
let times = 100;
for (let i = 0; i < times; i++) {
    let mod3 = i % 3 === 0;
    let mod5 = i % 5 === 0;
    let output = i;
    if (mod3 && mod5) {
        output = 'fizzbuzz';
    } else if (mod3) {
        output = 'fizz';
    } else if (mod5) {
        output = 'buzz';
    }
    console.log(output);
}
