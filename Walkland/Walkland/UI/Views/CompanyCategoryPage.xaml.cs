using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class CompanyCategoryPage : MvxContentPage<CompanyCategoryPageViewModel>
    {
        public CompanyCategoryPage()
        {
            InitializeComponent();         
        }

        private void CollectionView_CompanySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompanyList.SelectedItem != null)
            {
                CompanyList.SelectedItem = null;
            }
        }

        void CompanyList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
        }
    }
}