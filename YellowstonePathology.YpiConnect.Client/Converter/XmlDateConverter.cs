using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace YellowstonePathology.YpiConnect.Client.Converter
{
	[ValueConversion(typeof(String), typeof(DateTime))]
	public class XmlDateTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return null;
			}
			else
			{
				DateTime dt;
				strValue.Replace('T', ' ');
				DateTime.TryParse(strValue, out dt);
				return dt;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}

	[ValueConversion(typeof(String), typeof(String))]
	public class XmlDateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return null;
			}
			else
			{
				DateTime dt;
				int idx = strValue.IndexOf('T');
				string shortDate = strValue.Substring(0, idx);
				DateTime.TryParse(shortDate, out dt);
				return dt.ToShortDateString();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
