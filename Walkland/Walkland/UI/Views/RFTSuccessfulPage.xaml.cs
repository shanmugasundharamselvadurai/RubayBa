using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    [MvxContentPagePresentation(NoHistory = true)]
    public partial class RFTSuccessfulPage : MvxContentPage<RFTSuccessfulPageViewModel>
    {
        public RFTSuccessfulPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}