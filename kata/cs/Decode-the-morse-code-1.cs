// https://www.codewars.com/kata/54b724efac3d5402db00065e/train/csharp

using System;
using System.Collections.Generic;

class MorseCodeDecoder1
{
  private static Dictionary<string, string> map = new Dictionary<string, string>()
  {
    {".-", "A"},
    {"-...", "B"},
    {"-.-.", "C"},
    {"-..", "D"},
    {".", "E"},
    {"..-.", "F"},
    {"--.", "G"},
    {"....", "H"},
    {"..", "I"},
    {".---", "J"},
    {"-.-", "K"},
    {".-..", "L"},
    {"--", "M"},
    {"-.", "N"},
    {"---", "O"},
    {".--.", "P"},
    {"--.-", "Q"},
    {".-.", "R"},
    {"...", "S"},
    {"-", "T"},
    {"..-", "U"},
    {"...-", "V"},
    {".--", "W"},
    {"-..-", "X"},
    {"-.--", "Y"},
    {"--..", "Z"},
    {".----", "1"},
    {"..---", "2"},
    {"...--", "3"},
    {"....-", "4"},
    {".....", "5"},
    {"-....", "6"},
    {"--...", "7"},
    {"---..", "8"},
    {"----.", "9"},
    {"-----", "0"},
    {"...---...", "SOS"},
    {"-.-.--", "!"},
    {".-.-.-", "."},
  };

  public static string Decode(string morseCode)
  {
    morseCode = morseCode.Trim();
    string output = "";
    string[] words = morseCode.Split("   ");
    for (int i = 0; i < words.Length; i++)
    {
      string[] chars = words[i].Split(" ");
      foreach (string c in chars)
      {
        if (!map.ContainsKey(c))
        {
          throw new ArgumentException($"Code not found: {c}");
        }
        output += map[c];
      }
      if (i != words.Length - 1) output += " ";
    }
    return output;
  }
}
