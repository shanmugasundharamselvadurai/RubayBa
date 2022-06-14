using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class PaymentMPINPageViewModel : MvxViewModel<dynamic>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletTransactionService _companyWalletTransactionService;

        public PaymentMPINPageViewModel(IMvxNavigationService navigationService, IAuthenticationService authenticationService, IUserDialogs userDialogs, ICompanyWalletTransactionService companyWalletTransactionService)
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

        private string _transactionId;
        public string TransactionId
        {
            get => _transactionId;
            set => SetProperty(ref _transactionId, value);
        }

        private DateTime _transactionDateTime;
        public DateTime CreatedDateTimeUtc
        {
            get => _transactionDateTime;
            set => SetProperty(ref _transactionDateTime, value);
        }

        private bool _isGetOffer;
        public bool IsGetOffer
        {
            get => _isGetOffer;
            set => SetProperty(ref _isGetOffer, value);
        }

        private string _platformOfferType;
        public string PlatformOfferType
        {
            get => _platformOfferType;
            set => SetProperty(ref _platformOfferType, value);
        }

        private decimal _platformOfferDiscount;
        public decimal PlatformOfferDiscount
        {
            get => _platformOfferDiscount;
            set => SetProperty(ref _platformOfferDiscount, value);
        }

        private long _platformOfferId;
        public long PlatformOfferId
        {
            get => _platformOfferId;
            set => SetProperty(ref _platformOfferId, value);
        }


        private string _transNo;
        public string TransNo
        {
            get => _transNo;
            set
            {
                _transNo = value;
                RaisePropertyChanged(() => TransNo);
            }
        }

        public override void Prepare(dynamic companyWalletTransaction)
        {
            AccountNumber = companyWalletTransaction.AccountNumber;
            PayAmount = companyWalletTransaction.PayAmount;
            Reason = companyWalletTransaction.Reason;
            LegalName = companyWalletTransaction.LegalName;
            CompanyLocationId = companyWalletTransaction.CompanyLocationId;

            IsGetOffer = companyWalletTransaction.IsGetOffer;
            PlatformOfferType = companyWalletTransaction.PlatformOfferType;
            PlatformOfferDiscount = companyWalletTransaction.PlatformOfferDiscount;
            PlatformOfferId = companyWalletTransaction.PlatformOfferId;
        }

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        public IMvxAsyncCommand PaymentMPINVerifyCommand => new MvxAsyncCommand(async () =>
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
                            using (_userDialogs.Loading("Loading..."))
                            {
                                await LoadPaymentMutation();
                            }
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
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        });     

        private async Task LoadPaymentMutation()
        {
            try
            {
                decimal disAmt = 0;
                decimal ActAmount = 0;
                if (IsGetOffer)
                {
                    if (PlatformOfferType == "Fixed")
                    {
                        ActAmount = PayAmount;
                        disAmt = PlatformOfferDiscount;
                        //PayAmount = PayAmount - disAmt;
                    }
                    else if(PlatformOfferType == "Percentage")
                    {
                        ActAmount = PayAmount;
                        disAmt = PayAmount * PlatformOfferDiscount / 100;
                        //PayAmount = PayAmount - disAmt;
                    }
                }
                
                var response = await _companyWalletTransactionService.SendPoints
                    (AccountNumber, PayAmount, Reason, CompanyLocationId,TransNo);
                _transactionId = response.TransactionId;
                _transactionDateTime = response.CreatedDateTimeUtc;

                if (response != null)
                {
                    if (IsGetOffer)
                    {
                        var ApplicationUserID = await SecureStorage.GetAsync("ApplicationUserId");
                        OfferRequestDto objOfferRequestDto = new OfferRequestDto();
                        objOfferRequestDto.CompanyWalletNo = AccountNumber;
                        objOfferRequestDto.PlatformOfferID = PlatformOfferId;
                        objOfferRequestDto.ApplicationUserID = Convert.ToInt32(ApplicationUserID);
                        objOfferRequestDto.DiscountAmount = disAmt;
                        objOfferRequestDto.CompanyLocationId = Convert.ToInt32(CompanyLocationId);
                        objOfferRequestDto.PayAmount = PayAmount;
                        var res = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/platformOffer/UpdatePlatformOffer", new StringContent(JsonConvert.SerializeObject(objOfferRequestDto), Encoding.UTF8, "application/json"));
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(CompanyLocationId)))
                    {
                        var ApplicationUserID = await SecureStorage.GetAsync("ApplicationUserId");
                        OfferRequestDto objOfferRequestDto = new OfferRequestDto();
                        objOfferRequestDto.CompanyWalletNo = AccountNumber;
                        objOfferRequestDto.PlatformOfferID = PlatformOfferId;
                        objOfferRequestDto.ApplicationUserID = Convert.ToInt32(ApplicationUserID);
                        objOfferRequestDto.DiscountAmount = disAmt;
                        objOfferRequestDto.CompanyLocationId = Convert.ToInt32(CompanyLocationId);
                        objOfferRequestDto.PayAmount = PayAmount;
                        var res = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/platformOffer/SendNotificationToLocation", new StringContent(JsonConvert.SerializeObject(objOfferRequestDto), Encoding.UTF8, "application/json"));
                    }

                        await _navigationService.Navigate<PaymentSuccessfulViewModel, dynamic>(new
                    {
                        AccountNumber = AccountNumber,
                        PayAmount = PayAmount,
                        Reason = Reason,
                        LegalName = LegalName,
                        TransactionId = _transactionId,
                        CreatedDateTimeUtc = _transactionDateTime,
                    });
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}