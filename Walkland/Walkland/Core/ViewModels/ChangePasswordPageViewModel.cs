using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Walkland;
using Walkland.Core.Exceptions;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.ViewModels
{
    public class ChangePasswordPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        protected readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;

        public ChangePasswordPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _authenticationService = authenticationService;
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _oldpassword;
        public string OldPassword
        {
            get => _oldpassword;
            set => SetProperty(ref _oldpassword, value);
        }

        private string _newpassword;
        public string NewPassword
        {
            get => _newpassword;
            set => SetProperty(ref _newpassword, value);
        }

        private string _confirmnewpassword;
        public string ConfirmNewPassword
        {
            get => _confirmnewpassword;
            set => SetProperty(ref _confirmnewpassword, value);
        }

        public IMvxAsyncCommand ChangePasswordCommand => new MvxAsyncCommand(async () =>
        {
            if (NewPassword != ConfirmNewPassword)
            {
                _userDialogs.Toast("Please enter same password in both fields");
            }
            else
            {
                var changePasswordDto = new ChangePasswordRequestDto
                {
                    OldPassword = OldPassword,
                    NewPassword = NewPassword,
                };

                try
                {
                    await _authenticationService.ChangePasswordAsync(changePasswordDto);
                    await _userDialogs.AlertAsync("Password changed successfully.", "Rupay Bachao", "OK");
                }
                catch (AppException exception)
                {
                    _userDialogs.Toast(exception.Message);
                }               
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}