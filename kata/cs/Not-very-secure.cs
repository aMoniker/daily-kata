// Not very secure
// https://www.codewars.com/kata/526dbd6c8c0eb53254000110/train/csharp

using System;
using System.Text.RegularExpressions;

public class NotVerySecureKata
{
  public static bool Alphanumeric(string str)
  {
    return new Regex("^[a-z0-9]+$", RegexOptions.IgnoreCase).IsMatch(str);
  }
}
