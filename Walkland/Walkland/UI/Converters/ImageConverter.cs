using System;
using System.Globalization;
using Walkland.Core.Constant;
using Xamarin.Forms;

namespace Walkland.UI.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                var imageUrl = value.ToString();
                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    retSource = ImageSource.FromUri(new Uri(Constants.BannerImageURL + imageUrl));
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