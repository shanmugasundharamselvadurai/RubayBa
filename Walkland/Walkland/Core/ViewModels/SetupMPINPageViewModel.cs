using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class SetupMPINPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        public SetupMPINPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
        }

        private string _text1;
        public string Text1
        {
            get => _text1;
            set => SetProperty(ref _text1, value);
        }

        private string _text2;
        public string Text2
        {
            get => _text2;
            set => SetProperty(ref _text2, value);
        }

        private string _text3;
        public string Text3
        {
            get => _text3;
            set => SetProperty(ref _text3, value);
        }

        private string _text4;
        public string Text4
        {
            get => _text4;
            set => SetProperty(ref _text4, value);
        }

        public IMvxAsyncCommand SavedMPINVerifyCommand => new MvxAsyncCommand(async () =>
        {
            using (_userDialogs.Loading("Loading..."))
            {
                if ((!string.IsNullOrWhiteSpace(Text1)) && (!string.IsNullOrWhiteSpace(Text2)) && (!string.IsNullOrWhiteSpace(Text3)) && (!string.IsNullOrWhiteSpace(Text4)))
                {
                    var pin = Text1 + Text2 + Text3 + Text4;
                    await SecureStorage.SetAsync("MPin", pin);

                    await _navigationService.Navigate<DashboardPageViewModel>();
                }
                else
                {
                    _userDialogs.Toast("Please enter all four fields");
                }
            }
        });
    }
}