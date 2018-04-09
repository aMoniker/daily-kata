/**
 * Ping Pong
 * https://www.codewars.com/kata/57542b169a4524d7d9000b68
 */

function playPingPong(pos, dir) {
  let h = 200;
  let w = 300;
  let m = (dir === 'DL' ? -1 : 1);
  let btm = Math.abs(pos + (h * m));
  if (btm > w) { btm = w - (btm % w); }
  return Math.max(10, Math.min(290, btm));
}

// Examples
// console.log(playPingPong(10, 'DL'));
// console.log(playPingPong(210, 'DL'));
// console.log(playPingPong(200, 'DL'));
// console.log(playPingPong(100, 'DR'));
// console.log(playPingPong(210, 'DR'));
// console.log(playPingPong(10, 'DR'));
