using System;
using System.Linq;

public class PyramidSlideDown
{
  public static int LongestSlideDown(int[][] pyramid)
  {
    for (int y = 1; y < pyramid.Length; y++)
    {
      for (int x = 0; x < pyramid[y].Length; x++)
      {
        int m = pyramid[y][x];
        int a = (x - 1 >= 0) ? pyramid[y - 1][x - 1] : 0;
        int b = (x < pyramid[y - 1].Length) ? pyramid[y - 1][x] : 0;
        pyramid[y][x] = Math.Max(m + a, m + b);
      }
    }
    return pyramid.Last().Max();
  }
}
