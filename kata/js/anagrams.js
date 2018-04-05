/**
 * Anagrams
 *
 * http://codekata.com/kata/kata06-anagrams/
 */

import fs from 'fs';
import readline from 'readline';

console.time();

const map = {};
const dictFile = '/usr/share/dict/words';

const makeKey = (word) => {
  let letters = '0'.repeat(26);
  for (let i = 0; i < word.length; i++) {
    let letterPosition = Number.parseInt(word[i], 36) - 10;
    let letterCount = Number.parseInt(letters[letterPosition], 10) + 1;
    let sliceA = letters.slice(0, letterPosition);
    let sliceB = letters.slice(letterPosition + 1);
    letters = `${sliceA}${letterCount}${sliceB}`;
  }
  return letters;
};

const addToMap = (word) => {
  let key = makeKey(word);
  if (map[key] === undefined) { map[key] = []; }
  map[key].push(word);
};

const getAnagrams = (word) => {
  return (map[makeKey(word)] || []).filter((w) => {
    return w !== word;
  });
};

const lineReader = readline.createInterface({
  input: fs.createReadStream(dictFile)
});

lineReader.on('line', (word) => {
  addToMap(word);
});

lineReader.on('close', () => {
  console.timeEnd();
  console.log('anagram sets:', Object.keys(map).length);

  let words = [
    'rots', 'kinship', 'enlist', 'boaster', 'fresher', 'knits', 'rots',
    'crepitus', 'punctilio', 'paste', 'sunders',
  ];
  words.forEach((word) => {
    console.log(`anagrams for ${word}:`, getAnagrams(word));
  });
});
