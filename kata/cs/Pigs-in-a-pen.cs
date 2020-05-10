// https://www.codewars.com/kata/58fdcc51b4f81a0b1e00003e/train/csharp

using System;
using System.Collections.Generic;

namespace CodeWars
{
  public class Game
  {
    private int n; // board size
    private bool[] lines; // which lines are filled
    private bool[] boxes; // which boxes are filled (optimization)

    public Game(int board)
    {
      n = board;
      int n2 = Convert.ToInt32(Math.Pow(n, 2));
      boxes = new bool[n2];
      lines = new bool[2 * n2 + 2 * n];
    }

    /**
     * Plays all possible moves (cascading) for a player's turn.
     */
    public List<int> play(List<int> lineList)
    {
      if (lineList.Count < 3) return turnEnded(lineList);
      foreach (int line in lineList) lines[line - 1] = true;
      bool playingMove = true;
      while (playingMove) playingMove = playMove(lines, lineList);
      return turnEnded(lineList);
    }

    /**
     * Plays a single line if it's possible to play one.
     * Returns true if a line was played, false otherwise.
     */
    public bool playMove(bool[] lines, List<int> lineList)
    {
      for (int i = 0; i < Math.Pow(n, 2); i++)
      {
        if (boxes[i]) continue; // optimization, don't double-check boxes
        int x = i + Convert.ToInt32(Math.Floor((double)(i / n))) * (n + 1);
        int[] checkLines = new int[4] { x, x + n, x + n + 1, x + n + 1 + n };

        int count = 0;
        foreach (int line in checkLines) if (lines[line]) count++;
        if (count == 4) boxes[i] = true;
        if (count != 3) continue;
        foreach (int line in checkLines)
        {
          if (lines[line]) continue;
          lines[line] = true;
          lineList.Add(line + 1);
          boxes[i] = true;
          break;
        }
        return true;
      }
      return false;
    }

    private List<int> turnEnded(List<int> lineList)
    {
      lineList.Sort();
      return lineList;
    }
  }
}
