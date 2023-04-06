using Newtonsoft.Json;
using System.IO;

namespace LabExplorer.Common
{
	public sealed class Settings
	{
		public string Theme { get; set; } = "DarkTheme.xaml";
		public int LastOpenedLab { get; set; }

		// Load settings from file
		public static Settings Load()
		{
			if (File.Exists("settings.json"))
			{
				string json = File.ReadAllText("settings.json");
				Settings? settings = JsonConvert.DeserializeObject<Settings>(json);
				if (settings != null)
					return settings;
			}

			return new Settings();
		}

		// Save settings to file
		public void Save()
		{
			File.WriteAllText("settings.json", JsonConvert.SerializeObject(this));
		}
	}
}
