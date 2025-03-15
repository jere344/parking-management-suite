using System;
using System.Globalization;
using System.Windows.Data;

namespace admintickets.Converters
{
    public class TimeSpanToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return new DateTime(1, 1, 1, 
                    timeSpan.Hours, 
                    timeSpan.Minutes, 
                    timeSpan.Seconds);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second);
            }
            return TimeSpan.Zero;
        }
    }
}
