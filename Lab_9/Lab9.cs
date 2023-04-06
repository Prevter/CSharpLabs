using System.Globalization;

namespace Labs.Lab9
{
	public static class Lab9
	{
		#region Task 1

		public static string FormatX(double x)
		{
			if (Math.Abs(x) > 1)
			{
				bool isPositive = x > 0;
				bool isGreaterThan10 = Math.Abs(x) > 10;

				string part1 = isPositive ? "більш за " : "менш за -";
				string part2 = isGreaterThan10 ? "10" : "1";

				return $"X = {x} {part1}{part2}";
			}

			return string.Empty;
		}

		public static double CalculateResult(double x, double y, double z)
		{
			return Math.Abs(Math.Cos(z) + Math.Cos(y)) * (1 + 2 * Math.Pow(x, 2));
		}

		#endregion

		#region Task 2

		public static double CalculateVolume(
			double firstSpeed, double firstTime, 
			double secondSpeed, double secondTime, 
			double thirdSpeed, double thirdTime
		) => firstSpeed * firstTime + secondSpeed * secondTime + thirdSpeed * thirdTime;

		#endregion
	}
}