// https://www.codewars.com/kata/52bb6539a4cf1b12d90005b7/train/csharp

namespace BattleshipFieldValidatorSolution
{
  using System;
  using System.Collections.Generic;

  public class BattleshipField
  {
    public static bool ValidateBattlefield(int[,] field)
    {
      Dictionary<int, int> ships = new Dictionary<int, int>();
      Dictionary<(int, int), bool> skips = new Dictionary<(int, int), bool>();

      for (int y = 0; y < field.GetLength(0); y++)
      {
        for (int x = 0; x < field.GetLength(1); x++)
        {
          if (skips.ContainsKey((y, x))) continue;
          if (field[y, x] == 0) continue;
          if (BordersDiagonalPiece(y, x, field)) return false;
          int xn = x;
          int yn = y;
          int size = 1;
          bool valid = true;
          bool first_piece = true;
          (int, int) expectedDir = (0, 0);
          while (true)
          {
            skips[(yn, xn)] = true;
            int oldY = yn;
            int oldX = xn;
            (yn, xn, valid) = GetNextShipPiece(yn, xn, field, skips);
            if (!valid) return false;
            if (xn == -1 && yn == -1)
            {
              if (!ships.ContainsKey(size)) ships[size] = 0;
              ships[size]++;
              break;
            }
            if (first_piece)
            {
              expectedDir = (Math.Abs(yn - oldY), Math.Abs(xn - oldX));
              first_piece = false;
            }
            else
            {
              (int dirY, int dirX) = expectedDir;
              if (yn != oldY + dirY || xn != oldX + dirX) return false;
            }
            size++;
          }
        }
      }

      Dictionary<int, int> shipCounts = new Dictionary<int, int>(){
        {4, 1},{3, 2},{2, 3},{1, 4},
      };
      foreach ((int ship, int count) in shipCounts)
      {
        if (!ships.ContainsKey(ship)) return false;
        if (ships[ship] != count) return false;
      }

      return true;
    }

    private static (int, int, bool) GetNextShipPiece(
      int y, int x, int[,] field, Dictionary<(int, int), bool> skips
    )
    {
      int count = 0;
      (int, int, bool) nextPiece = (-1, -1, true);
      (int, int)[] dirs = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };
      foreach ((int yi, int xi) in dirs)
      {
        int yn = y + yi;
        int xn = x + xi;
        if (yn < 0 || yn >= field.GetLength(0)) continue;
        if (xn < 0 || xn >= field.GetLength(1)) continue;
        if (skips.ContainsKey((yn, xn))) continue;
        if (field[yn, xn] == 1)
        {
          count++;
          bool valid = count <= 1;
          nextPiece = (yn, xn, valid);
          if (count > 1) return nextPiece;
        }
      }
      return nextPiece;
    }

    private static bool BordersDiagonalPiece(int y, int x, int[,] field)
    {
      foreach (int yi in new int[] { -1, 1 })
      {
        foreach (int xi in new int[] { -1, 1 })
        {
          int yn = y + yi;
          int xn = x + xi;
          if (yn < 0 || yn > field.GetLength(0)) continue;
          if (xn < 0 || xn > field.GetLength(1)) continue;
          if (field[yn, xn] == 1) return true;
        }
      }
      return false;
    }
  }
}
