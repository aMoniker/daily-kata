using System;
using System.Linq;
using System.Collections.Generic;

public static class WhereMyAnagramsAt2Kata
{
  public static List<string> Anagrams(string word, List<string> words)
  {
    var matches = new List<string>();
    var hist = word.GroupBy(c => c).ToDictionary(c => c.Key, n => n.Count());

    foreach (string w in words)
    {
      if (w.Length != word.Length) continue;
      var whist = new Dictionary<char, int>();
      bool match = true;
      foreach (char c in w)
      {
        if (!hist.ContainsKey(c))
        {
          match = false;
          break;
        }
        if (!whist.ContainsKey(c)) whist[c] = 0;
        whist[c]++;
        if (whist[c] > hist[c])
        {
          match = false;
          break;
        }
      }
      if (match) matches.Add(w);
    }

    return matches;
  }
}
