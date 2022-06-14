
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class SearchPage : MvvmCross.Forms.Views.MvxContentPage<SearchPageViewModel>
    {
       
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(250);
                entrySearch.Focus();
            });           
        }
        
        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductDetail.SelectedItem != null)
            {
                ProductDetail.SelectedItem = null;
            }
            if (CompanyDetail.SelectedItem != null)
            {
                CompanyDetail.SelectedItem = null;
            }

        }
    }
}