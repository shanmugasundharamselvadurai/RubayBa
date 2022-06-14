using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class ChangeMPINPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        public ChangeMPINPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
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

        private string _text5;
        public string Text5
        {
            get => _text5;
            set => SetProperty(ref _text5, value);
        }

        private string _text6;
        public string Text6
        {
            get => _text6;
            set => SetProperty(ref _text6, value);
        }

        private string _text7;
        public string Text7
        {
            get => _text7;
            set => SetProperty(ref _text7, value);
        }

        private string _text8;
        public string Text8
        {
            get => _text8;
            set => SetProperty(ref _text8, value);
        }

        private string _text9;
        public string Text9
        {
            get => _text9;
            set => SetProperty(ref _text9, value);
        }

        private string _text10;
        public string Text10
        {
            get => _text10;
            set => SetProperty(ref _text10, value);
        }

        private string _text11;
        public string Text11
        {
            get => _text11;
            set => SetProperty(ref _text11, value);
        }

        private string _text12;
        public string Text12
        {
            get => _text12;
            set => SetProperty(ref _text12, value);
        }

        public IMvxAsyncCommand ChangeMPINCommand => new MvxAsyncCommand(async () =>
        {
            try
            {
                if ((!string.IsNullOrWhiteSpace(Text1)) && (!string.IsNullOrWhiteSpace(Text2)) && (!string.IsNullOrWhiteSpace(Text3)) && (!string.IsNullOrWhiteSpace(Text4)) &&
                        (!string.IsNullOrWhiteSpace(Text5)) && (!string.IsNullOrWhiteSpace(Text6)) && (!string.IsNullOrWhiteSpace(Text7)) && (!string.IsNullOrWhiteSpace(Text8)) &&
                        (!string.IsNullOrWhiteSpace(Text9)) && (!string.IsNullOrWhiteSpace(Text10)) && (!string.IsNullOrWhiteSpace(Text11)) && (!string.IsNullOrWhiteSpace(Text12)))
                {
                    var oldpin = Text1 + Text2 + Text3 + Text4;
                    var newpin = Text5 + Text6 + Text7 + Text8;
                    var confirmNewPin = Text9 + Text10 + Text11 + Text12;

                    var savedPin = await SecureStorage.GetAsync("MPin");

                    if (oldpin == savedPin && newpin == confirmNewPin)
                    {
                        await SecureStorage.SetAsync("MPin", newpin);
                        await _userDialogs.AlertAsync(newpin, "Your new MPIN is", "Ok");
                        await _navigationService.Navigate<DashboardPageViewModel>();
                    }
                    else if (newpin != confirmNewPin)
                    {
                        _userDialogs.Toast("NewPin and Confirm NewPin doesn't match");
                    }
                    else
                    {
                        _userDialogs.Toast("Please enter correct OldPin");
                    }
                }
                else
                {
                    _userDialogs.Toast("Please enter all four fields");
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}