using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Windows.Input;
using Walkland.Core.Services.Interfaces;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class GuestLoginPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;

        public GuestLoginPageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _authenticationService = authenticationService;

        }

        private string _firstname;
        public string FirstName
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        private bool _IsCheck;
        public bool IsCheck
        {
            get => _IsCheck;
            set => SetProperty(ref _IsCheck, value);
        }

        public IMvxAsyncCommand GuestLoginCommand => new MvxAsyncCommand(async () =>
        {
            if (IsCheck)
            {
                var code = GenerateLoginCode(6);
                using (_userDialogs.Loading("Loading..."))
                {
                    await _authenticationService.GuestRegisterSignInAsync(new GuestUserRequestDto()
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        Mobile = Mobile,
                        Code = code
                    });

                    //await _textingService.SendTextAsyc("", $"Your OTP for Rupay Bachao login is {code}.");
                    await _navigationService.Navigate<OTPVerificationPageViewModel, string>(code);
                }
            }
            else
            {
                _userDialogs.Toast("Please accept Terms and Condition of Rupay Bachao.");
            }
        });

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Xamarin.Essentials.Launcher.OpenAsync(url);
        });

        private string GenerateLoginCode(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = string.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
