using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Services.Interfaces;
using Walkland.Core.ViewModels;
using Xamarin.Essentials;

namespace Walkland.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IAuthenticationService authService)
            : base(application, navigationService)
        {
            _navigationService = navigationService;
            _authenticationService = authService;
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            if (await _authenticationService.ValidateToken())
            {
                var pinSaved = await SecureStorage.GetAsync("MPin");
                if (pinSaved != null)
                {
                    try
                    {
                        await _navigationService.Navigate<MPINLoginPageViewModel>();
                    }
                    catch(Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    await _navigationService.Navigate<LoginPageViewModel>();
                }
            }
            else
            {
                _authenticationService.SignOut();
                await _navigationService.Navigate<LoginPageViewModel>();
            }
        }
    }
}
