// https://www.codewars.com/kata/513e08acc600c94f01000001/train/csharp

using System;
using System.Linq;

public class RGBToHexConversionKata
{
  public static string Rgb(int r, int g, int b)
  {
    return String.Join(null, (new[] { r, g, b }).ToList().Select(
      i => Math.Max(0, Math.Min(255, i)).ToString("X2").ToUpper()
    ));
  }
}
