// https://www.codewars.com/kata/526156943dfe7ce06200063e/train/csharp

using System;

public static class Kata
{
  public static string BrainLuck(string code, string input)
  {
    int memSize = 30000;
    byte[] memory = new byte[memSize];
    int cell = 0; // active memory cell
    int inputPointer = 0;
    string output = "";

    for (int ip = 0; ip < code.Length; ip++)
    {
      switch (code[ip])
      {
        case '>':
          cell++;
          break;
        case '<':
          cell--;
          break;
        case '+':
          memory[cell]++;
          break;
        case '-':
          memory[cell]--;
          break;
        case '.':
          output += Convert.ToChar(memory[cell]);
          break;
        case ',':
          memory[cell] = Convert.ToByte(input[inputPointer++]);
          break;
        case '[':
          if (memory[cell] != 0) break;
          int f = 0;
          for (int i = ip; i < code.Length; i++)
          {
            if (code[i] == '[') f++;
            if (code[i] == ']') f--;
            if (f == 0)
            {
              ip = i;
              break;
            }
          }
          break;
        case ']':
          if (memory[cell] == 0) break;
          int b = 0;
          for (int i = ip; i >= 0; i--)
          {
            if (code[i] == ']') b++;
            if (code[i] == '[') b--;
            if (b == 0)
            {
              ip = i;
              break;
            }
          }
          break;
      }
    }

    return output;
  }
}
