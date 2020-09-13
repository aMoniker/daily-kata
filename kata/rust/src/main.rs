mod help_the_bookseller;

fn main() {
  // let a = vec!["BBAR 150", "CDXE 515", "BKWR 250", "BTSQ 890", "DRTY 600"];
  // let b = vec!["A", "B", "C", "D"];
  let a = vec![];
  let b = vec!["B", "R", "D", "X"];

  let result = help_the_bookseller::stock_list(a, b);
  println!("result: {}", result);
}
