using System;
using System.Globalization;
using System.Windows.Data;

namespace paymentterminal.Converters
{
    public class TaxCalculationConverter : IMultiValueConverter
    {
        // values[0]: PaymentAmountAfterCode (base amount)
        // values[1]: Tax rate (as a decimal; e.g. 0.20 for 20%)
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 &&
                values[0] is decimal baseAmount &&
                values[1] is decimal taxRate)
            {
                decimal taxAmount = baseAmount * taxRate;
                return taxAmount.ToString("C"); // Format as currency
            }
            return Binding.DoNothing;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
