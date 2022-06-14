using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class MyFavoritePage : MvxContentPage<MyFavoritePageViewModel>
    {
        public MyFavoritePage()
        {
            InitializeComponent();
        }

        private void CollectionView_FavouriteProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FavouriteProduct.SelectedItem != null)
            {
                FavouriteProduct.SelectedItem = null;
            }
        }
    }
}