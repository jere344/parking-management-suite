using System;
using System.Globalization;
using System.Windows.Data;

namespace paymentterminal.Converters
{
    public class TimeSpanToHumanReadableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                if (timeSpan.TotalDays >= 1)
                    return $"{(int)timeSpan.TotalDays} days";
                if (timeSpan.TotalHours >= 1)
                    return $"{(int)timeSpan.TotalHours} hours";
                if (timeSpan.TotalMinutes >= 1)
                    return $"{(int)timeSpan.TotalMinutes} minutes";
                return $"{(int)timeSpan.TotalSeconds} seconds";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}