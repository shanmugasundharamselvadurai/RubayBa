using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class CompanySubCategoryPage : MvxContentPage<CompanySubCategoryPageViewModel>
    {
        public CompanySubCategoryPage()
        {
            InitializeComponent();
           
        }

        void CompanySubList_SelectionChanged_1(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (CompanySubList.SelectedItem != null)
            {
                CompanySubList.SelectedItem = null;
            }
        }
    }
}
