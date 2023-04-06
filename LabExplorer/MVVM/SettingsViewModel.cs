using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LabExplorer.MVVM
{
	public sealed class SettingsViewModel : INotifyPropertyChanged
	{
		public List<KeyValuePair<string, string>> Themes = new()
		{
			new("Темна", "DarkTheme.xaml"),
			new("Світла", "LightTheme.xaml")
		};

		public List<string> ThemeNames => Themes.Select(x => x.Key).ToList();

		private int _selectedTabIndex;
		public int SelectedThemeIndex
		{
			get => _selectedTabIndex;
			set
			{
				SetField(ref _selectedTabIndex, value);
				App.SetTheme(Themes[value].Value);
			}
		}

		public SettingsViewModel()
		{
			_selectedTabIndex = Themes.FindIndex(t => t.Value == AppHelpers.Settings.Theme);
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
