pub fn high_and_low(numbers: &str) -> String {
  let mut high: i32 = i32::MIN;
  let mut low: i32 = i32::MAX;

  for num in numbers.split(' ') {
    let x = num.parse::<i32>().unwrap();
    if x > high {
      high = x;
    }
    if x < low {
      low = x;
    }
  }

  return format!("{} {}", high, low);
}
