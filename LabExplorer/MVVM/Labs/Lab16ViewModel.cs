using LabExplorer.Common;
using Labs.Lab16;
using System;
using System.Globalization;
using System.Windows;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab16ViewModel : BaseViewModel
	{
		private string firstEquation, secondEquation, thirdEquation;
		private string aValue, bValue;

		private RelayCommand keyDownCommand;
		public event EventHandler OnKeyPressed;

		public RelayCommand KeyDownCommand
		{
			get => keyDownCommand ??= new RelayCommand(obj =>
			{
				OnKeyPressed?.Invoke(this, null);
			});
		}

		public string AValue
		{
			get => aValue;
			set
			{
				SetField(ref aValue, value);
				UpdateValues();
			}
		}

		public string BValue
		{
			get => bValue;
			set
			{
				SetField(ref bValue, value);
				UpdateValues();
			}
		}

		public string FirstEquation
		{
			get => firstEquation;
			set => SetField(ref firstEquation, value);
		}

		public string SecondEquation
		{
			get => secondEquation;
			set => SetField(ref secondEquation, value);
		}

		public string ThirdEquation
		{
			get => thirdEquation;
			set => SetField(ref thirdEquation, value);
		}

		public void UpdateValues()
		{
			if (aValue.Parse(out double a) && bValue.Parse(out double b))
			{
				double value1 = Delegates.DefinedIntegral(Delegates.FirstIntegral, a, b);
				FirstEquation = $"\\int_{{{aValue}}}^{{{bValue}}}\\frac{{1}}{{\\sqrt[3]{{x}}}}dx={value1.ToString(CultureInfo.InvariantCulture)}";
				double value2 = Delegates.DefinedIntegral(Delegates.SecondIntegral, a, b);
				SecondEquation = $"\\int_{{{aValue}}}^{{{bValue}}}\\frac{{sin(x)}}{{\\sqrt{{x^2}}}}dx={value2.ToString(CultureInfo.InvariantCulture)}";
				double value3 = Delegates.DefinedIntegral(Delegates.ThirdIntegral, a, b);
				ThirdEquation = $"\\int_{{{aValue}}}^{{{bValue}}}xcos(x)dx={value3.ToString(CultureInfo.InvariantCulture)}";
			}
		}

		public Lab16ViewModel()
		{
			AValue = "5";
			BValue = "10";
			UpdateValues();

			OnKeyPressed += (object? sender, EventArgs e) =>
			{
				MessageBox.Show("Олександр");
			};
		}
	}
}
