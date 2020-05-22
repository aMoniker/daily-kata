// https://www.codewars.com/kata/57658bfa28ed87ecfa00058a/train/csharp

using System;
using System.Linq;
using System.Collections.Generic;

public class ShortestPathFinder2
{
  public static int PathFinder(string rawMaze)
  {
    if (rawMaze.Length == 1) return 0;
    char[][] maze = ParseMaze(rawMaze);
    int[,] costs = new int[maze.Length, maze.Length];
    (int, int)[] dirs = new (int, int)[] {
      (1, 0), (0, 1), (-1, 0), (0, -1)
    };
    Dictionary<(int, int), bool> processed = new Dictionary<(int, int), bool>();
    Queue<(int, int)> queue = new Queue<(int, int)>();
    queue.Enqueue((0, 0));

    while (queue.Count > 0)
    {
      (int x, int y) = queue.Dequeue();
      if (processed.ContainsKey((y, x))) continue;
      foreach ((int dx, int dy) in dirs)
      {
        int xn = x + dx;
        int yn = y + dy;
        if (xn == 0 && yn == 0) continue;
        if (yn < 0 || yn >= maze.Length) continue;
        if (xn < 0 || xn >= maze.Length) continue;
        if (maze[yn][xn] == 'W') continue;
        int curCost = costs[yn, xn];
        int newCost = costs[y, x] + 1;
        costs[yn, xn] = curCost == 0 ? newCost : Math.Min(curCost, newCost);
        queue.Enqueue((xn, yn));
        processed[(y, x)] = true;
      }
    }

    int shortest = costs[maze.Length - 1, maze.Length - 1];
    return shortest == 0 ? -1 : shortest;
  }

  private static char[][] ParseMaze(string maze)
  {
    return maze.Split("\n").Select(x => x.ToCharArray()).ToArray();
  }
}
