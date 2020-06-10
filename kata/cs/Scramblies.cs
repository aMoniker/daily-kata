using System;
using System.Collections.Generic;

public class Scramblies
{
  public static bool Scramble(string str1, string str2)
  {
    var map = new Dictionary<char, int>();
    foreach (char c in str1)
    {
      if (!map.ContainsKey(c)) map[c] = 0;
      map[c]++;
    }

    foreach (char c in str2)
    {
      if (!map.ContainsKey(c)) return false;
      map[c]--;
      if (map[c] < 0) return false;
    }

    return true;
  }
}
