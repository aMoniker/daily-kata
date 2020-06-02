using System;

public class Complex
{
  public static double[] Exp(double[] z)
  {
    double x = z[0]; // Real part of z = x + iy
    double y = z[1]; // Imaginary part of z = x + iy
    double ex = Math.Pow(Math.E, x);
    return new double[] { ex * Math.Cos(y), ex * Math.Sin(y) };
  }
}
