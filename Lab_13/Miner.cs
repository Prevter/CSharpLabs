using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Lab13
{
	public sealed class Miner : Device
	{
		public double HashRateMin { get; set; } = 0.00000001;
		public double HashRateMax { get; set; } = 0.0000001;
		public double Balance { get; set; } = 0;
		public CancellationTokenSource? TokenSource { get; set; }

		public event EventHandler? OnMined;

		public Miner() : base(3250.0, 300.0)
		{
		}

		public override void Activate()
		{
			if (Active) return;
			Active = true;
			TokenSource = new CancellationTokenSource();

			Task.Run(() =>
			{
				while (Active)
				{
					Thread.Sleep(1000);
					var rand = new Random();
					var hashRate = rand.NextDouble() * (HashRateMax - HashRateMin) + HashRateMin;
					Balance += hashRate;
					OnMined?.Invoke(this, new EventArgs());
				}
			}, TokenSource.Token);
		}
		
		public override void Deactivate()
		{
			TokenSource?.Cancel();
			Active = false;
		}
	}
}
