using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.YpiConnect.Client.Converter
{
    [ValueConversion(typeof(string), typeof(bool))]
    class OrderTypeConverter : IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;
            //if (value != null)
            //{
                if ((string)value == (string)parameter)
                {
                    result = true;
                }
            //}
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;
            //if (value != null)
            //{
                if ((bool)value == true)
                {
                    result = (string)parameter;
                }
            //}
            return result;
        }
    }
}
