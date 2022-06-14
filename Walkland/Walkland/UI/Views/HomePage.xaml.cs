using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    [MvxTabbedPagePresentation]
    public partial class HomePage : MvxContentPage<HomePageViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void CollectionView_CompanyCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (companylist.SelectedItem != null)
            {
                companylist.SelectedItem = null;
            }
        }

        private void CollectionView_LatestDealSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (latestdeal.SelectedItem != null)
            {
                latestdeal.SelectedItem = null;
            }
        }

        private void CollectionView_NearbyLocationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nearbyLocation.SelectedItem != null)
            {
                nearbyLocation.SelectedItem = null;
            }
        }
    }
}