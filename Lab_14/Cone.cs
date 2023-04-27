namespace Labs.Lab14
{
	public sealed class Cone : IFigure
	{
		public double Radius { get; set; }
		public double Height { get; set; }

		public double Volume
		{
			get => Math.PI * Radius * Radius * Height / 3;
		}

		public double Area
		{
			get => Math.PI * Radius * (Radius + Math.Sqrt(Height * Height + Radius * Radius));
		}

		public Cone()
		{
			Radius = 0;
			Height = 0;
		}

		public Cone(double radius, double height)
		{
			Radius = radius;
			Height = height;
		}

		public string ShortName => $"Cone {{ Radius = {Radius}, Height = {Height}, Volume = {Volume}, Area = {Area} }}";
	}
}
