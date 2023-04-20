using LabExplorer.Common;
using Labs.Lab14;
using Labs.Lab15;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab15ViewModel : BaseViewModel
	{
		private string leg, angle;
		private double leg1Value, leg2Value, hypotenuseValue, angleValue;
		private double area, perimeter;

		public string Leg
		{
			get => leg;
			set
			{
				SetField(ref leg, value);
				UpdateValues();
			}
		}

		public string Angle
		{
			get => angle;
			set
			{
				SetField(ref angle, value);
				UpdateValues();
			}
		}

		public double AngleValue
		{
			get => angleValue;
			set => SetField(ref angleValue, value);
		}

		public double Leg1Value
		{
			get => leg1Value;
			set => SetField(ref leg1Value, value);
		}

		public double Leg2Value
		{
			get => leg2Value;
			set => SetField(ref leg2Value, value);
		}

		public double HypotenuseValue
		{
			get => hypotenuseValue;
			set => SetField(ref hypotenuseValue, value);
		}

		public double Area
		{
			get => area;
			set => SetField(ref area, value);
		}

		public double Perimeter
		{
			get => perimeter;
			set => SetField(ref perimeter, value);
		}

		public void UpdateValues()
		{
			if (Leg.Parse(out double parsedLeg) && Angle.Parse(out double parsedAngle) 
				&& parsedAngle > 0 && parsedAngle < 90 && parsedLeg > 0)
			{
				Leg1Value = parsedLeg;
				AngleValue = parsedAngle;

				Leg2Value = Utils.GetLeg2(parsedLeg, parsedAngle);
				HypotenuseValue = Utils.GetHypotenuse(parsedLeg, Leg2Value);
				Area = Utils.GetArea(parsedLeg, Leg2Value);
				Perimeter = Utils.GetPerimeter(parsedLeg, Leg2Value, HypotenuseValue);
			}
		}

		private string text, reversedText;
		private RelayCommand modifyTextCommand, saveToFileCommand;

		public string Text
		{
			get => text;
			set => SetField(ref text, value);
		}

		public string ReversedText
		{
			get => reversedText;
			set => SetField(ref reversedText, value);
		}

		public RelayCommand ModifyTextCommand
		{
			get
			{
				return modifyTextCommand ??= new RelayCommand(obj =>
				{
					ReversedText = Utils.ReverseText(Text);
				});
			}
		}

		public RelayCommand SaveToFileCommand
		{
			get
			{
				return saveToFileCommand ??= new RelayCommand(obj =>
				{
					try
					{
						var dialog = new Microsoft.Win32.SaveFileDialog
						{
							Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
							FilterIndex = 1,
							RestoreDirectory = true
						};

						if (dialog.ShowDialog() == true)
						{
							File.WriteAllText(dialog.FileName, ReversedText);
							MessageBox.Show("Успішно збережено файл!");
						}
					}
					catch (Exception e)
					{
						MessageBox.Show($"Error!", $"{e.Message}");
					}
				});
			}
		}

		public Lab15ViewModel()
		{
			Leg = "7";
			Angle = "45";
			UpdateValues();
		}
	}
}
