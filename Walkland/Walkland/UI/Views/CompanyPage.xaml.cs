using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class CompanyPage : MvxContentPage<CompanyPageViewModel>
    {
        public CompanyPage()
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

       private void companylocation_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (companylocation.SelectedItem != null)
            {
                companylocation.SelectedItem = null;
            }
        }
    }
}