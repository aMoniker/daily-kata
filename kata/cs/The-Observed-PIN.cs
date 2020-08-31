// https://www.codewars.com/kata/5263c6999e0f40dee200059d/train/csharp

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class TheObservedPINKata
{
  private static Dictionary<char, char[]> map = new Dictionary<char, char[]> {
    { '1', new char[] { '1', '2', '4' } },
    { '2', new char[] { '2', '1', '3', '5'} },
    { '3', new char[] { '3', '2', '6'} },
    { '4', new char[] { '4', '1', '5', '7'} },
    { '5', new char[] { '5', '2', '4', '6', '8'} },
    { '6', new char[] { '6', '3', '5', '9'} },
    { '7', new char[] { '7', '4', '8'} },
    { '8', new char[] { '8', '5', '7', '9', '0'} },
    { '9', new char[] { '9', '6', '8'} },
    { '0', new char[] { '0', '8'} },
  };

  public static List<string> GetPINs(string observed)
  {
    var possible = new List<char[]> { observed.ToCharArray() };
    for (int i = 0; i < observed.Length; i++)
    {
      possible = GetExplodedCombos(possible, i);
    }
    return possible.Select(c => String.Join("", c)).ToList();
  }

  private static List<char[]> GetExplodedCombos(List<char[]> combos, int idx)
  {
    var newCombos = new List<char[]>();
    foreach (char[] combo in combos)
    {
      foreach (char possible in map[combo[idx]])
      {
        var newCombo = (char[])combo.Clone();
        newCombo[idx] = possible;
        newCombos.Add(newCombo);
      }
    }
    return newCombos;
  }
}
