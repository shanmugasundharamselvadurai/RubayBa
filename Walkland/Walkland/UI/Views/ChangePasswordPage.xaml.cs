using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    public partial class ChangePasswordPage : MvxContentPage<ChangePasswordPageViewModel>
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
            OldPassword.ReturnCommand = new Command(() => NewPassword.Focus());
            NewPassword.ReturnCommand = new Command(() => ConfirmNewPassword.Focus());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OldPassword.Focus();          
        }
    }
}