namespace Labs.Lab13
{
	public sealed class Microwave : Device
	{
		public int TimeRemaining { get; set; } = 60;

		public event EventHandler? Cooked;
		public event EventHandler<TickEventArgs>? OnSecond;

		public Microwave() : base(1000.0, 5.0)
		{
		}

		public override void Activate()
		{
			if (Active) return;

			Active = true;

			Task.Run(() =>
			{
				int timer = TimeRemaining;
				while (timer > 0 && Active)
				{
					OnSecond?.Invoke(this, new TickEventArgs(timer));
					Thread.Sleep(1000);
					timer--;
				}
				if (!Active) return;
				Cooked?.Invoke(this, new EventArgs());
				Active = false;
			});
		}

		public override void Deactivate()
		{
			if (!Active) return;
			Active = false;
			Cooked?.Invoke(this, new EventArgs());
		}
	}

	public sealed class TickEventArgs
	{
		public int Remaining { get; set; }
		public TickEventArgs(int remaining)
		{
			Remaining = remaining;
		}
	}
}
