using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
