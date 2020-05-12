using System;
using System.Collections.Generic;

public class TicTacToe3D
{
  private static int size = 4;

  public static string PlayOX3D(List<(int, int, int)> moves)
  {
    bool playerO = true;
    int moveCount = 0;
    int[,,] board = new int[size, size, size];
    foreach ((int x, int y, int z) in moves)
    {
      moveCount++;
      board[x, y, z] = playerO ? 1 : 2;
      if (DoesMoveWin(x, y, z, board))
      {
        string player = playerO ? "O" : "X";
        return $"{player} wins after {moveCount} moves";
      }
      playerO = !playerO;
    }
    return "No winner";
  }

  private static bool DoesMoveWin(int x, int y, int z, int[,,] board)
  {
    int symbol = board[x, y, z];
    int[] dirs = new int[] { -1, 0, 1 };
    int[] signs = new int[] { -1, 1 };
    Dictionary<(int, int, int), bool> isChecked =
      new Dictionary<(int, int, int), bool>();

    foreach (int xi in dirs)
    {
      foreach (int yi in dirs)
      {
        foreach (int zi in dirs)
        {
          if ((xi, yi, zi) == (0, 0, 0)) continue;
          if (isChecked.ContainsKey((xi, yi, zi))) continue;
          isChecked[(xi, yi, zi)] = true;
          isChecked[(-xi, -yi, -zi)] = true;
          int sequence = 1;
          for (int i = 1; i <= size; i++)
          {
            foreach (int n in signs)
            {
              (int xx, int yy, int zz) = (xi * i * n, yi * i * n, zi * i * n);
              (int xinc, int yinc, int zinc) = (x + xx, y + yy, z + zz);
              if (
                 xinc < size && xinc >= 0
              && yinc < size && yinc >= 0
              && zinc < size && zinc >= 0
              && board[xinc, yinc, zinc] == symbol)
              {
                sequence++;
                if (sequence == size) return true;
              }
            }
          }
        }
      }
    }
    return false;
  }

  private static void DebugBoard(int[,,] board)
  {
    for (int z = size - 1; z >= 0; z--)
    {
      for (int y = size - 1; y >= 0; y--)
      {
        for (int x = 0; x < size; x++)
        {
          string sym = board[x, y, z] == 0 ? " ." : board[x, y, z] == 1 ? " O" : " X";
          Console.Write(sym);
        }
        Console.WriteLine("");
      }
      Console.WriteLine(" -------");
    }
  }
}
