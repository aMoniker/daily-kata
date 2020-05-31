using System;

public class PerfectPower
{
  public static (int, int)? IsPerfectPower(int n)
  {
    if (n <= 1) return null;

    double tolerance = 0.000000000001;
    double limit = Math.Ceiling(Math.Sqrt(n));
    for (int x = 2; x <= limit; x++)
    {
      double r = Math.Pow(n, 1.0 / x);
      double rr = Math.Round(r);
      if (Math.Abs(rr - r) <= tolerance) return ((int)rr, x);
    }

    return null;
  }
}
