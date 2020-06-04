// https://www.codewars.com/kata/51c8e37cee245da6b40000bd/train/csharp

using System;

public class StripCommentsSolution
{
  public static string StripComments(string text, string[] commentSymbols)
  {
    string[] lines = text.Split("\n");
    for (int i = 0; i < lines.Length; i++)
    {
      double earliest = double.PositiveInfinity;
      foreach (string sym in commentSymbols)
      {
        int pos = lines[i].IndexOf(sym);
        if (pos != -1 && pos < earliest) earliest = (double)pos;
      }
      if (!double.IsInfinity(earliest))
      {
        lines[i] = lines[i].Substring(0, (int)earliest);
      }
      lines[i] = lines[i].TrimEnd();
    }
    return String.Join("\n", lines);
  }
}
