using System;
using System.Collections.Generic;
using System.Linq;

public class DidYouMeanKata
{
  private IEnumerable<string> words;

  public DidYouMeanKata(IEnumerable<string> words)
  {
    this.words = words;
  }

  public string FindMostSimilar(string term)
  {
    string smallestWord = "";
    double smallestDistance = double.PositiveInfinity;

    foreach (string word in words)
    {
      int distance = LevenshteinDistance(term, word);
      if (distance < smallestDistance)
      {
        smallestWord = word;
        smallestDistance = Convert.ToDouble(distance);
      }
    }

    return smallestWord;
  }

  private int LevenshteinDistance(string s, string t)
  {
    int m = s.Length;
    int n = t.Length;
    int[,] d = new int[m + 1, n + 1];

    for (int i = 1; i <= m; i++) d[i, 0] = i;
    for (int j = 1; j <= n; j++) d[0, j] = j;

    for (int j = 1; j <= n; j++)
    {
      for (int i = 1; i <= m; i++)
      {
        int del = d[i - 1, j] + 1;
        int ins = d[i, j - 1] + 1;
        int sub = d[i - 1, j - 1] + (s[i - 1] == t[j - 1] ? 0 : 1);
        d[i, j] = Math.Min(del, Math.Min(ins, sub));
      }
    }

    return d[m, n];
  }
}
