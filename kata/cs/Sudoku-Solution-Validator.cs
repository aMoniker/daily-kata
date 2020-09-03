// https://www.codewars.com/kata/529bf0e9bdf7657179000008/train/csharp

using System;
using System.Linq;

public class SudokuSolutionValidatorKata
{
  public static bool ValidateSolution(int[][] board)
  {
    if (!ValidateBoard(board)) return false;
    for (int i = 0; i < 9; i++)
    {
      if (!ValidateRow(board, i)) return false;
      if (!ValidateCol(board, i)) return false;
    }
    for (int y = 1; y <= 7; y += 3)
    {
      for (int x = 1; x <= 7; x += 3)
      {
        if (!ValidateBlock(board, (y, x))) return false;
      }
    }
    return true;
  }

  private static bool ValidateBoard(int[][] board)
  {
    if (board.Length != 9) return false;
    foreach (int[] row in board)
    {
      if (row.Length != 9) return false;
    }
    return true;
  }

  private static bool ValidateRow(int[][] board, int row)
  {
    return ValidateSequence(board[row]);
  }

  private static bool ValidateCol(int[][] board, int col)
  {
    return ValidateSequence(
      Enumerable.Range(0, 9).Select(x => board[x][col]).ToArray()
    );
  }

  private static bool ValidateBlock(int[][] board, (int, int) block)
  {
    int[] seq = new int[9];
    (int y, int x) = block;
    int i = 0;
    for (int yd = -1; yd <= 1; yd++)
    {
      for (int xd = -1; xd <= 1; xd++)
      {
        seq[i] = board[y + yd][x + xd];
        i++;
      }
    }
    return ValidateSequence(seq);
  }

  private static int ValidSequenceMask = 0b111111111;
  private static bool ValidateSequence(int[] seq)
  {
    int mask = 0;
    foreach (int n in seq) mask |= 1 << (n - 1);
    return mask == ValidSequenceMask;
  }
}
