using System;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Xamarin.Forms;

namespace Walkland.UI.Converters
{
    public class BannerImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;

            if (value != null)
            {
                var appBanner = value as AppBanner;
                if (!string.IsNullOrWhiteSpace(appBanner.PictureStoragePath))
                {
                    retSource = ImageSource.FromUri(new Uri(Constants.BannerImageURL + appBanner.PictureStoragePath));
                }
                else
                {
                    retSource = ImageSource.FromFile("UserPlaceholder.png");
                }
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}