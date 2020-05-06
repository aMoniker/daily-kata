// https://www.codewars.com/kata/52423db9add6f6fc39000354/train/csharp

using System;
using System.Linq;
using System.Collections.Generic;
using Map = System.Collections.Generic.Dictionary<string, bool>;

public class ConwayLife
{
  public static int[,] GetGeneration(int[,] cells, int generation)
  {
    Map dict = ConvertCellsToDict(cells);
    for (int i = 0; i < generation; i++)
    {
      dict = GetNextGeneration(dict);
    }
    return ConvertDictToCells(dict);
  }

  private static Map GetNextGeneration(Map dict)
  {
    Map nextDict = new Map();

    Map keys = GetKeysToCheck(dict);
    foreach (KeyValuePair<string, bool> entry in keys)
    {
      int[] coords = DecodeCoords(entry.Key);
      int neighbors = CountNeighbors(dict, coords[0], coords[1]);
      switch (neighbors)
      {
        case int n when (n > 3 || n < 2): // dead no matter what
          break; // simply has no entry in nextDict
        case int n when (n == 2 || n == 3):
          if (entry.Value)
          {
            nextDict[entry.Key] = true; // lives on if alive
          }
          else if (n == 3)
          {
            nextDict[entry.Key] = true; // comes to life if dead
          }
          break;
      }
    }

    return nextDict;
  }

  private static Map GetKeysToCheck(Map dict)
  {
    Map keys = new Map();
    foreach (KeyValuePair<string, bool> entry in dict)
    {
      int[] coords = DecodeCoords(entry.Key);
      for (int x = -1; x <= 1; x++)
      {
        for (int y = -1; y <= 1; y++)
        {
          string key = EncodeCoords(coords[0] + x, coords[1] + y);
          keys[key] = dict.ContainsKey(key);
        }
      }
    }
    return keys;
  }

  private static int CountNeighbors(Map dict, int x, int y)
  {
    int neighbors = 0;
    for (int xi = -1; xi <= 1; xi++)
    {
      for (int yi = -1; yi <= 1; yi++)
      {
        if (xi == 0 && yi == 0) continue;
        if (dict.ContainsKey(EncodeCoords(x + xi, y + yi))) neighbors++;
        if (neighbors > 3) return neighbors; // optimization
      }
    }
    return neighbors;
  }

  private static Map ConvertCellsToDict(int[,] cells)
  {
    Map dict = new Map();
    for (int x = 0; x < cells.GetLength(0); x++)
    {
      for (int y = 0; y < cells.GetLength(1); y++)
      {
        if (cells[x, y] == 0) continue;
        dict[EncodeCoords(x, y)] = cells[x, y] == 1;
      }
    }
    return dict;
  }

  private static int[,] ConvertDictToCells(Map dict)
  {
    int xMax = int.MinValue;
    int xMin = int.MaxValue;
    int yMax = int.MinValue;
    int yMin = int.MaxValue;

    bool hasLiveCells = false;

    foreach (KeyValuePair<string, bool> entry in dict)
    {
      if (entry.Value) hasLiveCells = true;
      int[] coords = DecodeCoords(entry.Key);
      if (coords[0] > xMax) xMax = coords[0];
      if (coords[0] < xMin) xMin = coords[0];
      if (coords[1] > yMax) yMax = coords[1];
      if (coords[1] < yMin) yMin = coords[1];
    }

    if (!hasLiveCells) return new int[0, 0];

    int xDim = xMax - xMin + 1;
    int yDim = yMax - yMin + 1;
    int xShift = xMin != 0 ? -xMin : 0;
    int yShift = yMin != 0 ? -yMin : 0;

    int[,] cells = new int[xDim, yDim];
    foreach (KeyValuePair<string, bool> entry in dict)
    {
      int[] coords = DecodeCoords(entry.Key);
      cells[coords[0] + xShift, coords[1] + yShift] = 1;
    }

    return cells;
  }

  private static int[] DecodeCoords(string coords)
  {
    return coords.Split(':').Select(i => Convert.ToInt32(i)).ToArray();
  }

  private static string EncodeCoords(int x, int y)
  {
    return $"{x}:{y}";
  }
}
