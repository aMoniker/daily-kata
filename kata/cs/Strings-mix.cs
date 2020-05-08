// https://www.codewars.com/kata/5629db57620258aa9d000014/train/csharp

using System;
using System.Collections.Generic;
using System.Linq;

public class Mixing
{
  public static string Mix(string s1, string s2)
  {
    int[] c1 = LowercaseCounts(s1);
    int[] c2 = LowercaseCounts(s2);
    List<string> ret = new List<string>();

    for (int i = 0; i < c1.Length; i++)
    {
      int max = Math.Max(c1[i], c2[i]);
      if (max < 2) continue;
      char symbol = c1[i] == c2[i] ? '9' : c1[i] > c2[i] ? '1' : '2';
      ret.Add($"{symbol}:{new String((char)(i + 97), max)}");
    }

    ret = ret.OrderByDescending(x => x.Length).ThenBy(y => y).ToList();
    return String.Join('/', ret).Replace('9', '=');
  }

  private static int[] LowercaseCounts(string s)
  {
    int[] counts = new int[26];
    foreach (char c in s)
    {
      UInt16 ci = Convert.ToUInt16(c);
      // lowercase a - z are 97 - 122 inclusive
      if (ci < 97 || ci > 122) continue;
      counts[ci - 97]++;
    }
    return counts;
  }
}
