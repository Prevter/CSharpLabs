using LabExplorer.Common;
using Labs.Lab11;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab11ViewModel : INotifyPropertyChanged
	{
		#region Task 1 Fields

		private string name = "Олександр", lastName = "Немеш", secondName = "Олександрович";
		private string result1 = "";

		public string Name
		{
			get => name;
			set
			{
				SetField(ref name, value);
				UpdateResult1();
			}
		}

		public string LastName
		{
			get => lastName;
			set
			{
				SetField(ref lastName, value);
				UpdateResult1();
			}
		}

		public string SecondName
		{
			get => secondName;
			set
			{
				SetField(ref secondName, value);
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

		private string password = "fGp@3xZa";
		private string message = "Hello World!";
		private RelayCommand encryptCommand;
		private RelayCommand decryptCommand;

		public string Password { get => password; set => SetField(ref password, value); }

		public string Message { get => message; set => SetField(ref message, value); }

		public RelayCommand EncryptCommand => encryptCommand ??= new RelayCommand(Encrypt);

		public RelayCommand DecryptCommand => decryptCommand ??= new RelayCommand(Decrypt);

		#endregion

		public void UpdateResult1()
		{
			var lastNameHorizontal = Lab11.HorizontalString(LastName);

			ResultTask1 = $"Довжина імені: {Name.Length}\n" +
				$"Довжина прізвища: {LastName.Length}\n" +
				$"Довжина по-батькові: {SecondName.Length}\n" +
				$"Прізвище по горизонталі:\n{lastNameHorizontal}";
		}

		public void Encrypt(object? e)
		{
			try
			{
				var encrypted = Lab11.Encrypt(Message, Password);
				Message = encrypted;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void Decrypt(object? e)
		{
			try
			{
				var decrypted = Lab11.Decrypt(Message, Password);
				Message = decrypted;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public Lab11ViewModel()
		{
			UpdateResult1();
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
