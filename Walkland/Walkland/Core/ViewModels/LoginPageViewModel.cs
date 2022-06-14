using Acr.UserDialogs;
using Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Windows.Input;
using Walkland.Core.Services.Interfaces;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class LoginPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;

        public LoginPageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _authenticationService = authenticationService;
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _IsCheck;
        public bool IsCheck
        {
            get => _IsCheck;
            set => SetProperty(ref _IsCheck, value);
        }

        public IMvxAsyncCommand LoginCommand => new MvxAsyncCommand(async () =>
        {
            if (IsCheck)
            {
                if (UserName.ToLower() == "apple.test" && Password == "shanu@1234")
                {
                    var code = "123456";
                    IDevice device = DependencyService.Get<IDevice>();
                    string deviceIdentifier = device.GetIdentifier();
                    try
                    {
                        using (_userDialogs.Loading("Loading..."))
                        {
                            await _authenticationService.SignInAsync(new LoginRequestDto()
                            {
                                UserName = UserName,
                                Password = Password,
                                Code = code,
                                DeviceID = deviceIdentifier,
                                UserIP = null
                            });

                            // device id and IP
                            await _navigationService.Navigate<OTPVerificationPageViewModel, string>(code);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                        _userDialogs.Toast("Invalid username or password, please try again.");
                    }
                }
                else
                {
                    var code = GenerateLoginCode(6);

                    try
                    {
                        using (_userDialogs.Loading("Loading..."))
                        {
                            await _authenticationService.SignInAsync(new LoginRequestDto()
                            {
                                UserName = UserName,
                                Password = Password,
                                Code = code
                            });
                            await _navigationService.Navigate<OTPVerificationPageViewModel, string>(code);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                        _userDialogs.Toast("Invalid username or password, please try again.");
                    }
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

        public IMvxAsyncCommand GuestCommand => new MvxAsyncCommand(async () =>
        {
            using (_userDialogs.Loading("Loading..."))
            {
                await _navigationService.Navigate<GuestLoginPageViewModel>();
            }
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