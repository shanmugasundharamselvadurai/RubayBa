using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class CompanyWithLocationPage : MvxContentPage<CompanyWithLocationPageViewModel>
    {
        public CompanyWithLocationPage()
        {
            InitializeComponent();
        }

        private void CollectionView_ProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductLists.SelectedItem != null)
            {
                ProductLists.SelectedItem = null;
            }
        }
    }
}