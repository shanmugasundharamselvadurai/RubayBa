using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class MPINLoginPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;

        public MPINLoginPageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _authenticationService = authenticationService;
            _ = AuthAsync();

        }

        private async Task AuthAsync()
        {
            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (!isFingerprintAvailable)
            {
                _userDialogs.Toast("No biomertics available");
                isVisible = true;
                return;
            }
            else
            {
                var authType = await CrossFingerprint.Current.GetAuthenticationTypeAsync();
                if (authType == AuthenticationType.Fingerprint)
                {
                    await callFingerPrint();
                }
                else if (authType == AuthenticationType.Face)
                {
                    await callFace();
                }
                else
                {
                    return;
                }
            }
           
        }

        /// <summary>
        /// callFingerPrint 
        /// </summary>
        /// <returns></returns>
        private async Task callFingerPrint()
        {
            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new
                AuthenticationRequestConfiguration("Rupay Bachao", "Please authenticate"));

            if (authResult.Authenticated)
            {
                isVisible = false;
                await _navigationService.Navigate<DashboardPageViewModel>();
            }
            else
            {
              _userDialogs.Toast("Failed to authenticate");
              isVisible = true;
            }
        }

        /// <summary>
        /// callFace
        /// </summary>
        /// <returns></returns>
        private async Task callFace()
        {
            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new
                AuthenticationRequestConfiguration("Rupay Bachao", "Please authenticate"));

            if (authResult.Authenticated)
            {
                isVisible = false;
                await _navigationService.Navigate<DashboardPageViewModel>();
            }
            else
            {
                _userDialogs.Toast("Failed to authenticate");
                isVisible = true;
            }
        }

        private string _text1;
        public string Text1
        {
            get => _text1;
            set => SetProperty(ref _text1, value);
        }

        private bool _isVisible =false;
        public bool isVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
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

        public IMvxAsyncCommand LoginMPINVerifyCommand => new MvxAsyncCommand(async () =>
        {
            if ((!string.IsNullOrWhiteSpace(Text1)) && (!string.IsNullOrWhiteSpace(Text2)) && (!string.IsNullOrWhiteSpace(Text3)) && (!string.IsNullOrWhiteSpace(Text4)))
            {
                if (await _authenticationService.ValidateToken())
                {
                    var pin = Text1 + Text2 + Text3 + Text4;
                    var pinSaved = await SecureStorage.GetAsync("MPin");

                    if (pin == pinSaved)
                    {
                        await _navigationService.Navigate<DashboardPageViewModel>();
                    }
                    else
                    {
                        _userDialogs.Toast("Invalid MPIN");
                    }
                }
            }
            else
            {
                _userDialogs.Toast("Please enter all four fields");
            }
        });
    }
}