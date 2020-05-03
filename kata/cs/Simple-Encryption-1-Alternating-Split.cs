// https://www.codewars.com/kata/57814d79a56c88e3e0000786/train/csharp

using System;

public class SimpleEncryptionAlternatingSplitKata
{
  public static string Encrypt(string text, int n)
  {
    if (text == null) return null;
    string ret = text;
    for (int i = 0; i < n; i++)
    {
      string even = "";
      string odd = "";
      for (int j = 0; j < ret.Length; j++)
      {
        if (j % 2 == 0)
        {
          even += ret[j];
        }
        else
        {
          odd += ret[j];
        }
      }
      ret = odd + even;
    }
    return ret;
  }

  public static string Decrypt(string encryptedText, int n)
  {
    if (encryptedText == null) return null;
    string ret = encryptedText;
    int mid = (int)Math.Floor((double)encryptedText.Length / 2);
    for (int i = 0; i < n; i++)
    {
      string one = ret.Substring(mid);
      string two = ret.Substring(0, mid);
      ret = "";
      for (int j = 0; j <= mid; j++)
      {
        if (one.Length > j) ret += one[j];
        if (two.Length > j) ret += two[j];
      }
    }
    return ret;
  }
}
