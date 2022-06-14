using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Walkland.Core;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Walkland.Core.ViewModels;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class PaymentConfirmPageViewModel : MvxViewModel<dynamic>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletService _companyWalletService;
        private readonly ICompanyWalletTransactionService _companyWalletTransactionService;

        public PaymentConfirmPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyWalletService companyWalletService, ICompanyWalletTransactionService companyWalletTransactionService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyWalletService = companyWalletService;
            _companyWalletTransactionService = companyWalletTransactionService;
        }

        private string _transactionId;
        public string TransactionId
        {
            get => _transactionId;
            set => SetProperty(ref _transactionId, value);
        }

        private DateTime _transactionDateTime;
        public DateTime TransactionDateTime
        {
            get => _transactionDateTime;
            set => SetProperty(ref _transactionDateTime, value);
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set => SetProperty(ref _accountNumber, value);
        }

        private decimal _walletBalance;
        public decimal WalletBalance
        {
            get => _walletBalance;
            set => SetProperty(ref _walletBalance, value);
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

        private decimal _remainingamount;
        public decimal RemainingAmount
        {
            get => _remainingamount;
            set
            {
                _remainingamount = value;
                RaisePropertyChanged(() => RemainingAmount);
            }
        }

        private string _rbOffer;
        public string RBOffer
        {
            get => _rbOffer;
            set => SetProperty(ref _rbOffer, value);
        }


        private bool _isCheck;
        public bool IsCheck
        {
            get => _isCheck;
            set => SetProperty(ref _isCheck, value);
        }

        private bool _isGetOffer;
        public bool IsGetOffer
        {
            get => _isGetOffer;
            set => SetProperty(ref _isGetOffer, value);
        }

        private long _platformOfferId;
        public long PlatformOfferId
        {
            get => _platformOfferId;
            set
            {
                _platformOfferId = value;
                RaisePropertyChanged(() => PlatformOfferId);
            }
        }

        private string _platformOfferName;
        public string PlatformOfferName
        {
            get => _platformOfferName;
            set
            {
                _platformOfferName = value;
                RaisePropertyChanged(() => PlatformOfferName);
            }
        }

        private int _platformOfferPerUserLimit;
        public int PlatformOfferPerUserLimit
        {
            get => _platformOfferPerUserLimit;
            set
            {
                _platformOfferPerUserLimit = value;
                RaisePropertyChanged(() => PlatformOfferPerUserLimit);
            }
        }

        private string _platformOfferType;
        public string PlatformOfferType
        {
            get => _platformOfferType;
            set
            {
                _platformOfferType = value;
                RaisePropertyChanged(() => PlatformOfferType);
            }
        }

        private decimal _platformOfferPercentageDiscount;
        public decimal PlatformOfferPercentageDiscount
        {
            get => _platformOfferPercentageDiscount;
            set
            {
                _platformOfferPercentageDiscount = value;
                RaisePropertyChanged(() => PlatformOfferPercentageDiscount);
            }
        }

        private decimal _platformOfferFixedDiscountAmount;
        public decimal PlatformOfferFixedDiscountAmount
        {
            get => _platformOfferFixedDiscountAmount;
            set
            {
                _platformOfferFixedDiscountAmount = value;
                RaisePropertyChanged(() => PlatformOfferFixedDiscountAmount);
            }
        }

        private decimal _platformOfferMaximumDiscountAmount;
        public decimal PlatformOfferMaximumDiscountAmount
        {
            get => _platformOfferMaximumDiscountAmount;
            set
            {
                _platformOfferMaximumDiscountAmount = value;
                RaisePropertyChanged(() => PlatformOfferMaximumDiscountAmount);
            }
        }

        private decimal _platformOfferMinimumTransactionAmount;
        public decimal PlatformOfferMinimumTransactionAmount
        {
            get => _platformOfferMinimumTransactionAmount;
            set
            {
                _platformOfferMinimumTransactionAmount = value;
                RaisePropertyChanged(() => PlatformOfferMinimumTransactionAmount);
            }
        }

        private decimal _platformOfferDiscount;
        public decimal PlatformOfferDiscount
        {
            get => _platformOfferDiscount;
            set
            {
                _platformOfferDiscount = value;
                RaisePropertyChanged(() => PlatformOfferDiscount);
            }
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

        public override void Prepare(dynamic confirmPaymentModel)
        {
            AccountNumber = confirmPaymentModel.AccountNumber;
            RemainingAmount = confirmPaymentModel.RemainingAmount;
            WalletBalance = confirmPaymentModel.WalletBalance;
            PayAmount = confirmPaymentModel.PayAmount;
            Reason = confirmPaymentModel.Reason;
            TransNo = confirmPaymentModel.TransNo;
            LegalName = confirmPaymentModel.LegalName;
            CompanyLocationId = confirmPaymentModel.CompanyLocationId;

        }

        public override async Task Initialize()
        {
            using (_userDialogs.Loading("Loading..."))
            {
                await base.Initialize();

                await LoadRupayBachaoOffer();
            }
        }

        private async Task LoadRupayBachaoOffer()
        {
            try
            {
                var UserID = await SecureStorage.GetAsync("ApplicationUserId");
                //var UserID = SecureStorage.GetAsync("ApplicationUserId").Id;
                var legalname = await _companyWalletService.GetCompanyByAccountNumber(AccountNumber);
                PlatformRequestDto objPlatformRequestDto = new PlatformRequestDto();
                objPlatformRequestDto.CompanyID = legalname.Id;
                objPlatformRequestDto.UserID = Convert.ToInt32(UserID);
                var response = await CoreApp.PublicHttpClient.PostAsync(Constants.BaseURL + "/platformOffer/GetPlatformOffer", new StringContent(JsonConvert.SerializeObject(objPlatformRequestDto), Encoding.UTF8, "application/json"));
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var Rdata = JsonConvert.DeserializeObject<PlatformOffer>(jsonString);

                    PlatformOfferId = Rdata.Id;
                    PlatformOfferName = Rdata.Name;
                    PlatformOfferPerUserLimit = Rdata.PerUserLimit;
                    PlatformOfferType = Rdata.Type;
                    PlatformOfferPercentageDiscount = Rdata.PercentageDiscount;
                    PlatformOfferFixedDiscountAmount = Rdata.FixedDiscountAmount;
                    PlatformOfferMaximumDiscountAmount = Rdata.MaximumDiscountAmount;
                    PlatformOfferMinimumTransactionAmount = Rdata.MinimumTransactionAmount;
                    IsCheck = true;
                    var symbol = "";
                    decimal Dis = 0;
                    if (PayAmount > Rdata.MinimumTransactionAmount)
                    {
                        if (PlatformOfferType == "Percentage")
                        {
                            symbol = "%";
                            Dis = PlatformOfferPercentageDiscount;
                            PlatformOfferDiscount = PlatformOfferPercentageDiscount;
                        }
                        else
                        {
                            Dis = PlatformOfferFixedDiscountAmount;
                            PlatformOfferDiscount = PlatformOfferFixedDiscountAmount;
                        }
                        RBOffer = "Rupay Bachao Offer " + Dis + symbol + " " + "discount";
                    }
                    else
                    {
                        IsCheck = false;
                        RBOffer = "";
                    }
                }
                else
                {
                    IsCheck = false;
                    RBOffer = "";
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public IMvxAsyncCommand ConfirmCommand => new MvxAsyncCommand(async () =>
        {
            var availablity = await CrossFingerprint.Current.IsAvailableAsync();

            if (availablity == false)
            {
                await _navigationService.Navigate<PaymentMPINPageViewModel, dynamic>(new
                {
                    AccountNumber = AccountNumber,
                    WalletBalance = WalletBalance,
                    PayAmount = PayAmount,
                    Reason = Reason,
                    LegalName = LegalName,
                    CompanyLocationId = CompanyLocationId,
                    TransNo = TransNo,
                    IsGetOffer = IsGetOffer,
                    PlatformOfferType = PlatformOfferType,
                    PlatformOfferDiscount = PlatformOfferDiscount,
                    PlatformOfferId = PlatformOfferId
                });
               // _userDialogs.Toast("No biomertics available");

            }
            else if (availablity == true)
            {
               var authResult = await CrossFingerprint.Current.AuthenticateAsync(new
                    AuthenticationRequestConfiguration("Rupaybachos", "I would like to use your biometrics"));

                if (authResult.Authenticated)
                {
                    // isVisible = false;

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
                            else if (PlatformOfferType == "Percentage")
                            {
                                ActAmount = PayAmount;
                                disAmt = PayAmount * PlatformOfferDiscount / 100;
                                //PayAmount = PayAmount - disAmt;
                            }
                        }

                        var response = await _companyWalletTransactionService.SendPoints
                            (AccountNumber, PayAmount, Reason, CompanyLocationId, TransNo);
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
                                TransNo = TransNo,
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
            else
            {
                return;
            }

        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}