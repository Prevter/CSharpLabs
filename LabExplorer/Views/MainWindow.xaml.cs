using System.Windows;

namespace LabExplorer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			App.SetTheme(AppHelpers.Settings.Theme);
		}
	}
}
