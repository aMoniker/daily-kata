pub fn hor_mirror(s: String) -> String {
  return s.split('\n').rev().collect::<Vec<&str>>().join("\n");
}

pub fn vert_mirror(s: String) -> String {
  return s
    .split('\n')
    .map(|v| v.chars().rev().collect::<String>())
    .collect::<Vec<String>>()
    .join("\n");
}

pub fn oper(f: fn(String) -> String, s: String) -> String {
  f(s)
}
