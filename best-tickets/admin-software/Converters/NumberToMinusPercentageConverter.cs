using System;
using System.Globalization;
using System.Windows.Data;

namespace admintickets.Converters
{
    public class NumberToMinusPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double reduction)
            {
                return $"-{Math.Round(reduction)}%";
            }
            if (value is int reductionInt)
            {
                return $"-{reductionInt}%";
            }
            if (value is string reductionString)
            {
                return $"-{reductionString}%";
            }
            if (value is decimal reductionDecimal)
            {
                return $"-{Math.Round(reductionDecimal)}%";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}