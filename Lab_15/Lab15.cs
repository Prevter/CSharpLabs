namespace Labs.Lab15
{
	public static class Utils
	{
		public static double GetLeg2(double leg, double angle)
		{
			angle = angle * Math.PI / 180;
			return leg * Math.Tan(angle);
		}

		public static double GetHypotenuse(double leg1, double leg2)
		{
			return Math.Sqrt(leg1 * leg1 + leg2 * leg2);
		}

		public static double GetArea(double leg1, double leg2)
		{
			return leg1 * leg2 / 2;
		}

		public static double GetPerimeter(double leg1, double leg2, double hypotenuse)
		{
			return leg1 + leg2 + hypotenuse;
		}

		public static string ReverseText(string text)
		{
			char[] arr = text.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}
	}
}