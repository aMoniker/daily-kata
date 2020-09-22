pub fn gps(s: i32, x: Vec<f64>) -> i32 {
  if x.len() <= 1 {
    return 0;
  }

  let mut max: f64 = 0.0;
  for i in 1..x.len() {
    let delta: f64 = x[i] - x[i - 1];
    let speed = (3600.00 * delta) / s as f64;
    if speed > max {
      max = speed;
    }
  }

  return max.floor() as i32;
}
