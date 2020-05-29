using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TopWords
{
  public static List<string> Top3(string s)
  {
    s = s.Replace("\n", " ");
    s = Regex.Replace(s, @"[^a-z'\s]", " ", RegexOptions.IgnoreCase);

    Regex validWord = new Regex(@"[a-z]", RegexOptions.IgnoreCase);
    Dictionary<string, int> wordCount = new Dictionary<string, int>();

    string[] words = s.Split(" ");
    foreach (string word in words)
    {
      if (word == "") continue;
      if (!validWord.IsMatch(word)) continue;
      string lowerWord = word.ToLower();
      if (!wordCount.ContainsKey(lowerWord)) wordCount.Add(lowerWord, 0);
      wordCount[lowerWord]++;
    }

    // doing it this way as a challenge to avoid sorting wordCount
    (string, int)[] topWords = new (string, int)[3];
    foreach (KeyValuePair<string, int> pair in wordCount)
    {
      for (int i = 0; i < topWords.Length; i++)
      {
        if (pair.Value > topWords[i].Item2)
        {
          for (int j = topWords.Length - 1; j > i; j--)
          {
            topWords[j] = topWords[j - 1];
          }
          topWords[i] = (pair.Key, pair.Value);
          break;
        }
      }
    }

    List<string> top = new List<string>();
    foreach ((string word, int count) in topWords)
    {
      if (count > 0) top.Add(word);
    }

    return top;
  }
}
