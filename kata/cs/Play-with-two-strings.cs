// https://www.codewars.com/kata/56c30ad8585d9ab99b000c54/train/csharp

namespace smile67Kata
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  public class Kata
  {
    private Dictionary<char, bool> FindSwaps(string s)
    {
      Dictionary<char, bool> d = new Dictionary<char, bool>();
      foreach (char c in s)
      {
        char key = Char.ToLower(c);
        if (!d.ContainsKey(key)) d[key] = false;
        d[key] = !d[key];
      }
      return d;
    }

    private string MakeSwaps(string s, Dictionary<char, bool> d)
    {
      char[] chars = s.ToCharArray();
      foreach (KeyValuePair<char, bool> x in d)
      {
        if (!x.Value) continue;
        char clower = x.Key;
        char cupper = Char.ToUpper(clower);
        for (int i = 0; i < chars.Length; i++)
        {
          if (chars[i] == clower)
          {
            chars[i] = cupper;
          }
          else if (chars[i] == cupper)
          {
            chars[i] = clower;
          }
        }
      }
      return String.Join("", chars);
    }

    public string workOnStrings(string a, string b)
    {
      return MakeSwaps(a, FindSwaps(b)) + MakeSwaps(b, FindSwaps(a));
    }
  }
}
