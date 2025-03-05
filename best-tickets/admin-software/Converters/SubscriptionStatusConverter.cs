using System;
using System.Globalization;
using System.Windows.Data;
using ticketlibrary.Models;

namespace admintickets.Converters
{
    public class SubscriptionStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Subscription subscription)
            {
                DateTime now = DateTime.Now;
                return (now >= subscription.DateStart && now <= subscription.DateEnd) ? "Active" : "Expired";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
