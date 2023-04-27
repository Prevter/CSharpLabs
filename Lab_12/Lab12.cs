namespace Labs.Lab12
{
	public class Puck
	{
		private double a;

		public double A { get => a; set => a = value; }

		public double Area => 3 * Math.Sqrt(3) * Math.Pow(A, 2) / 2;

		public Puck()
		{
			a = 0;
		}

		public Puck(double a)
		{
			this.a = a;
		}

		public override string ToString() => $"Puck{{ A = {A}; Area = {Area} }}";

		public static Puck operator +(Puck left, Puck right)
		{
			return new Puck(left.a + right.a);
		}

		public static Puck operator +(Puck _, double area)
		{
			double side = Math.Sqrt(area * 2 / 3 / Math.Sqrt(3));
			return new Puck(side);
		}
	}
}