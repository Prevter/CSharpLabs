using LabExplorer.Common;
using Labs.Lab12;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab12ViewModel : INotifyPropertyChanged
	{
		private Puck puck;
		private string side = "5";
		private string area;
		private string secondSide = "3";
		private string secondNumber = "10";

		private RelayCommand addSide, setArea;

		public RelayCommand AddSideCommand
		{
			get
			{
				return addSide ??= new RelayCommand(obj =>
				{
					AddSide();
				});
			}
		}

		public RelayCommand SetAreaCommand
		{
			get
			{
				return setArea ??= new RelayCommand(obj =>
				{
					SetArea();
				});
			}
		}

		public string Side { get => side; set { SetField(ref side, value); UpdateArea(); } }
		public string Area { get => area; set => SetField(ref area, value); }
		public string SecondSide { get => secondSide; set => SetField(ref secondSide, value); }
		public string SecondNumber { get => secondNumber; set => SetField(ref secondNumber, value); }

		public Puck Puck { get => puck; set => SetField(ref puck, value); }

		public void UpdateArea()
		{
			if (!side.Parse(out double sideValue))
				return;

			Puck = new(sideValue);
			Area = Puck.Area.ToString(CultureInfo.InvariantCulture);
		}

		public void AddSide()
		{
			if (!secondSide.Parse(out double secondSideValue))
				return;

			Puck += new Puck(secondSideValue);
			Side = Puck.A.ToString(CultureInfo.InvariantCulture);
			Area = Puck.Area.ToString(CultureInfo.InvariantCulture);
		}

		public void SetArea()
		{
			if (!secondNumber.Parse(out double secondNumberValue))
				return;

			Puck += secondNumberValue;
			Side = Puck.A.ToString(CultureInfo.InvariantCulture);
			Area = Puck.Area.ToString(CultureInfo.InvariantCulture);
		}

		public Lab12ViewModel()
		{
			UpdateArea();
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
