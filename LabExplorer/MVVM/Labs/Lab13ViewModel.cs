using LabExplorer.Common;
using Labs.Lab13;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab13ViewModel : INotifyPropertyChanged
	{
		private RelayCommand toggleMicrowaveCommand, toggleMinerCommand;
		private string microwaveTimer;
		private SolidColorBrush microwaveLights = Brushes.SaddleBrown;
		private SolidColorBrush phoneLights = Brushes.Gray;

		public Outlet Outlet { get; set; }
		public string PowerUsageLabel
		{
			get => $"{Outlet.TotalWattage} W";
		}

		public Microwave Microwave { get; set; }
		public string MicrowaveTimer
		{
			get => microwaveTimer;
			set => SetField(ref microwaveTimer, value);
		}
		public bool MicrowavePlugged
		{
			get => Outlet.IsPluggedIn(Microwave);
			set
			{
				if (!value)
				{
					if (Microwave.Active)
					{
						Microwave.Deactivate();
					}
					Outlet.Unplug(Microwave);
					MicrowaveTimer = "";
				}
				else
				{
					Outlet.Plug(Microwave);
					MicrowaveTimer = $"0:00";
				}
				UpdateWattage();
			}
		}
		public SolidColorBrush MicrowaveLights
		{
			get => microwaveLights;
			set => SetField(ref microwaveLights, value);
		}

		public PhoneCharger Charger { get; set; }
		public bool ChargerPlugged
		{
			get => Outlet.IsPluggedIn(Charger);
			set
			{
				if (!value)
				{
					Outlet.Unplug(Charger);
					PhoneLights = Brushes.Gray;
				}
				else
				{
					Outlet.Plug(Charger);
					PhoneLights = Brushes.Bisque;
				}
				UpdateWattage();
			}
		}
		public SolidColorBrush PhoneLights
		{
			get => phoneLights;
			set => SetField(ref phoneLights, value);
		}

		public Miner Miner { get; set; }
		public bool MinerPlugged
		{
			get => Outlet.IsPluggedIn(Miner);
			set
			{
				if (!value)
				{
					if (Miner.Active)
					{
						Miner.Deactivate();
					}
					Outlet.Unplug(Miner);
				}
				else
				{
					Outlet.Plug(Miner);
				}
				UpdateWattage();
				UpdateBitcoinBalance();
			}
		}
		public string MinerBalance
		{
			get {
				if (MinerPlugged)
				{
					return $"{Miner.Balance:0.00000000} BTC";
				}
				return "";
			}
		}


		public RelayCommand ToggleMinerCommand
		{
			get
			{
				return toggleMinerCommand ??= new RelayCommand(obj =>
				{
					if (!MinerPlugged) return;

					if (!Miner.Active)
						Miner.Activate();
					else
						Miner.Deactivate();
					UpdateWattage();
					UpdateBitcoinBalance();
				});
			}
		}

		public RelayCommand ToggleMicrowaveCommand
		{
			get
			{
				return toggleMicrowaveCommand ??= new RelayCommand(obj =>
				{
					if (!MicrowavePlugged) return;

					if (!Microwave.Active)
						Microwave.Activate();
					else
						Microwave.Deactivate();
					UpdateWattage();
					MicrowaveLights = Microwave.Active ? Brushes.Orange : Brushes.SaddleBrown;
				});
			}
		}

		public void UpdateWattage()
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PowerUsageLabel)));
		}

		public void UpdateBitcoinBalance() 
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinerBalance)));
		}

		public Lab13ViewModel()
		{
			Outlet = new();

			Microwave = new();
			Microwave.OnSecond += (object? sender, TickEventArgs e) =>
			{
				if (e.Remaining == 60)
				{
					MicrowaveTimer = $"1:00";
					return;
				}
				MicrowaveTimer = $"0:{e.Remaining:00}";
			};
			Microwave.Cooked += (object? sender, EventArgs e) =>
			{
				MicrowaveTimer = $"0:00";
				MicrowaveLights = Brushes.SaddleBrown;
				UpdateWattage();
			};

			Charger = new();
			Miner = new();
			Miner.OnMined += (object? sender, EventArgs e) =>
			{
				UpdateBitcoinBalance();
			};
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return;

			field = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
