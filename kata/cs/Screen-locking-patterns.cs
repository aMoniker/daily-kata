// https://www.codewars.com/kata/585894545a8a07255e0002f1/train/csharp

using System;
using System.Linq;
using System.Collections.Generic;

using QueueItem = System.ValueTuple<
  char, System.Collections.Generic.List<char>
>;

public static class ScreenLockingPatternsKata
{
  private static Dictionary<char, char[]> map = new Dictionary<char, char[]> {
    {'A', new char[]{'B', 'F', 'E', 'H', 'D'}},
    {'B', new char[]{'C', 'F', 'I', 'E', 'G', 'D', 'A'}},
    {'C', new char[]{'F', 'H', 'E', 'D', 'B'}},
    {'D', new char[]{'A', 'B', 'C', 'E', 'I', 'H', 'G'}},
    {'E', new char[]{'A', 'B', 'C', 'D', 'F', 'G', 'H', 'I'}},
    {'F', new char[]{'A', 'B', 'C', 'E', 'G', 'H', 'I'}},
    {'G', new char[]{'D', 'B', 'E', 'F', 'H'}},
    {'H', new char[]{'G', 'D', 'A', 'E', 'C', 'F', 'I'}},
    {'I', new char[]{'H', 'D', 'E', 'B', 'F'}},
  };

  private static Dictionary<char, (char, char)[]> unlocks =
  new Dictionary<char, (char, char)[]> {
    {'A', new (char, char)[]{('B', 'C'), ('E', 'I'), ('D', 'G')}},
    {'B', new (char, char)[]{('E', 'H')}},
    {'C', new (char, char)[]{('B', 'A'), ('E', 'G'), ('F', 'I')}},
    {'D', new (char, char)[]{('E', 'F')}},
    {'E', new (char, char)[]{}},
    {'F', new (char, char)[]{('E', 'D')}},
    {'G', new (char, char)[]{('D', 'A'), ('E', 'C'), ('H', 'I')}},
    {'H', new (char, char)[]{('E', 'B')}},
    {'I', new (char, char)[]{('H', 'G'), ('E', 'A'), ('F', 'C')}},
  };

  public static int CountPatternsFrom(char firstDot, int length)
  {
    if (length < 1 || length > 9) return 0;
    if (length == 1) return 1;

    int count = 0;

    List<QueueItem> firstItems = MakeNewQueueItems(firstDot, new List<char>());
    Queue<QueueItem> queue = new Queue<QueueItem>(firstItems);

    while (queue.Count > 0)
    {
      (char next, List<char> used) = queue.Dequeue();
      if (used.Count + 1 == length)
      {
        count++;
        continue;
      }
      List<QueueItem> newItems = MakeNewQueueItems(next, used);
      foreach (QueueItem item in newItems) queue.Enqueue(item);
    }

    return count;
  }

  private static List<QueueItem> MakeNewQueueItems(char cur, List<char> used)
  {
    List<QueueItem> items = new List<QueueItem>();

    List<char> unlocked = new List<char>();
    foreach ((char ifUsed, char thenUnlock) in unlocks[cur])
    {
      if (used.Contains(ifUsed)) unlocked.Add(thenUnlock);
    }

    char[] possible = map[cur].Union(unlocked)
                              .Where(x => !used.Contains(x))
                              .ToArray();

    List<char> nowUsed = used.Union(new char[] { cur }).ToList();
    foreach (char next in possible) items.Add((next, nowUsed));

    return items;
  }
}
