use std::collections::HashMap;

pub fn ordered_count(sip: &str) -> Vec<(char, i32)> {
  let mut ret: Vec<(char, i32)> = Vec::new();
  let mut map: HashMap<char, i32> = HashMap::new();

  for c in sip.chars() {
    *map.entry(c).or_insert(0) += 1;
  }

  for c in sip.chars() {
    let v = map.entry(c).or_insert(0);
    if *v != 0 {
      ret.push((c, *v));
      *v = 0;
    }
  }

  return ret;
}
