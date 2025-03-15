using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace admintickets.Converters
{
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isInverted = parameter as string == "invert";
            bool result = value != null;
            
            if (targetType == typeof(Visibility))
                return (isInverted ? !result : result) ? Visibility.Visible : Visibility.Collapsed;
                
            return isInverted ? !result : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
