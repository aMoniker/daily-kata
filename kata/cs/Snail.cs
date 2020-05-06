// https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1/train/csharp

using System;
using System.Collections.Generic;

public class SnailSolution
{
  public static int[] Snail(int[][] array)
  {
    int count = array.Length * array[0].Length;
    int[] ret = new int[count];
    int[] dir = { 1, 0 };
    int[] cell = { 0, 0 };

    Dictionary<string, int> limits = new Dictionary<string, int>();
    limits["up"] = 0;
    limits["down"] = array.Length - 1;
    limits["left"] = 0;
    limits["right"] = array[0].Length - 1;

    for (int i = 0; i < count; i++)
    {
      ret[i] = array[cell[1]][cell[0]];
      int[] nextCell = GetNextCell(cell, dir);
      if (nextCell[0] > limits["right"])
      {
        dir = new int[] { 0, 1 };
        limits["up"]++;
      }
      else if (nextCell[0] < limits["left"])
      {
        dir = new int[] { 0, -1 };
        limits["down"]--;
      }
      else if (nextCell[1] > limits["down"])
      {
        dir = new int[] { -1, 0 };
        limits["right"]--;
      }
      else if (nextCell[1] < limits["up"])
      {
        dir = new int[] { 1, 0 };
        limits["left"]++;
      }
      cell = GetNextCell(cell, dir);
    }

    return ret;
  }

  private static int[] GetNextCell(int[] cell, int[] dir)
  {
    int[] nextCell = { cell[0] + dir[0], cell[1] + dir[1] };
    return nextCell;
  }
}
