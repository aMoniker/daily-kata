using System;
using System.Diagnostics;

public class User
{
  public int rank { get; protected set; }
  public int progress { get; protected set; }

  public User(int r = -8)
  {
    rank = r;
    progress = 0;
  }

  public void incProgress(int n)
  {
    if (n > 8 || n < -8 || n == 0)
    {
      throw new ArgumentException($"{n} out of range");
    }

    bool cross = false;
    if (n != Math.Abs(n)) cross = !cross;
    if (rank != Math.Abs(rank)) cross = !cross;

    int d = rank - n;
    d -= (cross ? 1 : 0) * (d < 0 ? -1 : 1);

    if (d == 0)
    {
      progress += 3;
    }
    else if (d == 1)
    {
      progress += 1;
    }
    else if (d < 0)
    {
      progress += 10 * d * d;
    }

    while (progress >= 100)
    {
      progress -= 100;
      rank++;
      if (rank == 0) rank = 1;
      if (rank == 8) break;
    }

    if (rank == 8) progress = 0;
  }
}
