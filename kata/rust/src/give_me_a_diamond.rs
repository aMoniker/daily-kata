pub fn print(n: i32) -> Option<String> {
  if n <= 0 || n % 2 == 0 {
    return None;
  }

  let mut ret = String::new();
  let mid: i32 = (n as f32 / 2.0).floor() as i32;
  for i in 0..n {
    let mut j = i;
    if i > mid {
      j = n - i - 1
    }
    let star_count: i32 = 2 * j + 1;
    let space_count: i32 = (n - star_count) / 2;
    let stars = "*".repeat(star_count as usize);
    let spaces = " ".repeat(space_count as usize);
    ret.push_str(&format!("{}{}\n", spaces, stars));
  }

  return Some(ret);
}
