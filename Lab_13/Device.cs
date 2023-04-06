namespace Labs.Lab13
{
	public abstract class Device
	{
		public double Wattage { get; set; }
		public double IdleWattage { get; set; }
		public bool Active { get; set; }

		public Device(double wattage, double idleWattage = 0.0)
		{
			Wattage = wattage;
			IdleWattage = idleWattage;
		}

		public abstract void Activate();
		public abstract void Deactivate();

		public override bool Equals(object? obj)
		{
			if (obj is Device device)
				return Wattage == device.Wattage;
			return false;
		}

		public override int GetHashCode()
		{
			return Wattage.GetHashCode();
		}
	}
}
