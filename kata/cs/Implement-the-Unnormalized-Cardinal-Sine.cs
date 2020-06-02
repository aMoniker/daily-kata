// https://www.codewars.com/kata/5963a29980509479cd000075/train/csharp

using System;

public class Complex
{
  public static ComplexNumber Sinc(ComplexNumber z)
  {
    double a = ComplexNumber.Re(z);
    double b = ComplexNumber.Im(z);

    if (a == 0) return new ComplexNumber(1);

    ComplexNumber sinz = new ComplexNumber(
      Math.Sin(a) * Math.Cosh(b), Math.Cos(a) * Math.Sinh(b)
    );

    ComplexNumber conz = new ComplexNumber(a, -1 * b);
    ComplexNumber num = Mult(sinz, conz);
    ComplexNumber den = Mult(z, conz);

    double na = ComplexNumber.Re(num);
    double nb = ComplexNumber.Im(num);
    double da = ComplexNumber.Re(den);

    return new ComplexNumber(na / da, nb / da);
  }

  public static ComplexNumber Mult(ComplexNumber z1, ComplexNumber z2)
  {
    double z1a = ComplexNumber.Re(z1);
    double z1b = ComplexNumber.Im(z1);
    double z2a = ComplexNumber.Re(z2);
    double z2b = ComplexNumber.Im(z2);
    double real = z1a * z2a + z1b * z2b * -1;
    double imag = z1a * z2b + z1b * z2a;
    return new ComplexNumber(real, imag);
  }

  public static ComplexNumber Sinc(int n)
  {
    return Sinc(new ComplexNumber(n));
  }

  public static ComplexNumber Sinc(float n)
  {
    return Sinc(new ComplexNumber(n));
  }

  public static ComplexNumber Sinc(double n)
  {
    return Sinc(new ComplexNumber(n));
  }
}
