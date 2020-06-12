using System;
using System.Linq;
using System.Collections.Generic;

class SamePrimeFactors
{
  public static int[] SameFactRev(int nMax)
  {
    int smallest = 1089;
    if (nMax <= smallest) return new int[] { };

    var maxPrime = Convert.ToInt32(
      Math.Floor((Math.Pow(10, nMax.ToString().Length) - 1) / 2)
    );
    var primes = GeneratePrimes(maxPrime);

    var revDict = new Dictionary<int, bool>();
    for (int x = smallest; x < nMax; x++)
    {
      if (revDict.ContainsKey(x)) continue;
      var nx = x.ToString();
      var rx = Reverse(nx);
      if (nx == rx) continue; // skip self-same palindromes
      var r = Convert.ToInt32(rx);

      bool hasPrimeFactor = false;
      bool samePrimeFactors = true;
      foreach (int p in primes)
      {
        if (p > x && p > r) break;
        int modx = x % p;
        int modr = r % p;
        if (modx == 0 && modr == 0)
        {
          hasPrimeFactor = true;
          continue;
        }
        if (((modx == 0) ^ (modr == 0)))
        {
          samePrimeFactors = false;
          break;
        }
      }

      if (hasPrimeFactor && samePrimeFactors)
      {
        revDict[x] = true;
        if (r < nMax) revDict[r] = true;
      }
    }

    int[] ret = new int[revDict.Count];
    int i = 0;
    foreach (KeyValuePair<int, bool> p in revDict)
    {
      ret[i] = p.Key;
      i++;
    }
    return ret.OrderBy(x => x).ToArray();
  }

  private static string Reverse(string s)
  {
    var a = s.ToCharArray();
    Array.Reverse(a);
    return new String(a);
  }

  private static List<int> GeneratePrimes(int max)
  {
    var primes = new List<int> { 2 };
    for (int x = 3; x <= max; x += 2)
    {
      bool prime = true;
      foreach (int p in primes)
      {
        if (x % p == 0)
        {
          prime = false;
          break;
        }
      }
      if (prime) primes.Add(x);
    }
    return primes;
  }
}
