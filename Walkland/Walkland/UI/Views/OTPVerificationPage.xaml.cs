using MvvmCross.Forms.Views;
using Walkland.Core.Services;
using Walkland.Core.ViewModels;

namespace Walkland.UI.Views
{
    public partial class OTPVerificationPage : MvxContentPage<OTPVerificationPageViewModel>
    {
        public OTPVerificationPage()
        {
            InitializeComponent();
            UserInputOTP.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = UserInputOTP.Focus();
            CommonServices.ListenToSmsRetriever();

            this.Subscribe<string>(Events.SmsRecieved, code =>
            {
                UserInputOTP.Text = code;
            });
        }
    }
}