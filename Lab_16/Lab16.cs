namespace Labs.Lab16
{
	public static class Delegates
	{
		public delegate double Function(double x);

		public static readonly Function FirstIntegral = (x) => 3 * Math.Pow(x * x, 1.0 / 3.0) / 2;
		public static readonly Function SecondIntegral = Si;
		public static readonly Function ThirdIntegral = (x) => x * Math.Sin(x) + Math.Cos(x);

		public static double DefinedIntegral(Function function, double a, double b) => function(b) - function(a);

		public static double Si(double x)
		{
			double si = 0;
			double term;
			int n = 1;
			do
			{
				term = Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 1) / (Factorial(2 * n) * (2 * n - 1));
				si += term;
				n++;
			} while (Math.Abs(term) > 1E-15);

			return si;
		}

		private static double Factorial(int n)
		{
			if (n <= 1) return 1;
			else return n * Factorial(n - 1);
		}
	}
}