using System;
using System.Text.RegularExpressions;

public class BinomialExpansionKataSolution
{
  public static string Expand(string expr)
  {
    Regex rx = new Regex(
      @"^\((\-?[0-9]*)([a-z])([\-\+])(\-?[0-9]+)\)\^([0-9]+)$"
    );
    Match m = rx.Match(expr);
    if (m.Groups.Count != 6) return "wrong format!";

    int a = 0;
    if (m.Groups[1].Value == "")
    {
      a = 1;
    }
    else if (m.Groups[1].Value == "-")
    {
      a = -1;
    }
    else
    {
      a = Convert.ToInt32(m.Groups[1].Value);
    }
    string x = m.Groups[2].Value;
    string op = m.Groups[3].Value;
    int b = Convert.ToInt32(m.Groups[4].Value);
    int p = Convert.ToInt32(m.Groups[5].Value);

    if (p == 0) return "1";

    if (b == 0)
    {
      string pp = p > 1 ? $"^{p}" : "";
      double co = Math.Pow(a, p);
      string coeff = $"{co}";
      if (co == 1) coeff = "";
      if (co == -1) coeff = "-";
      return $"{coeff}{x}{pp}";
    }

    double[] row = GeneratePascalRow(p);
    string[] terms = new string[row.Length];

    int ap = p;
    int bp = 0;
    for (int i = 0; i < row.Length; i++)
    {
      double val = Math.Pow(a, ap) * Math.Pow(b, bp) * row[i];
      string pow = ap > 1 ? $"^{ap}" : "";
      string variable = ap > 0 ? $"{x}" : "";

      string coefficient = $"{val}";
      if (i != row.Length - 1)
      {
        if (val == 1) coefficient = "";
        if (val == -1) coefficient = "-";
      }
      string term = $"{coefficient}{variable}{pow}";
      terms[i] = term;
      ap--;
      bp++;
    }

    bool alt = op == "-";
    string ret = terms[0];
    for (int i = 1; i < terms.Length; i++)
    {
      if (terms[i].Length > 0 && terms[i][0] == '-')
      {
        string mop = op == "-" ? "+" : "-";
        ret += $"{mop}{terms[i].Substring(1)}";
      }
      else
      {
        ret += $"{op}{terms[i]}";
      }
      if (alt) op = op == "-" ? "+" : "-";
    }
    return ret;
  }

  private static double[] GeneratePascalRow(int n)
  {
    double[] prevRow = new double[] { 1 };
    if (n == 0) return prevRow;

    for (int i = 1; i <= n; i++)
    {
      double[] nextRow = new double[i + 1];
      for (int j = 0; j < nextRow.Length; j++)
      {
        double a = (j - 1) >= 0 ? prevRow[j - 1] : 0;
        double b = j < prevRow.Length ? prevRow[j] : 0;
        nextRow[j] = a + b;
      }
      prevRow = nextRow;
    }

    return prevRow;
  }
}
