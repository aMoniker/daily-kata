// https://www.codewars.com/kata/5552101f47fc5178b1000050/train/csharp

using System;
using System.Collections.Generic;

public class DigPow
{
  public static long digPow(int n, int p)
  {
    double nx = n;
    double sum = 0;
    List<double> digits = new List<double>();
    while (nx > 0)
    {
      digits.Add(nx % 10);
      nx = Math.Floor(nx / 10);
    }
    digits.Reverse();
    foreach (int d in digits)
    {
      sum += Math.Pow(d, p);
      p++;
    }
    if (sum % n == 0) return (long)(sum / n);
    return -1;
  }
}
