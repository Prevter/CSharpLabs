using Labs.Lab9;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab09ViewModel : INotifyPropertyChanged
	{
		#region Task 1 Fields

		private string valueX = "5", valueY = "10", valueZ = "2.5";
		private string result1 = "";

		public string ValueX
		{
			get => valueX;
			set
			{
				SetField(ref valueX, value);
				UpdateResult1();
			}
		}
		public string ValueY
		{
			get => valueY;
			set
			{
				SetField(ref valueY, value);
				UpdateResult1();
			}
		}
		public string ValueZ
		{
			get => valueZ;
			set
			{
				SetField(ref valueZ, value);
				UpdateResult1();
			}
		}

		public string ResultTask1
		{
			get => result1;
			set => SetField(ref result1, value);
		}

		#endregion

		#region Task 2 Fields

		private string speed1 = "50", speed2 = "75.3", speed3 = "25.7";
		private string time1 = "2", time2 = "1.5", time3 = "3.5";
		private string result2 = "";

		public string Speed1
		{
			get => speed1;
			set
			{
				SetField(ref speed1, value);
				UpdateResult2();
			}
		}

		public string Speed2
		{
			get => speed2;
			set
			{
				SetField(ref speed2, value);
				UpdateResult2();
			}
		}

		public string Speed3
		{
			get => speed3;
			set
			{
				SetField(ref speed3, value);
				UpdateResult2();
			}
		}

		public string Time1
		{
			get => time1;
			set
			{
				SetField(ref time1, value);
				UpdateResult2();
			}
		}

		public string Time2
		{
			get => time2;
			set
			{
				SetField(ref time2, value);
				UpdateResult2();
			}
		}

		public string Time3
		{
			get => time3;
			set
			{
				SetField(ref time3, value);
				UpdateResult2();
			}
		}

		public string ResultTask2
		{
			get => result2;
			set => SetField(ref result2, value);
		}

		#endregion

		public void UpdateResult1()
		{
			if (!double.TryParse(ValueX, NumberStyles.Any, CultureInfo.InvariantCulture, out double x) ||
				!double.TryParse(ValueY, NumberStyles.Any, CultureInfo.InvariantCulture, out double y) ||
				!double.TryParse(ValueZ, NumberStyles.Any, CultureInfo.InvariantCulture, out double z))
			{
				ResultTask1 = "Некоректні дані";
				return;
			}

			double resultValue = Lab9.CalculateResult(x, y, z);
			string resultStr = resultValue.ToString("0.#####", CultureInfo.InvariantCulture);
			ResultTask1 = $"Результат: {resultStr} ({(resultValue >= 0 ? "позитивне" : "негативне")})";
			ResultTask1 += $"\n{Lab9.FormatX(x)}";
		}

		public void UpdateResult2()
		{
			if (!double.TryParse(Speed1, NumberStyles.Any, CultureInfo.InvariantCulture, out double s1) ||
				!double.TryParse(Speed2, NumberStyles.Any, CultureInfo.InvariantCulture, out double s2) ||
				!double.TryParse(Speed3, NumberStyles.Any, CultureInfo.InvariantCulture, out double s3) ||
				!double.TryParse(Time1, NumberStyles.Any, CultureInfo.InvariantCulture, out double t1) ||
				!double.TryParse(Time2, NumberStyles.Any, CultureInfo.InvariantCulture, out double t2) ||
				!double.TryParse(Time3, NumberStyles.Any, CultureInfo.InvariantCulture, out double t3))
			{
				ResultTask2 = "Некоректні дані";
				return;
			}

			double resultValue = Lab9.CalculateVolume(s1, t1, s2, t2, s3, t3);
			string volume = resultValue.ToString("0.###", CultureInfo.InvariantCulture);
			ResultTask2 = $"Всього води набрано: {volume} л.";
		}

		public Lab09ViewModel()
		{
			UpdateResult1();
			UpdateResult2();
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
