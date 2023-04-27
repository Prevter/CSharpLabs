using LabExplorer.Common;
using Labs.Lab17;
using System.Globalization;
using System.IO;

namespace LabExplorer.MVVM.Labs
{
	public sealed class Lab17ViewModel : BaseViewModel
	{
		public HourGlass Glass { get; set; }

		private string points, widthValue, heightValue;
		private RelayCommand saveAsBinaryCommand, saveAsXmlCommand;
		private RelayCommand loadAsBinaryCommand, loadAsXmlCommand;

		public string WidthValue
		{
			get => widthValue;
			set
			{
				SetField(ref widthValue, value);
				UpdateValues();
			}
		}

		public string HeightValue
		{
			get => heightValue;
			set
			{
				SetField(ref heightValue, value);
				UpdateValues();
			}
		}

		public string Points { get => points; set => SetField(ref points, value); }

		public RelayCommand SaveAsBinaryCommand
		{
			get => saveAsBinaryCommand ??= new RelayCommand(obj => File.WriteAllBytes("save.bin", BinaryConvert.Serialize(Glass)));
		}

		public RelayCommand SaveAsXmlCommand
		{
			get => saveAsXmlCommand ??= new RelayCommand(obj => File.WriteAllText("save.xml", Glass.XmlSerialize()));
		}

		public RelayCommand OpenAsBinaryCommand
		{
			get => loadAsBinaryCommand ??= new RelayCommand(obj =>
			{
				if (!File.Exists("save.bin")) return;

				Glass = BinaryConvert.Deserialize<HourGlass>(File.ReadAllBytes("save.bin"));
				ResetValues();
			});
		}

		public RelayCommand OpenAsXmlCommand
		{
			get => loadAsXmlCommand ??= new RelayCommand(obj =>
			{
				if (!File.Exists("save.xml")) return;

				Glass = HourGlass.XmlDeserialize(File.ReadAllText("save.xml")) ?? new();
				ResetValues();
			});
		}

		public void UpdateValues()
		{
			if (WidthValue.Parse(out double width) && HeightValue.Parse(out double height))
			{
				Glass.Width = width;
				Glass.Height = height;
				Points = $"0,0 {width},0 0,{height} {width},{height}";
			}
		}

		public void ResetValues()
		{
			SetField(ref widthValue, Glass.Width.ToString(CultureInfo.InvariantCulture), nameof(WidthValue));
			SetField(ref heightValue, Glass.Height.ToString(CultureInfo.InvariantCulture), nameof(HeightValue));
			UpdateValues();
		}

		public Lab17ViewModel()
		{
			Glass = new(10, 15);
			ResetValues();
		}
	}
}
