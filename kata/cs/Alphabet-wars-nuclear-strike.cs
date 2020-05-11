// https://www.codewars.com/kata/59437bd7d8c9438fb5000004/train/csharp

using System;
using System.Text.RegularExpressions;

public class AlphabetWarsNuclearStrikeKata
{
  public static string AlphabetWar(string s)
  {
    if (!s.Contains('#')) return s.Replace("[", "").Replace("]", "");

    char[] field = s.ToCharArray();
    for (int i = 0; i < field.Length; i++)
    {
      if (field[i] == '[' || field[i] == ']') field[i] = '2';
    }

    NukeUnprotected(field);
    for (int i = 0; i < field.Length; i++)
    {
      if (field[i] == '#') DetonateNuke(i, field);
    }

    return Regex.Replace(String.Join("", field), @"[^a-z]", "");
  }

  private static void NukeUnprotected(char[] field)
  {
    bool bunker = false;
    for (int i = 0; i < field.Length; i++)
    {
      if (field[i] == '2') { bunker = !bunker; continue; }
      if (!bunker && field[i] != '#') field[i] = '.';
    }
  }

  private static void DetonateNuke(int pos, char[] field)
  {
    field[pos] = '_';
    DetonateNukeDirection(pos, field, 1);
    DetonateNukeDirection(pos, field, -1);
  }

  private static void DetonateNukeDirection(int pos, char[] field, int step)
  {
    int walls = 0;
    bool dead = false;
    for (int i = pos + step; (i < field.Length && i >= 0); i += step)
    {
      if (field[i] == '0') break;
      if (field[i] == '1') { walls++; dead = true; }
      if (field[i] == '2') { field[i] = '1'; walls++; }
      if (dead) field[i] = '0';
      if (walls == 2) break;
    }
  }
}
