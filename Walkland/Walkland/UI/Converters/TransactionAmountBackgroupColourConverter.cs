using System;
using System.Globalization;
using Walkland.Core.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.UI.Converters
{
    public class TransactionAmountBackgroupColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            var transaction = value as CompanyWalletTransaction;
            return SecureStorage.GetAsync("CommpanyWalletId").Result == transaction.ToWalletId.ToString() ? Color.FromHex("#c9f7f5") : Color.FromHex("#ffe2e5");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}