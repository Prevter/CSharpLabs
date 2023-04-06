using LabExplorer.Common;
using Labs.Lab10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab10ViewModel : INotifyPropertyChanged
	{
		#region Task 1 Fields

		private string size1 = "10", min1 = "-10", max1 = "10";
		private string result1 = "";
		private RelayCommand updateArray1;

		public string Size1
		{
			get => size1;
			set
			{
				SetField(ref size1, value);
				UpdateResult1();
			}
		}

		public string Min1
		{
			get => min1;
			set
			{
				SetField(ref min1, value);
				UpdateResult1();
			}
		}

		public string Max1
		{
			get => max1;
			set
			{
				SetField(ref max1, value);
				UpdateResult1();
			}
		}

		public string ResultTask1
		{
			get => result1;
			set => SetField(ref result1, value);
		}

		public RelayCommand UpdateArray1
		{
			get
			{
				return updateArray1 ??= new RelayCommand(obj =>
				{
					UpdateResult1();
				});
			}
		}

		#endregion

		#region Task 2 Fields

		private string size2 = "10", min2 = "1", max2 = "25";
		private string result2 = "";
		private RelayCommand updateArray2;

		public string Size2
		{
			get => size2;
			set
			{
				SetField(ref size2, value);
				UpdateResult2();
			}
		}

		public string Min2
		{
			get => min2;
			set
			{
				SetField(ref min2, value);
				UpdateResult2();
			}
		}

		public string Max2
		{
			get => max2;
			set
			{
				SetField(ref max2, value);
				UpdateResult2();
			}
		}

		public string ResultTask2
		{
			get => result2;
			set => SetField(ref result2, value);
		}

		public RelayCommand UpdateArray2
		{
			get
			{
				return updateArray2 ??= new RelayCommand(obj =>
				{
					UpdateResult2();
				});
			}
		}

		#endregion

		public void UpdateResult1()
		{
			if (!Size1.Parse(out int size) || size <= 0)
			{
				ResultTask1 = "Некоректний розмір масиву";
				return;
			}

			if (!Min1.Parse(out int min) || !Max1.Parse(out int max))
			{
				ResultTask1 = "Некоректні межі масиву";
				return;
			}

			int[] array = Lab10.CreateRandomArray(size, min, max);
			int[] negative = Lab10.Sift(array, x => x < 0);

			ResultTask1 =
				$"{Lab10.ArrayCaption(array, "Масив")}\n" +
				$"{Lab10.ArrayCaption(negative, "Негативні")}\n";

			if (negative.Length > 0)
			{
				int max_neg = Lab10.FindMax(negative);
				int index = Array.IndexOf(array, max_neg);
				ResultTask1 += $"Максимальне від'ємне число: Масив[{index}] = {max_neg}";
			}
			else
				ResultTask1 += "Немає негативних елементів";
		}

		public void UpdateResult2()
		{
			if (!Size2.Parse(out int size) || size <= 0)
			{
				ResultTask2 = "Некоректний розмір масиву";
				return;
			}

			if (!Min2.Parse(out int min) || !Max2.Parse(out int max))
			{
				ResultTask2 = "Некоректні межі масиву";
				return;
			}

			int[] array = Lab10.CreateRandomArray(size, min, max);

			int min_val = Lab10.FindMin(array);
			int max_val = Lab10.FindMax(array);

			ResultTask2 =
				$"{Lab10.ArrayCaption(array, "Масив")}\n" +
				$"Мінімальне число: {min_val}\n" +
				$"Максимальне число: {max_val}\n" +
				$"Добуток: {min_val * max_val}";
		}

		public Lab10ViewModel()
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
