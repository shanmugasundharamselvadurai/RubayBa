using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Walkland.Core.ViewModels
{
    public class GuestOTPVerificationPageViewModel :MvxViewModel<string>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        private string _code;

        public GuestOTPVerificationPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
        }

        private string _enterOTPCode;
        public string EnterOTPCode
        {
            get { return _enterOTPCode; }
            set
            {
                if (_enterOTPCode != value)
                {
                    SetProperty(ref _enterOTPCode, value);
                }
            }
        }

        public override void Prepare(string code)
        {
            _code = code;
        }

        public IMvxAsyncCommand OTPVerifyCommand => new MvxAsyncCommand(async () =>
        {
            if (EnterOTPCode == _code)
            {
                await _navigationService.Navigate<SetupMPINPageViewModel>();
            }
            else
            {
                _userDialogs.Toast("Invalid OTP");
            }
        });
    }
}
