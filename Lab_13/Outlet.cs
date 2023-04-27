namespace Labs.Lab13
{
	public class Outlet
	{
		public List<Device> Devices { get; } = new List<Device>();

		public void Plug(Device device)
		{
			if (Devices.Contains(device)) return;
			Devices.Add(device);
		}

		public void Unplug(Device device)
		{
			if (Devices.Contains(device))
			{
				if (device.Active) device.Deactivate();
				Devices.Remove(device);
			}
		}

		public bool IsPluggedIn(Device device)
		{
			return Devices.Contains(device);
		}

		public void ActivateAll()
		{
			foreach (Device device in Devices)
				device.Activate();
		}

		public void DeactivateAll()
		{
			foreach (Device device in Devices)
				device.Deactivate();
		}

		public double TotalWattage
		{
			get
			{
				double total = 0.0;
				foreach (Device device in Devices)
					total += device.Active ? device.Wattage : device.IdleWattage;
				return total;
			}
		}
	}
}