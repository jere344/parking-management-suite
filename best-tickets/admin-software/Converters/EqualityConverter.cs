using System;
using System.Globalization;
using System.Windows.Data;

namespace admintickets.Converters
{
    public class EqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(parameter) ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEqual && isEqual)
            {
                return parameter;
            }
            return Binding.DoNothing;
        }
    }
}
