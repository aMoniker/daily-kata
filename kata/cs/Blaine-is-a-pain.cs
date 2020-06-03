// https://www.codewars.com/kata/59b47ff18bcb77a4d1000076/train/csharp

// I'm not going to spend the time to refactor this right now,
// but if I were, it would have been nicer to create a class
// with non-static methods so I'm not passing around structs everywhere

using System;
using System.Linq;
using System.Collections.Generic;

using PieceDirMap = System.Collections.Generic.Dictionary<
  char, System.Collections.Generic.Dictionary<
    (int, int), char[]
  >
>;

public struct TrackPiece
{
  public (int, int) Coords { get; set; }
  public bool IsStation { get; set; }
  public bool IsCrossing { get; set; }
  public int CrossesWith { get; set; }
  public override string ToString()
  {
    (int x, int y) = Coords;
    var extra = new List<string>();
    if (IsStation) extra.Add("S");
    if (IsCrossing) extra.Add("C");
    string e = String.Join(",", extra);
    if (e != "") e = $" ({e})";
    return $"{x},{y}{e}";
  }
}

public struct Train
{
  public int Position { get; set; }
  public int Direction { get; set; }
  public int Carriages { get; set; }
  public int WaitedAtStation { get; set; }
  public bool IsExpress { get; set; }
}

public class Dinglemouse
{
  // =======================================
  // Blaine is a pain, and that is the truth
  // =======================================

  public static int TrainCrash(
    string track, string aTrain, int aPos, string bTrain, int bPos, int limit
  )
  {
    var pieces = ParseTrack(track);
    var trainA = MakeTrain(aTrain, aPos, pieces);
    var trainB = MakeTrain(bTrain, bPos, pieces);

    // DebugTrack(track);
    // DebugTrain(trainA);
    // DebugTrain(trainB);
    // DebugPieces(pieces);

    int turn = 0;
    bool crash = false;
    do
    {
      if (DidTrainsCrash(trainA, trainB, pieces))
      {
        crash = true;
        break;
      }
      trainA = StepTrain(trainA, pieces);
      trainB = StepTrain(trainB, pieces);
      turn++;
    } while (turn <= limit);

    return crash ? turn : -1;
  }

  private static Train StepTrain(Train t, List<TrackPiece> pieces)
  {
    TrackPiece p = pieces[t.Position];
    if (p.IsStation && !t.IsExpress && t.WaitedAtStation < t.Carriages)
    {
      t.WaitedAtStation++;
    }
    else
    {
      t.WaitedAtStation = 0;
      t.Position = GetNextPosition(t.Position, t.Direction, pieces);
    }
    return t;
  }

  private static bool DidTrainsCrash(Train a, Train b, List<TrackPiece> pieces)
  {
    var aPositions = GetTrainPositions(a, pieces);
    var bPositions = GetTrainPositions(b, pieces);

    // check if the two trains overlap
    if (DoTrainsOverlap(aPositions, bPositions, pieces)) return true;

    // check if a train overlaps itself
    if (DoesTrainOverlapItself(aPositions, pieces)) return true;

    // check if b train overlaps itself
    if (DoesTrainOverlapItself(bPositions, pieces)) return true;

    return false;
  }

  private static bool DoesTrainOverlapItself(int[] t, List<TrackPiece> pieces)
  {
    for (int i = 0; i < t.Length - 1; i++)
    {
      for (int j = i + 1; j < t.Length; j++)
      {
        if (t[i] == t[j]) return true;
        var iPiece = pieces[t[i]];
        if (iPiece.IsCrossing && iPiece.CrossesWith == t[j]) return true;
      }
    }
    return false;
  }

  private static bool DoTrainsOverlap(int[] a, int[] b, List<TrackPiece> pieces)
  {
    foreach (var aPos in a)
    {
      foreach (var bPos in b)
      {
        if (aPos == bPos) return true;
        var aPiece = pieces[aPos];
        if (aPiece.IsCrossing && aPiece.CrossesWith == bPos) return true;
      }
    }
    return false;
  }

  private static int GetNextPosition(int pos, int dir, List<TrackPiece> pieces)
  {
    int nextPos = (pos + dir) % pieces.Count;
    if (nextPos < 0) nextPos = pieces.Count + nextPos;
    return nextPos;
  }

