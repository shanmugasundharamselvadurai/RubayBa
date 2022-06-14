using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;


namespace Walkland.UI.Views
{
    public partial class PaymentSucessful : MvxContentPage<PaymentSuccessfulViewModel>
    {
        public PaymentSucessful()
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