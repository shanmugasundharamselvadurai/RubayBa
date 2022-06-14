using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;

namespace Walkland.UI.Views
{
    public partial class GuestLoginPage : MvxContentPage<GuestLoginPageViewModel>
    {
        public GuestLoginPage()
        {
            InitializeComponent();
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }
    }
}