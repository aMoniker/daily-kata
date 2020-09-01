using System;
using System.Linq;
using System.Collections.Generic;

public static class CatchingCarMileageNumbersKata
{
  public static int IsInteresting(int number, List<int> awesomePhrases)
  {
    if (IsInterestingNumber(number, awesomePhrases)) return 2;
    foreach (int i in new int[] { 1, 2 })
    {
      if (IsInterestingNumber(number + i, awesomePhrases)) return 1;
    }
    return 0;
  }

  private static bool IsInterestingNumber(int number, List<int> extra)
  {
    if (number <= 99) return false;
    return extra.Contains(number)
        || HasTrailingZeroes(number)
        || HasUniformDigits(number)
        || HasSequentialDigits(number)
        || HasPalindromicDigits(number)
        ;
  }

  private static bool HasTrailingZeroes(int number)
  {
    return Convert.ToInt32(number.ToString().Substring(1)) == 0;
  }

  private static bool HasUniformDigits(int number)
  {
    var s = number.ToString();
    foreach (char c in s)
    {
      if (c != s[0]) return false;
    }
    return true;
  }

  private static bool HasSequentialDigits(int number)
  {
    var d = number.ToString().Select(c => Char.GetNumericValue(c)).ToArray();
    if (d.Length > 10 || d.Length < 2) return false;
    var step = d[1] - d[0];
    if (Math.Abs(step) != 1) return false;
    for (int i = 2; i < d.Length; i++)
    {
      if (d[i] == 0 && step == 1 && d[i - 1] == 9) return true;
      if (d[i] - d[i - 1] != step) return false;
    }
    return true;
  }

  private static bool HasPalindromicDigits(int number)
  {
    var s = number.ToString();
    for (int i = 0; i < (int)Math.Floor(Convert.ToDecimal(s.Length / 2)); i++)
    {
      if (s[i] != s[s.Length - i - 1]) return false;
    }
    return true;
  }
}
