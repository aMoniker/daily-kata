// https://www.codewars.com/kata/525f4206b73515bffb000b21/train/csharp

using System;
using System.Numerics;

public class AddingBigNumbersKata
{
  public static string Add(string a, string b)
  {
    return BigInteger.Add(BigInteger.Parse(b), BigInteger.Parse(a)).ToString();
  }
}
