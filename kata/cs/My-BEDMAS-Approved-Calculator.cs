using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class BEDMASApprovedCalculatorKata
{
  public static double calculate(string s)
  {
    s = Regex.Replace(s, @"\s+", "");

    Regex innerBrackets = new Regex(@"(\([^\(\)]+\))");
    while (innerBrackets.IsMatch(s))
    {
      MatchCollection matches = innerBrackets.Matches(s);
      foreach (Match match in matches)
      {
        double result = Evaluate(match.Value);
        s = s.Replace(match.Value, result.ToString());
      }
    }

    return Evaluate(s);
  }

  private static double Evaluate(string s)
  {
    Regex numbersOperators = new Regex(@"([\d\.]+)([+^\-\/*])*");
    MatchCollection matches = numbersOperators.Matches(s);

    List<string> terms = new List<string>();

    foreach (Match match in matches)
    {
      for (int i = 1; i < match.Groups.Count; i++)
      {
        terms.Add(match.Groups[i].Value);
      }
    }

    string[] precedence = new string[] { "^", "/", "*", "+", "-" };
    foreach (string op in precedence)
    {
      for (int i = 0; i < terms.Count; i++)
      {
        if (terms[i] != op) continue;
        double a = Convert.ToDouble(terms[i - 1]);
        double b = Convert.ToDouble(terms[i + 1]);
        terms[i - 1] = Operate(a, b, op).ToString();
        terms.RemoveRange(i, 2);
        i = i - 1;
      }
    }

    return Convert.ToDouble(terms[0]);
  }

  private static double Operate(double a, double b, string op)
  {
    switch (op)
    {
      case "+": return a + b;
      case "-": return a - b;
      case "*": return a * b;
      case "/": return a / b;
      case "^": return Math.Pow(a, b);
    }
    return 0;
  }
}
