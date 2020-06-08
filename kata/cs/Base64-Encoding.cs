using System;
using System.Text;

public static class Base64Utils
{
  public static string ToBase64(string s)
  {
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
  }

  public static string FromBase64(string s)
  {
    return Encoding.UTF8.GetString(Convert.FromBase64String(s));
  }
}
