using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Services.Interfaces;
using Walkland.Core.ViewModels;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class RFTMPINPageViewModel : MvxViewModel<dynamic>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletTransactionService _companyWalletTransactionService;
        private string _transactionId;
        private DateTime _createdDateTimeUtc;

        public RFTMPINPageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IUserDialogs userDialogs, ICompanyWalletTransactionService companyWalletTransactionService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _authenticationService = authenticationService;
            _companyWalletTransactionService = companyWalletTransactionService;
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

        private string _mobilenumber;
        public string MobileNumber
        {
            get => _mobilenumber;
            set => SetProperty(ref _mobilenumber, value);
        }


        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set => SetProperty(ref _accountNumber, value);
        }


        private string _legalName;
        public string LegalName
        {
            get => _legalName;
            set => SetProperty(ref _legalName, value);
        }


        private decimal _paymentamount;
        public decimal PayAmount
        {
            get => _paymentamount;
            set => SetProperty(ref _paymentamount, value);
        }


        private decimal _returnamount;
        public decimal ReturnAmount
        {
            get => _returnamount;
            set => SetProperty(ref _returnamount, value);
        }


        private string _reason;
        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }


        private string _amount;
        public string RFT
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }


        private long? _companyLocationId;
        public long? CompanyLocationId
        {
            get => _companyLocationId;
            set => SetProperty(ref _companyLocationId, value);
        }

        public override void Prepare(dynamic confirmPaymentModel)
        {
            ReturnAmount = confirmPaymentModel.PayAmount;
            Reason = confirmPaymentModel.Reason;
            LegalName = confirmPaymentModel.LegalName;
        }

        private async Task ReturnRFTMutation()
        {
            try
            {
                var response = await _companyWalletTransactionService.ReturnPoints(ReturnAmount, Reason);
                _transactionId = response.TransactionId;
                _createdDateTimeUtc = response.CreatedDateTimeUtc;
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public IMvxAsyncCommand RFTMPINVerifyCommand => new MvxAsyncCommand(async () =>
        {
            try
            {
                if ((!string.IsNullOrWhiteSpace(Text1)) && (!string.IsNullOrWhiteSpace(Text2)) && (!string.IsNullOrWhiteSpace(Text3)) && (!string.IsNullOrWhiteSpace(Text4)))
                {
                    var paymentpin = Text1 + Text2 + Text3 + Text4;
                    var pinSaved = await SecureStorage.GetAsync("MPin");
                    if (paymentpin == pinSaved)
                    {
                        if (await _authenticationService.ValidateToken())
                        {
                            await ReturnRFTMutation();
                            await _navigationService.Navigate<RFTSuccessfulPageViewModel, dynamic>(new
                            {
                                PayAmount = ReturnAmount,
                                Reason = _reason,
                                LegalName = LegalName,
                                TransactionId = _transactionId,
                                CreatedDateTimeUtc = _createdDateTimeUtc,
                            });
                        }
                    }
                    else
                    {
                        _userDialogs.Toast("Invalid MPIN");
                    }
                }
                else
                {
                    _userDialogs.Toast("Please enter all four fields");
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}