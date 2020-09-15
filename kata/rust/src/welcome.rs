use std::collections::HashMap;

pub fn greet(language: &str) -> &str {
  let mut map: HashMap<&str, &str> = HashMap::new();
  let english = "Welcome";
  let pairs = vec![
    ("english", english),
    ("czech", "Vitejte"),
    ("danish", "Velkomst"),
    ("dutch", "Welkom"),
    ("estonian", "Tere tulemast"),
    ("finnish", "Tervetuloa"),
    ("flemish", "Welgekomen"),
    ("french", "Bienvenue"),
    ("german", "Willkommen"),
    ("irish", "Failte"),
    ("italian", "Benvenuto"),
    ("latvian", "Gaidits"),
    ("lithuanian", "Laukiamas"),
    ("polish", "Witamy"),
    ("spanish", "Bienvenido"),
    ("swedish", "Valkommen"),
    ("welsh", "Croeso"),
  ];
  map.extend(pairs);

  let phrase = map.get(language);
  let mut ret = english;
  match phrase {
    Some(&v) => ret = v,
    None => (),
  }

  return ret;
}
