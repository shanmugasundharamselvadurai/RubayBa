using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    [MvxContentPagePresentation(NoHistory = true)]
    public partial class LoginWithEmailPage : MvxContentPage<LoginPageViewModel>
    {
        public LoginWithEmailPage()
        {
            InitializeComponent();
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }
    }
}