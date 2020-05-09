// https://www.codewars.com/kata/58d06bfbc43d20767e000074/train/csharp

using System;
using System.Collections.Generic;
using Map = System.Collections.Generic.Dictionary<string, double>;

namespace CodeWars
{
  class BattleShipsSunkDamagedOrNotTouchedKata
  {
    public static Map damagedOrSunk(int[,] board, int[,] attacks)
    {
      Map ret = new Map() {
        {"sunk", 0}, {"damaged", 0}, {"notTouched", 0}, {"points", 0}
      };

      int yLen = board.GetLength(0);
      int xLen = board.GetLength(1);

      Dictionary<int, int> boatMaxHP = new Dictionary<int, int>();
      for (int y = 0; y < yLen; y++)
      {
        for (int x = 0; x < xLen; x++)
        {
          int boat = board[y, x];
          if (boat == 0) continue;
          if (!boatMaxHP.ContainsKey(boat)) boatMaxHP[boat] = 0;
          boatMaxHP[boat]++;
        }
      }
      Dictionary<int, int> boatHP = new Dictionary<int, int>(boatMaxHP);

      for (int i = 0; i < attacks.GetLength(0); i++)
      {
        int x = attacks[i, 0] - 1;
        int y = yLen - attacks[i, 1]; // y is reversed, x is not

        int boatHit = board[y, x];
        if (boatHit == 0) continue;

        boatHP[boatHit]--;
        if (boatHP[boatHit] == boatMaxHP[boatHit] - 1)
        {
          ret["points"] += 0.5;
          ret["damaged"]++;
        }
        if (boatHP[boatHit] == 0)
        {
          ret["points"] += 0.5;
          ret["sunk"]++;
          ret["damaged"]--;
        }
      }

      foreach (KeyValuePair<int, int> boat in boatHP)
      {
        if (boat.Value == boatMaxHP[boat.Key])
        {
          ret["points"]--;
          ret["notTouched"]++;
        }
      }

      return ret;
    }
  }
}
