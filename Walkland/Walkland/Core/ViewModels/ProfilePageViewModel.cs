using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Walkland.Core.Services.Interfaces;
using Walkland.Core.ViewModels;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class ProfilePageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public ProfilePageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    SetProperty(ref _email, value);
                }
            }
        }

        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    SetProperty(ref _username, value);
                }
            }
        }

        private string _uname;
        public string UName
        {
            get { return _uname; }
            set
            {
                if (_uname != value)
                {
                    SetProperty(ref _uname, value);
                }
            }
        }

        private string _accno;
        public string AccNo
        {
            get { return _accno; }
            set
            {
                if (_accno != value)
                {
                    SetProperty(ref _accno, value);
                }
            }
        }

        public override async Task Initialize()
        {
            Email = await SecureStorage.GetAsync("Email");
            UserName = await SecureStorage.GetAsync("FirstName") +(" ") + await SecureStorage.GetAsync("LastName");
            UName = "UserName:-" + await SecureStorage.GetAsync("UName");
            AccNo = "Account Number:-" + await SecureStorage.GetAsync("AccNo");
        }

        public IMvxAsyncCommand MyFavoriteCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<MyFavoritePageViewModel>();
        });

        public IMvxAsyncCommand ChangeMPINCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<ChangeMPINPageViewModel>();
        });

        public IMvxAsyncCommand ChangePasswordCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<ChangePasswordPageViewModel>();
        });

        public IMvxAsyncCommand ReturnRFTCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<RFTPageViewModel>();
        });

        public IMvxAsyncCommand TermsandConditionCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<TermandConditionPageViewModel>();
        });

        public IMvxAsyncCommand LogoutCommand => new MvxAsyncCommand(async () =>
        {
            _authenticationService.SignOut();
            await _navigationService.Navigate<LoginPageViewModel>();
        });

        public IMvxAsyncCommand AboutusCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<AboutusPageViewModel>();
        });
    }
}