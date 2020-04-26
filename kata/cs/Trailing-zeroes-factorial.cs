// Number of trailing zeroes of N!
// https://www.codewars.com/kata/52f787eb172a8b4ae1000a34/train/csharp

using System;

public static class TrailingZeroesFactorialKata
{
  public static int TrailingZeros(int n)
  {
    int zeroes = 0;
    int highestPowerOfFive = (int)Math.Floor(Math.Log(n, 5));
    for (int x = highestPowerOfFive; x > 0; x--)
    {
      zeroes += (int)Math.Floor(n / Math.Pow(5, x));
    }
    return zeroes;
  }
}
