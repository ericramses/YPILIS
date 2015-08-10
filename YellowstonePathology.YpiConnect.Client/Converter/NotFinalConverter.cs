using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.YpiConnect.Client.Converter
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class NotFinalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            string result = string.Empty;
            Nullable<DateTime> dateTime = (Nullable<DateTime>)value;
            if (dateTime.HasValue == false)
            {
                result = "Not Final";
            }
            else
            {
                result = dateTime.Value.ToString("MM/dd/yyyy hhhh:mm");
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