  private static int[] GetTrainPositions(Train t, List<TrackPiece> pieces)
  {
    var len = t.Carriages + 1;
    var positions = new int[len];
    for (int i = 0; i < len; i++)
    {
      positions[i] = GetNextPosition(
        t.Position - (t.Direction * i), 0, pieces
      );
    }
    return positions;
  }

  private static Train MakeTrain(string t, int pos, List<TrackPiece> pieces)
  {
    var train = new Train();
    train.Position = pos;
    train.Carriages = t.Length - 1;
    train.IsExpress = t[0].ToString().ToLower() == "x";

    var enginePiece = pieces[pos];
    train.Direction = (int)t[0] <= 90 ? -1 : 1;

    if (enginePiece.IsStation) train.WaitedAtStation = train.Carriages;

    return train;
  }

  private static PieceDirMap PieceDirMap = new PieceDirMap
  {
    { '-', new Dictionary<(int, int), char[]> {
      { (1, 0), new char[] { '-', '/', '\\', '+', 'S' } },
      { (-1, 0), new char[] { '-', '/', '\\', '+', 'S' } },
    }},
    { '|', new Dictionary<(int, int), char[]> {
      { (0, -1), new char[] { '|', '/', '\\', '+', 'S' } },
      { (0, 1), new char[] { '|', '/', '\\', '+', 'S' } },
    }},
    { '/', new Dictionary<(int, int), char[]> { // always the first piece
      { (1, 0), new char[] { '-', '+', } }, // order matters here
      { (0, -1), new char[] { '|', '+' } }, // since track starts clockwise
      { (1, -1), new char[] { '/', 'X', 'S' } }, // and we check these in order
      { (0, 1), new char[] { '|', '+' } },
      { (-1, 0), new char[] { '-', '+', } },
      { (-1, 1), new char[] { '/', 'X', 'S' } },
    }},
    { '\\', new Dictionary<(int, int), char[]> {
      { (0, -1), new char[] { '|', '+' } },
      { (0, 1), new char[] { '|', '+' } },
      { (1, 0), new char[] { '-', '+' } },
      { (-1, 0), new char[] { '-', '+' } },
      { (-1, -1), new char[] { '\\', 'X', 'S' } },
      { (1, 1), new char[] { '\\', 'X', 'S' } },
    }},
    { '+', new Dictionary<(int, int), char[]> {
      { (0, -1), new char[] { '|', '/', '\\', '+', 'S' } },
      { (0, 1), new char[] { '|', '/', '\\', '+', 'S' } },
      { (1, 0), new char[] { '-', '/', '\\', '+', 'S' } },
      { (-1, 0), new char[] { '-', '/', '\\', '+', 'S' } }
    }},
    { 'X', new Dictionary<(int, int), char[]> {
      { (-1, -1), new char[] { '\\', 'X', 'S' } },
      { (1, -1), new char[] { '/', 'X', 'S' } },
      { (1, 1), new char[] { '\\', 'X', 'S' } },
      { (-1, 1), new char[] { '/', 'X', 'S' } },
    }},
    { 'S', new Dictionary<(int, int), char[]> {
      { (0, -1), new char[] { '|', '+' } },
      { (0, 1), new char[] { '|', '+' } },
      { (1, 0), new char[] { '-', '+' } },
      { (-1, 0), new char[] { '-', '+' } },
      { (-1, -1), new char[] { '\\', 'X' } },
      { (1, -1), new char[] { '/', 'X' } },
      { (1, 1), new char[] { '\\', 'X' } },
      { (-1, 1), new char[] { '/', 'X' } },
    }}
  };

  private static char[] Crossings = new char[] { '+', 'X' };

  private static bool WithinBounds(int x, int y, string[] t)
  {
    return (y >= 0 && x >= 0 && y < t.Length && x < t[y].Length);
  }

  private static void DebugTrack(string track)
  {
    var t = track.Split("\n");
    int longest = 0;
    foreach (string l in t)
    {
      if (l.Length > longest) longest = l.Length;
    }
    Console.Write("   ");
    for (int i = 0; i < longest; i++)
    {
      Console.Write(i.ToString().PadLeft(2, '0')[0]);
    }
    Console.Write("\n");
    Console.Write("   ");
    for (int i = 0; i < longest; i++)
    {
      Console.Write(i.ToString().PadLeft(2, '0')[1]);
    }
    Console.Write("\n");
    for (int i = 0; i < t.Length; i++)
    {
      Console.WriteLine($"{i.ToString().PadLeft(2, '0')} {t[i]}");
    }
  }

