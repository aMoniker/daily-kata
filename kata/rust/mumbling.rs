// https://www.codewars.com/kata/5667e8f4e3f572a8f2000039/train/rust

fn main() {
  println!("{}", accum("ZpglnRxqenU"));
  assert_eq!(accum("ZpglnRxqenU"), "Z-Pp-Ggg-Llll-Nnnnn-Rrrrrr-Xxxxxxx-Qqqqqqqq-Eeeeeeeee-Nnnnnnnnnn-Uuuuuuuuuuu");
}

fn accum(s:&str)->String {
  let mut words: Vec<String> = Vec::new();
  let mut count: usize = 0;
  for c in s.chars() {
    let string = c.to_ascii_uppercase().to_string()
               + &c.to_ascii_lowercase().to_string().repeat(count);
    words.push(string);
    count += 1;
  }
  return words.join("-");
}
