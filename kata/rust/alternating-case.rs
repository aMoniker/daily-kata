// https://www.codewars.com/kata/56efc695740d30f963000557/train/rust

fn to_alternating_case(s: &str) -> String {
  let mut r = String::new();
  for c in s.chars() {
      if c.is_ascii_uppercase() {
          r.push(c.to_ascii_lowercase());
      } else if c.is_ascii_lowercase() {
          r.push(c.to_ascii_uppercase());
      } else {
          r.push(c);
      }
  }
  return r;
}