  private static void DebugPieces(List<TrackPiece> pieces)
  {
    Console.WriteLine($"pieces parsed: {pieces.Count}");
    Console.WriteLine("pieces:");
    int count = 0;
    foreach (var p in pieces)
    {
      if (count % 10 == 0) Console.Write("\n");
      string crosses = "";
      if (p.IsCrossing)
      {
        var crossPiece = pieces[p.CrossesWith];
        crosses = $"[c:{p.CrossesWith}:{crossPiece.Coords}]";
      }
      Console.Write($"{p}{crosses}, ");
      count++;
    }
    Console.Write("\n");
  }

  private static void DebugTrain(Train t)
  {
    string express = t.IsExpress ? $" (X)" : "";
    Console.WriteLine($"train at {t.Position} length {t.Carriages + 1} facing {t.Direction}{express}");
  }


  private static List<TrackPiece> ParseTrack(string track)
  {
    var pieces = new List<TrackPiece>();
    var t = track.Split("\n");

    // iterate through the array until you find the top-left zero-piece
    (int, int) zeroPos = (-1, -1);
    for (int y = 0; y < t.Length; y++)
    {
      for (int x = 0; x < t[y].Length; x++)
      {
        if (t[y][x] == ' ') continue;
        zeroPos = (x, y);
        break;
      }
      if (zeroPos != (-1, -1)) break;
    }

    // starting from the zeroPos, iterate the track
    (int, int) prevPos = zeroPos;
    (int, int) curPos = zeroPos;
    do
    {
      (int x, int y) = curPos;
      char sym = t[y][x];

      // add the piece to the track list
      var piece = new TrackPiece();
      piece.Coords = curPos;

      // set the crossing status of the piece
      if (Crossings.Contains(sym)) piece.IsCrossing = true;

      // check if the piece is a Station
      if (sym == 'S')
      {
        piece.IsStation = true;

        // if it's a Station, check if it is also a crossing
        var cDirs = new Dictionary<(int, int), char>[] {
          new Dictionary<(int, int), char> { // straight
            {(1, 0), '-'}, {(-1, 0), '-'}, {(0, 1), '|'}, {(0, -1), '|'}
          },
          new Dictionary<(int, int), char> { // diagonal
            {(1, 1), '\\'}, {(-1, 1), '/'}, {(1, -1), '/'}, {(-1, -1), '\\'}
          },
        };
        foreach (Dictionary<(int, int), char> cDir in cDirs)
        {
          int count = 0;
          foreach (KeyValuePair<(int, int), char> item in cDir)
          {
            (int sx, int sy) = item.Key;
            int sdx = x + sx;
            int sdy = y + sy;
            if (WithinBounds(sdx, sdy, t) && t[sdy][sdx] == item.Value) count++;
          }
          if (count == 4)
          {
            piece.IsCrossing = true;
            break;
          }
        }
      }

      // if it's a crossing, find the piece it crosses (if yet parsed)
      if (piece.IsCrossing)
      {
        for (int i = 0; i < pieces.Count; i++)
        {
          var cPiece = pieces[i];
          if (cPiece.IsCrossing && cPiece.Coords == piece.Coords)
          {
            piece.CrossesWith = i;
            cPiece.CrossesWith = pieces.Count;
            pieces[i] = cPiece;
          }
        }
      }

      // add the piece to the list
      pieces.Add(piece);

      // find the position of the next valid piece (ignore prevPos)
      foreach (KeyValuePair<(int, int), char[]> p in PieceDirMap[sym])
      {
        (int dx, int dy) = p.Key;
        int nx = x + dx;
        int ny = y + dy;
        if (!WithinBounds(nx, ny, t) || prevPos == (nx, ny)) continue;
        if (piece.IsCrossing)
        {
          (int px, int py) = prevPos;
          var validCrossDir = (x - px, y - py);
          if (p.Key != validCrossDir) continue;
        }

        bool found = false;
        foreach (char c in p.Value)
        {
          if (t[ny][nx] != c) continue;
          prevPos = curPos;
          curPos = (nx, ny);
          found = true;
        }
        if (found) break;
      }
    } while (curPos != zeroPos);

    return pieces;
  }
}
