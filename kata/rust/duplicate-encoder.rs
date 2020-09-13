use std::collections::HashMap;

fn duplicate_encode(word:&str)->String {
    let w: &str = &word.to_ascii_lowercase();
    let mut map: HashMap<char, u32> = HashMap::new();
    for c in w.chars() {
      *map.entry(c).or_insert(0) += 1;
    }
    let mut r = String::new();
    for c in w.chars() {
      match map.get(&c) {
        Some(&v) => r.push(if v > 1 { ')' } else { '(' }),
        _ => ()
      }
    }
    return r;
}
