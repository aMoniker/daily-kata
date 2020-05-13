// https://www.codewars.com/kata/56882731514ec3ec3d000009/train/csharp

using System;
using System.Collections.Generic;

public class ConnectFour
{
  private static int winSize = 4;
  private static int boardCols = 7;
  private static int boardRows = 6;

  public static string WhoIsWinner(List<string> moves)
  {
    if (moves.Count < (winSize * 2) - 1) return "Draw";

    int[,] board = new int[boardCols, boardRows];
    string[] players = new string[2] {
      moves[0].Split("_")[1], moves[1].Split("_")[1]
    };

    for (int i = 0; i < moves.Count; i++)
    {
      int player = i % 2;
      int symbol = player + 1;
      int col = ParseMove(moves[i]);
      (int x, int y) = PlayMove(col, symbol, board);
      if (CheckWin(x, y, board)) return players[player];
    }

    return "Draw";
  }

  private static (int, int) PlayMove(int col, int symbol, int[,] board)
  {
    for (int row = boardRows - 1; row >= 0; row--)
    {
      if (board[col, row] != 0) continue;
      board[col, row] = symbol;
      return (col, row);
    }
    return (0, 0);
  }

  private static bool CheckWin(int col, int row, int[,] board)
  {
    if (board[col, row] == 0) return false;
    (int, int)[] dirs = new (int, int)[] { (1, 0), (0, 1), (1, 1), (1, -1) };
    foreach ((int x, int y) in dirs)
    {
      if (CheckWinFromCell(col, row, x, y, board)) return true;
    }
    return false;
  }

  private static bool CheckWinFromCell(
    int col, int row, int xDir, int yDir, int[,] board
  )
  {
    int symbol = board[col, row];
    int count = 1;
    int[] signs = new int[] { 1, -1 };
    foreach (int sign in signs)
    {
      bool sequenceBroken = false;
      for (int i = 1; i < winSize; i++)
      {
        int x = col + xDir * i * sign;
        int y = row + yDir * i * sign;
        if (x < 0 || x >= board.GetLength(0)) sequenceBroken = true;
        if (y < 0 || y >= board.GetLength(1)) sequenceBroken = true;
        if (!sequenceBroken && board[x, y] != symbol) sequenceBroken = true;
        if (sequenceBroken) break;
        count++;
      }
    }
    return count >= winSize;
  }

  private static int ParseMove(string move)
  {
    return Convert.ToInt32(move[0]) - 65; // capital A is code point 65
  }

  private static void DebugBoard(int[,] board)
  {
    Console.WriteLine("");
    for (int x = 0; x < board.GetLength(1); x++)
    {
      for (int y = 0; y < board.GetLength(0); y++)
      {
        Console.Write(board[y, x]);
      }
      Console.WriteLine("");
    }
    Console.WriteLine("");
  }
}
