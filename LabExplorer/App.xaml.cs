using LabExplorer.Common;
using System;
using System.Windows;

namespace LabExplorer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			AppHelpers.Settings = Settings.Load();
		}

		public static void SetTheme(string theme)
		{
			if (string.IsNullOrEmpty(theme))
				return;

			if (AppHelpers.Theme is null)
			{
				// initialize theme
				AppHelpers.Theme = new ResourceDictionary();
				AppHelpers.Theme.Source = new Uri($"/Themes/{theme}", UriKind.Relative);
				Current.Resources.MergedDictionaries.Add(AppHelpers.Theme);
			}
			else
			{
				AppHelpers.Theme.Source = new Uri($"/Themes/{theme}", UriKind.Relative);

				// re-add theme to merged dictionaries
				Current.Resources.MergedDictionaries.Remove(AppHelpers.Theme);
				Current.Resources.MergedDictionaries.Add(AppHelpers.Theme);
			}

			// save settings
			AppHelpers.Settings.Theme = theme;
			AppHelpers.Settings.Save();
		}
	}
}
