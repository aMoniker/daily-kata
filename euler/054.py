from typing import Dict, List, Tuple
Card = Tuple[int, str]
Hand = List[Card]
Round = Tuple[Hand, Hand]
Values = List[int]

def main():
    p1wins = 0
    for r in get_rounds():
        p1wins += 1 if get_winner(r) == 1 else 0
    print(f"player one wins: {p1wins}")

def get_winner(r: Round) -> int:
    p1h = r[0]
    p2h = r[1]
    ranks = [
        is_royal_flush, is_straight_flush, is_four_kind, is_full_house,
        is_flush, is_straight, is_three_kind, is_two_pair, is_one_pair
    ]
    for rank in ranks:
        p1 = rank(p1h)
        p2 = rank(p2h)
        if p1[0] and p2[0]:
            # if both players have same rank hand, check highest value in rank
            winner = get_values_winner(p1[1], p2[1])
            if not winner == 0: return winner
            # if ranked cards have same value, break to high value comparison
            break
        elif p1[0]:
            return 1
        elif p2[0]:
            return 2

    # if no ranks appeared, highest value wins
    return get_values_winner(get_values(p1h), get_values(p2h))

def get_values_winner(p1: Values, p2: Values):
    m1 = max(p1)
    m2 = max(p2)
    if m1 > m2: return 1
    if m2 > m1: return 2
    return 0

def get_hand_count(h: Hand) -> Dict[int, int]:
    hc = {}
    for card in h:
        val = get_value(card)
        if not val in hc: hc[val] = 0
        hc[val] += 1
    return hc

def get_pair_count(h: Hand) -> int:
    pairs = 0
    for _,count in get_hand_count(h).items(): pairs += 1 if count == 2 else 0
    return pairs

def get_pairs(h: Hand) -> List[int]:
    pairs = []
    for value,count in get_hand_count(h).items():
        if count == 2: pairs.append(value)
    return pairs

def is_one_pair(h: Hand) -> Tuple[bool, Values]:
    pairs = get_pairs(h)
    return (len(pairs) == 1, pairs)

def is_two_pair(h: Hand) -> Tuple[bool, Values]:
    pairs = get_pairs(h)
    return (len(pairs) == 2, pairs)

def is_three_kind(h: Hand) -> Tuple[bool, Values]:
    for value,count in get_hand_count(h).items():
        if count == 3: return (True, [value])
    return (False, [])

def is_straight(h: Hand) -> Tuple[bool, Values]:
    values = sorted([ get_value(c) for c in h ])
    for i in range(1, len(values)):
        if values[i] - values[i-1] != 1: return (False, [])
    return (True, values)

def is_flush(h: Hand) -> Tuple[bool, Values]:
    suit = get_suit(h[0])
    for card in h[1:]:
        if get_suit(card) != suit: return (False, [])
    return (True, [c[0] for c in h])

def is_full_house(h: Hand) -> Tuple[bool, Values]:
    for _,count in get_hand_count(h).items():
        if count != 2 or count != 3: return (False, [])
    return (True, [c[0] for c in h])

def is_four_kind(h: Hand) -> Tuple[bool, Values]:
    for value,count in get_hand_count(h).items():
        if count == 4: return (True, [value])
    return (False, [])

def is_straight_flush(h: Hand) -> Tuple[bool, Values]:
    if is_straight(h)[0] and is_flush(h)[0]:
        return (True, [c[0] for c in h])
    return (False, [])

def is_royal_flush(h: Hand) -> Tuple[bool, Values]:
    royals = [10, 11, 12, 13, 14]
    for card in h:
        if get_value(card) not in royals: return (False, [])
    return is_straight_flush(h)

def get_value(c: Card) -> int:
    return c[0]

def get_values(h: Hand) -> Values:
    return [get_value(c) for c in h]

def get_suit(c: Card) -> str:
    return c[1]

def get_rounds() -> List[Round]:
    return [(
        [parse_card(c) for c in cards[:5]],
        [parse_card(c) for c in cards[5:]],
    ) for cards in [d.split() for d in read_data()]]

def parse_card(raw_card: str) -> Tuple[int, str]:
    face = raw_card[:-1]
    values = {
        'T': 10,
        'J': 11,
        'Q': 12,
        'K': 13,
        'A': 14,
    }
    value = values.get(face, face)
    return (int(value), raw_card[-1:])

def read_data() -> List[str]:
    file = open("./data/054.txt", mode="r")
    text = file.read()
    file.close()
    return text.splitlines()

if __name__ == '__main__':
    main()
