using System.Globalization;

namespace LabExplorer.Common
{
	public static class Extensions
	{
		public static bool Parse(this string str, out int result)
		{
			return int.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
		}

		public static bool Parse(this string str, out double result)
		{
			return double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
		}
	}
}
