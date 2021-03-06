﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class MilitaryTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return string.Empty;
			}
			DateTime dateTime = (DateTime)value;
			return dateTime.ToString("HH:mm");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}
	}
}
