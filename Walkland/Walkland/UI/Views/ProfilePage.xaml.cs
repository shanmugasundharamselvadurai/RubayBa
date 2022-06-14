using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Walkland.Core.ViewModels;

namespace Walkland.UI.Views
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
    public partial class ProfilePage : MvxContentPage<ProfilePageViewModel>
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}