using LabExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LabExplorer.MVVM
{
	public sealed class MainViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<LabTab> tabs = new();
		public ObservableCollection<LabTab> Tabs { get => tabs; set => SetField(ref tabs, value); }

		private int selectedTabIndex = AppHelpers.Settings.LastOpenedLab;
		public int SelectedTabIndex
		{
			get => selectedTabIndex;
			set
			{
				SetField(ref selectedTabIndex, value);
				AppHelpers.Settings.LastOpenedLab = value;
				AppHelpers.Settings.Save();
			}
		}

		public MainViewModel()
		{
			var pagesNamespace = typeof(App).Namespace + ".Pages";

			var pageTypes = Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => t.Namespace == pagesNamespace && t.IsSubclassOf(typeof(Page)));

			foreach (var pageType in pageTypes)
			{
				var page = (Page?)Activator.CreateInstance(pageType);

				if (page is null)
					continue;

				Tabs.Add(new LabTab { Name = page.Title, Content = $"../Pages/{pageType.Name}.xaml" });
			}

			// make Settings tab first
			var settingsTab = Tabs.FirstOrDefault(t => t.Name == "Налаштування");

			if (settingsTab is not null)
			{
				Tabs.Remove(settingsTab);
				Tabs.Insert(0, settingsTab);
			}
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
