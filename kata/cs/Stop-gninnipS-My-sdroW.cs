// https://www.codewars.com/kata/5264d2b162488dc400000001/train/csharp

using System;

public class WordSpinningKata
{
  public static string SpinWords(string sentence)
  {
    string[] words = sentence.Split(" ");
    for (int i = 0; i < words.Length; i++)
    {
      if (words[i].Length >= 5)
      {
        char[] c = words[i].ToCharArray();
        Array.Reverse(c);
        words[i] = new string(c);
      }
    }
    return String.Join(" ", words);
  }
}
