// https://www.codewars.com/kata/523a86aa4230ebb5420001e1/train/csharp

using System;
using System.Collections.Generic;

public static class WhereMyAnagramsAtKata
{
  public static List<string> Anagrams(string word, List<string> words)
  {
    List<string> ret = new List<string>();
    foreach (string checkWord in words)
    {
      if (IsAnagram(word, checkWord)) ret.Add(checkWord);
    }
    return ret;
  }

  private static bool IsAnagram(string s1, string s2)
  {
    if (s1.Length != s2.Length) return false;

    Dictionary<char, int> charCounts = new Dictionary<char, int>();
    foreach (char c in s1)
    {
      if (!charCounts.ContainsKey(c))
      {
        charCounts.Add(c, 0);
      }
      charCounts[c]++;
    }

    foreach (char c in s2)
    {
      if (!charCounts.ContainsKey(c))
      {
        return false;
      }
      charCounts[c]--;
      if (charCounts[c] < 0) return false;
    }

    return true;
  }
}
