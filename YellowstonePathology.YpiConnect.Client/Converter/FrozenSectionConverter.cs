using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.YpiConnect.Client.Converter
{
	[ValueConversion(typeof(string), typeof(string))]
	public class FrozenSectionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = null;
			if (value != null)
			{
				string valueString = value.ToString();
				if (string.IsNullOrEmpty(valueString) == true) result = "No";
				else if (valueString == "Routine Surgical Pathology") result = "No";
				else if (valueString == "Immediate Exam (with frozen section)") result = "Yes";
			}
			
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string result = "Routine Surgical Pathology";
			if (value != null)
			{
				string valueString = value.ToString();
				if (valueString == "Yes") result = "Immediate Exam (with frozen section)";
			}
			
			return result;
		}
	}
}
