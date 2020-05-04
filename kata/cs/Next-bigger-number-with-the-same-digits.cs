// https://www.codewars.com/kata/55983863da40caa2c900004e/train/csharp

using System;
using System.Linq;

public class NextBiggerNumberWithTheSameDigitsKata
{
  public static long NextBiggerNumber(long n)
  {
    if (n < 10) return -1;

    int[] nums = n.ToString().ToCharArray().Select(
      i => Convert.ToInt32(i.ToString())
    ).ToArray();

    nums = NextBiggerNumberArray(nums);
    long next = Convert.ToInt64(String.Join("", nums));
    if (next == n) return -1;

    return next;
  }

  private static int[] NextBiggerNumberArray(int[] nums)
  {
    double largestJ = double.NegativeInfinity;
    int[] ret = new int[nums.Length];
    nums.CopyTo(ret, 0);

    for (int i = nums.Length - 1; i >= 1; i--)
    {
      for (int j = i - 1; j >= 0; j--)
      {
        if (i == largestJ) return ret;
        if (nums[i] <= nums[j]) continue;
        if (j <= largestJ) continue;

        largestJ = j;
        nums.CopyTo(ret, 0);

        int swp = ret[i];
        ret[i] = ret[j];
        ret[j] = swp;

        Array.Sort(ret, j + 1, ret.Length - j - 1);
      }
    }
    return ret;
  }
}
