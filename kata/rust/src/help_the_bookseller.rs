use std::collections::HashMap;

pub fn stock_list(list_art: Vec<&str>, list_cat: Vec<&str>) -> String {
  if list_art.len() == 0 || list_cat.len() == 0 {
    return String::new();
  }

  let mut map: HashMap<String, u32> = HashMap::new();
  for x in list_art {
    let num: String = x.split(" ").skip(1).take(1).collect();
    let amt = num.parse::<u32>().unwrap();
    let c = x.chars().nth(0);
    if c.is_some() {
      *map.entry(c.unwrap().to_string()).or_insert(0) += amt;
    }
  }

  let mut entries: Vec<String> = Vec::new();
  for cat in list_cat {
    match map.get(cat) {
      Some(&v) => {
        let entry = format!("({} : {})", cat, v.to_string());
        entries.push(entry);
      }
      None => {
        let entry = format!("({} : 0)", cat);
        entries.push(entry);
      }
    }
  }

  return entries.join(" - ");
}
