// https://www.codewars.com/kata/54b72c16cd7f5154e9000457/train/csharp

using System;
using System.Text.RegularExpressions;

public class MorseCodeDecoder
{
  public static string DecodeBits(string bits)
  {
    bits = bits.Trim('0');
    double unit = Double.PositiveInfinity;
    int streak = 0;

    char streakChar = '1';
    for (int i = 0; i < bits.Length; i++)
    {
      if (bits[i] == streakChar) streak++;
      if (bits[i] != streakChar || i == bits.Length - 1)
      {
        if (streak > 0 && streak < unit) unit = streak;
        streakChar = bits[i];
        streak = 1;
      }
    }

    streak = 0;
    streakChar = '1';
    string output = "";
    for (int i = 0; i < bits.Length; i++)
    {
      if (bits[i] == streakChar) streak++;
      if (bits[i] != streakChar || i == bits.Length - 1)
      {
        int unitStreak = Convert.ToInt32(streak / unit);
        output += ConvertSignalCharsToMorse(streakChar, unitStreak);
        streakChar = bits[i];
        streak = 1;
      }
    }
    if (
      unit == 1 && bits.Length > 1 && bits.Substring(bits.Length - 2, 2) == "01"
    ) {
      output += ConvertSignalCharsToMorse(bits[bits.Length - 1], 1);
    }

    return output;
  }

  private static string ConvertSignalCharsToMorse(char c, int count)
  {
    if (c == '1')
    {
      switch (count)
      {
        case 1:
          return ".";
        case 3:
          return "-";
      }
    }
    else if (c == '0')
    {
      switch (count)
      {
        case 1:
          return ""; // pause between dots/dashes
        case 3:
          return  " ";
        case 7:
          return "   ";
      }
    }
    return "";
  }

  public static string DecodeMorse(string morseCode)
  {
    string output = "";
    string[] words = morseCode.Split("   ");
    for (int i = 0; i < words.Length; i++)
    {
      string[] characters = words[i].Split(" ");
      foreach (string c in characters)
      {
        output += MorseCode.Get(c);
      }
      if (i != words.Length - 1) output += " ";
    }
    return output;
  }
}
