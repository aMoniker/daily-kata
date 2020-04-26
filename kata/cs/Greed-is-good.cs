// Greed is good
// https://www.codewars.com/kata/5270d0d18625160ada0000e4/train/csharp

using System;
using System.Linq;

public static class GreedIsGoodKata
{
	public static int Score(int[] dice)
	{
		int score = 0;

		bool triple1 = hasTriple(dice, 1);
		bool triple5 = hasTriple(dice, 5);

		if (triple1) score += 1000;
		if (hasTriple(dice, 6)) score += 600;
		if (triple5) score += 500;
		if (hasTriple(dice, 4)) score += 400;
		if (hasTriple(dice, 3)) score += 300;
		if (hasTriple(dice, 2)) score += 200;

		score += (dice.Where(i => i == 1).Count() - (triple1 ? 3 : 0)) * 100;
		score += (dice.Where(i => i == 5).Count() - (triple5 ? 3 : 0)) * 50;

		return score;
	}

	private static bool hasTriple(int[] dice, int num)
	{
		return dice.Where(i => i == num).Count() >= 3;
	}
}
