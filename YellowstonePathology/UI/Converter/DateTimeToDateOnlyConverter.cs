using System;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    public class DateTimeToDateOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            DateTime dateTime = (DateTime)value;
            if (dateTime.ToShortTimeString() == "12:00 AM")
            {
                return dateTime.ToShortDateString();
            }
            return dateTime.ToShortDateString() + " " + dateTime.ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = value.ToString();
            if (strValue == string.Empty)
            {
                return null;
            }
            else
            {
                string fmtString = strValue;
                DateTime dt;
                if (strValue.IndexOf("/") == -1)
                {
                    if (strValue.IndexOf(" ") == 8 || strValue.Length == 8)
                    {
                        fmtString = fmtString.Insert(2, "/");
                        fmtString = fmtString.Insert(5, "/");
                    }
                }
                DateTime.TryParse(fmtString, out dt);
                DateTime dt1;
                DateTime.TryParse(dt.ToShortDateString(), out dt1);

                return dt1;
            }
        }
    }
}
