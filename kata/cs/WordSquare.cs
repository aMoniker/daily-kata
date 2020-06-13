namespace myjinxin
{
  using System;
  using System.Linq;

  public class Kata
  {
    public bool WordSquare(string letters)
    {
      double sqrt = Math.Sqrt(letters.Length);
      int odds = (int)sqrt;
      if (sqrt != odds) return false;
      var map = new bool[26];
      foreach (char c in letters)
      {
        var i = (int)Char.ToLower(c) - 97;
        map[i] = !map[i];
      }
      return map.Where(p => p).Count() <= odds;
    }
  }
}
