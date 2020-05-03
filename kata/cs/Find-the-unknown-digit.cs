// https://www.codewars.com/kata/546d15cebed2e10334000ed9/train/csharp

using System;
using System.Text.RegularExpressions;

public class Runes
{
  public static int solveExpression(string expression)
  {
    Regex rx = new Regex(@"^(\-?[0-9?]+)([+\-*]{1})(\-?[0-9?]+)=(\-?[0-9?]+)$");
    MatchCollection matches = rx.Matches(expression);

    if (matches.Count == 0) return -1;

    string t1 = matches[0].Groups[1].Value;
    string op = matches[0].Groups[2].Value;
    string t2 = matches[0].Groups[3].Value;
    string t3 = matches[0].Groups[4].Value;

    bool[] usedDigits = new bool[10];
    foreach (char c in t1 + t2 + t3)
    {
      if (c == '?' || c == '-') continue;
      usedDigits[Convert.ToInt32(c.ToString())] = true;
    }

    for (int i = 0; i <= 9; i++)
    {
      if (usedDigits[i]) continue;

      string s1 = t1.Replace('?', Convert.ToChar(i + 48));
      string s2 = t2.Replace('?', Convert.ToChar(i + 48));
      string s3 = t3.Replace('?', Convert.ToChar(i + 48));

      if (i == 0)
      {
        if (s1.StartsWith("00")) continue;
        if (s2.StartsWith("00")) continue;
        if (s3.StartsWith("00")) continue;
      }

      int int1 = Convert.ToInt32(s1);
      int int2 = Convert.ToInt32(s2);
      int int3 = Convert.ToInt32(s3);

      int? res = null;
      switch (op)
      {
        case "+":
          res = int1 + int2;
          break;
        case "-":
          res = int1 - int2;
          break;
        case "*":
          res = int1 * int2;
          break;
      }

      if (int3 == res)
      {
        return i;
      }
    }

    return -1;
  }
}
