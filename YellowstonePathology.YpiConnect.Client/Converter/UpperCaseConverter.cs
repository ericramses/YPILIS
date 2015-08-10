using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.YpiConnect.Client.Converter
{
    [ValueConversion(typeof(string), typeof(string))]
    class UpperCaseConverter : IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            return value.ToString().ToUpper();
        }
    }
}
