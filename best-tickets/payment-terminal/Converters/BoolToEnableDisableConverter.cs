using System;
using System.Globalization;
using System.Windows.Data;

namespace paymentterminal.Converters
{
    public class BoolToEnableDisableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                return isActive ? "Disable" : "Enable";
            }
            return "Enable";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
