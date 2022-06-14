using System;
using System.Globalization;
using Xamarin.Forms;

namespace Walkland.UI.Converters
{
    public class TransactionDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            return ((DateTime)value).AddHours(5).AddMinutes(30).ToString("dd-MMM-yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}