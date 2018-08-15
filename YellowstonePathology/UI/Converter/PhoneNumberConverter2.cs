using System;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{
    class PhoneNumberConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string phoneNumber = (string)value;
            if (string.IsNullOrEmpty(phoneNumber) == false)
            {
                phoneNumber = phoneNumber.Replace(" ", "");
                phoneNumber = phoneNumber.Replace(")", "");
                phoneNumber = phoneNumber.Replace("(", "");
                phoneNumber = phoneNumber.Replace("-", "");

                if (phoneNumber.Length == 11)
                {
                    return phoneNumber;
                }
                else
                {
                    while(phoneNumber.Length < 11)
                    {
                        phoneNumber = " " + phoneNumber;
                    }
                    return phoneNumber;
                }
            }
            else
            {
                return phoneNumber;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string phoneNumber = (string)value;
            return phoneNumber;
        }
    }
}
