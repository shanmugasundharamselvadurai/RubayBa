using System;
using System.Globalization;
using Walkland.Core.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.UI.Converters
{
    public class TransactionCompanyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            var transaction = value as CompanyWalletTransaction;
            return SecureStorage.GetAsync("CommpanyWalletId").Result == transaction.ToWalletId.ToString() ? transaction.FromWallet?.Company.BrandName : transaction.ToWallet?.Company.BrandName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}