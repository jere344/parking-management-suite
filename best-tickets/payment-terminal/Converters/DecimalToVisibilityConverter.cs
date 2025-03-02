using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace paymentterminal.Converters
{
    public class DecimalToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal amount && amount != 0)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
    
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}