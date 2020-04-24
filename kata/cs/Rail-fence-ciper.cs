// Rail Fence Cipher
// https://www.codewars.com/kata/58c5577d61aefcf3ff000081/solutions/csharp

using System;
public class RailFenceCipher
{
	public static string Encode(string s, int n)
	{
		string[] rails = new string[n];
		int dir = 1;
		int rail = 0;
		for (int i = 0; i < s.Length; i++)
		{
			rails[rail] += s[i].ToString();
			dir = (rail == 0) ? 1 : (rail == n - 1) ? -1 : dir;
			rail += dir;
		}
		return String.Join("", rails);
	}

	public static string Decode(string s, int r)
	{
		string[] ret = new string[s.Length];

		int x = 0;
		int p = x;
		bool flip = false;

		foreach (char c in s)
		{
			if (p >= s.Length)
			{
				x++;
				p = x;
				flip = false;
			}
			if (x == r) break;

			ret[p] = c.ToString();

			int x1 = r - x;
			if (x1 == 1) x1 = r;
			int x2 = (r + 1) - x1;

			int bx = flip ? x2 : x1;
			int b = 2 * (bx - 2) + 2;

			p += b;
			if (x1 != r) flip = !flip;
		}

		return String.Join("", ret);
	}
}
