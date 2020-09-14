pub fn xo(string: &'static str) -> bool {
  let mut m: i32 = 0;
  for c in string.to_lowercase().chars() {
    if c == 'x' {
      m += 1;
    }
    if c == 'o' {
      m -= 1;
    }
  }
  return m == 0;
}
