pub fn reverse_words(string: &str) -> String {
  let mut result = String::with_capacity(string.len());
  for portion in string.split(' ') {
    for c in portion.chars().rev() {
      result.push(c);
    }
    result.push(' ');
  }
  result.pop();
  return result;
}
