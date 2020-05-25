// https://www.codewars.com/kata/57bc5e0471f2ff9233000005/train/csharp

using System;
using System.Collections.Generic;

public class TTTSolver
{
  private static int size = 3;

  public static int[] TurnMethod(int[][] board, int player)
  {
    int opponent = player == 1 ? 2 : 1;
    (int x, int y) = FindMostImportantMove(board, player);
    return new int[] { x, y };
  }

  private static (int, int) FindMostImportantMove(int[][] board, int player)
  {
    int opponent = player == 1 ? 2 : 1;

    List<(int, int)> playerWins = FindWinningMoves(board, player);
    if (playerWins.Count > 0) return playerWins[0];
    List<(int, int)> opponentWins = FindWinningMoves(board, opponent);
    if (opponentWins.Count > 0) return opponentWins[0];

    List<(int, int)> opponentMoves = FindAllMoves(board, opponent);
    if (opponentMoves.Count == 0) return (1, 1);

    List<(int, int)> potentialMoves = FindAllMoves(board, 0);

    if (potentialMoves.Count == 8 && opponentMoves.Count == 1)
    {
      (int x, int y) = opponentMoves[0];
      if (x == 1 && y == 1) return (0, 0);
      return (1, 1);
    }

    if (potentialMoves.Count == 6 && opponentMoves.Count == 2)
    {
      if (
         (opponentMoves.Contains((0, 0)) && opponentMoves.Contains((2, 2)))
      || (opponentMoves.Contains((0, 2)) && opponentMoves.Contains((2, 0)))
      )
      {
        if (potentialMoves.Contains((1, 1))) return (1, 1);
        if (potentialMoves.Contains((0, 1))) return (0, 1);
        if (potentialMoves.Contains((2, 1))) return (2, 1);
        if (potentialMoves.Contains((1, 0))) return (1, 0);
      }
    }

    (int, int)[] dirs = new (int, int)[] { (1, 0), (1, 1), (0, 1), (-1, 1) };
    int[][] moves = new int[size][];
    for (int x = 0; x < moves.Length; x++) moves[x] = new int[size];

    foreach ((int x, int y) in potentialMoves)
    {
      foreach ((int xi, int yi) in dirs)
      {
        int count = 1;
        int opponentFills = 0;
        int potentialFills = 0;
        for (int k = 1; k < 3; k++)
        {
          foreach (int i in new int[] { -1, 1 })
          {
            int px = x + (k * xi * i);
            int py = y + (k * yi * i);
            if (px >= 0 && px < size && py >= 0 && py < size)
            {
              count++;
              if (board[px][py] == opponent) opponentFills++;
              if (board[px][py] == 0) potentialFills++;
            }
          }
        }
        if (count == 3)
        {
          if (moves[x][y] == 0) moves[x][y] = 1;
          if (potentialFills + opponentFills == 2) moves[x][y] += opponentFills;
        }
      }
    }

    int priority = 0;
    (int, int) move = (1, 1);
    for (int x = 0; x < size; x++)
    {
      for (int y = 0; y < size; y++)
      {
        if (moves[x][y] > priority)
        {
          priority = moves[x][y];
          move = (x, y);
        }
      }
    }
    return move;
  }

  private static List<(int, int)> FindAllMoves(int[][] board, int move)
  {
    List<(int, int)> potential = new List<(int, int)>();
    for (int x = 0; x < size; x++)
    {
      for (int y = 0; y < size; y++)
      {
        if (board[x][y] == move) potential.Add((x, y));
      }
    }
    return potential;
  }

  private static List<(int, int)> FindWinningMoves(int[][] board, int player)
  {
    (int, int)[] dirs = new (int, int)[] { (1, 0), (1, 1), (0, 1), (-1, 1) };
    List<(int, int)> winningMoves = new List<(int, int)>();
    List<(int, int)> potentialMoves = FindAllMoves(board, 0);
    foreach ((int x, int y) in potentialMoves)
    {
      foreach ((int xi, int yi) in dirs)
      {
        int count = 1;
        int fills = 0;
        int potentialFills = 0;
        for (int k = 1; k < 3; k++)
        {
          foreach (int i in new int[] { -1, 1 })
          {
            int px = x + (k * xi * i);
            int py = y + (k * yi * i);
            if (px >= 0 && px < size && py >= 0 && py < size)
            {
              count++;
              if (board[px][py] == player) fills++;
              if (board[px][py] == 0) potentialFills++;
            }
          }
        }
        if (count == 3 && fills == 2) winningMoves.Add((x, y));
      }
    }
    return winningMoves;
  }
}
