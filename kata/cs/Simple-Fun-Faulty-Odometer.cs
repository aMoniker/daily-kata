// https://www.codewars.com/kata/58b8d22560873d9068000085/train/csharp

namespace myjinxin
{
  using System;

  public class FaultyOdometerKata
  {
    public int FaultyOdometer(int n)
    {
      string s = n.ToString();
      int mul = 1;
      int sum = 0;
      for (int i = s.Length - 1; i >= 0; i--)
      {
        int val = (int)Char.GetNumericValue(s[i]);
        if (val > 4) val--;
        sum += val * mul;
        mul *= 9;
      }
      return sum;
    }
  }
}
