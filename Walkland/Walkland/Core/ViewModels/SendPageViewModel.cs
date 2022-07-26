using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.QrCode;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Walkland.Core;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Walkland.Core.ViewModels;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class SendPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletService _companyWalletService;
        private readonly IMvxQrCode _mvxQrCode;

        public SendPageViewModel(IMvxQrCode mvxQrCode, IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyWalletService companyWalletService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyWalletService = companyWalletService;
            _mvxQrCode = mvxQrCode;
        }

        private decimal _walletBalance;
        public decimal WalletBalance
        {
            get => _walletBalance;
            set
            {
                _walletBalance = value;
                RaisePropertyChanged(() => WalletBalance);
            }
        }

        public string WalletBalanceString
        {
            get
            {
                return _walletBalance.ToString("N2");
            }
        }

        private string _companyLegalName;
        public string CompanyLegalName
        {
            get => _companyLegalName;
            set
            {
                _companyLegalName = value;
                RaisePropertyChanged(() => CompanyLegalName);
            }
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                RaisePropertyChanged(() => AccountNumber);
            }
        }
        

        private decimal _paymentamount;
        public decimal PaymentAmount
        {
            get => _paymentamount;
            set
            {
                if (_paymentamount != value)
                {
                    _paymentamount = value;
                    RemainingAmount = WalletBalance - value;
                    RaisePropertyChanged(() => PaymentAmount);
                }

            }
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

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set
            {
                _brandName = value;
                RaisePropertyChanged(() => BrandName);
            }
        }

        private string _reason = "";
        public string Reason
        {
            get => _reason;
            set
            {
                _reason = value;
                RaisePropertyChanged(() => Reason);
            }
        }

        private long? _companyLocationId;
        public long? CompanyLocationId
        {
            get => _companyLocationId;
            set
            {
                _companyLocationId = value;
                RaisePropertyChanged(() => CompanyLocationId);
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

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged(() => Result);
            }
        }


        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set
            {
                _mobile = value;
                RaisePropertyChanged(() => Mobile);
            }
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

        private bool _IsValueMacdonald =true;
        public bool IsValueMacdonald
        {
            get => _IsValueMacdonald;
            set => SetProperty(ref _IsValueMacdonald, value);
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

        public override async Task Initialize()
        {
            using (_userDialogs.Loading("Loading..."))
            {
                await base.Initialize();

                await LoadWalletBalance();
            }
        }

        public IMvxAsyncCommand ScanCommand => new MvxAsyncCommand(async () =>
        {

            var scanResult = await _mvxQrCode.Scan("Hold the camera up to the barcode\nAbout 15 cm away",
                                    "The barcode will be automatically scanned",
                                    "Your camera doesn't support barcode scanning");

           // scanResult.ScanStatus = ScanStatus.Success;

            switch (scanResult.ScanStatus)
            {
                case ScanStatus.Success:
                    Result = scanResult.Result.Text; 
                    var str = Result;
                    var b = Result.Split(',');

                    if (b.Length > 1)
                    {
                        AccountNumber = b[0].ToString();
                        CompanyLocationId = long.Parse(b[1]);
                    }
                    else
                    {
                        try
                        {
                            string patternText = "mcdonalds.+[0-9]+@hdfcbank";
                            string PatternPaytm = "paytm-+[0-9]+@paytm";
                            Regex ReMcdo = new Regex(patternText);
                            Regex RePaytm = new Regex(PatternPaytm);

                            if (ReMcdo.IsMatch(b[0]))
                            {

                                var legalname = await _companyWalletService.GetCompanyByQRV1(b[0].ToString());
                                if (!string.IsNullOrEmpty(legalname.ToString()))
                                {
                                    IsValueMacdonald = false;
                                    var result = Result.Split('&');
                                    var amout = result[3].Split('=');
                                    AccountNumber = legalname.AccountNumber;
                                    PaymentAmount = Convert.ToDecimal(amout[1].ToString());
                                    var compLocationID = result[2].Split('=');
                                    TransNo = Convert.ToString(compLocationID[1].ToString());
                                }
                                else
                                {
                                    return;
                                }

                            }
                            else if (RePaytm.IsMatch(b[0]))
                            {

                                var legalname = await _companyWalletService.GetCompanyByQRV1(b[0].ToString());
                                if (!string.IsNullOrEmpty(legalname.ToString()))
                                {
                                    IsValueMacdonald = false;
                                    var result = Result.Split('&');
                                    var amout = result[4].Split('=');
                                    AccountNumber = legalname.AccountNumber; //4198775948
                                    PaymentAmount =  Convert.ToDecimal(amout[1].ToString());
                                    var compLocationID = result[3].Split('=');
                                    TransNo = Convert.ToString(compLocationID[1].ToString());
                                }
                                else
                                {
                                    return;
                                }

                            }
                            else
                            {

                                var legalname = await _companyWalletService.GetCompanyByQR(b[0].ToString());
                                if (!string.IsNullOrEmpty(legalname.ToString()))
                                {
                                    AccountNumber = legalname.AccountNumber;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _ = e.StackTrace;
                        }
                    }

                    break;
                case ScanStatus.Canceled:
                    Result = "Scan canceled";

                    break;
                case ScanStatus.Error:
                    Result = scanResult.Exception.Message;
                    break;
                default:
                    throw new NotImplementedException();
            }
        });

        public IMvxAsyncCommand PayCommand => new MvxAsyncCommand(async () =>
        {
            //await LoadRupayBachaoOffer();
            if (PaymentAmount > WalletBalance)
            {
                _userDialogs.Toast("Enter amount less than Wallet balance");
            }
            else if (PaymentAmount <= 0)
            {
                _userDialogs.Toast("Enter amount greater than zero");
            }
            else
            {
             //   await LoadLegalName();
                //if (CompanyLegalName != null)
                //{
                await _navigationService.Navigate<PaymentConfirmPageViewModel, dynamic>(new
                {
                    AccountNumber = AccountNumber,
                    RemainingAmount = RemainingAmount,
                    WalletBalance = WalletBalance,
                    PayAmount = PaymentAmount,
                    Reason = Reason,
                    CompanyLocationId = CompanyLocationId,
                    TransNo = TransNo,
                    LegalName = CompanyLegalName,
                });
                //}
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        private async Task LoadWalletBalance()
        {
            try
            {
                var userDetails = await _companyWalletService.GetWalletBalanceForCustomerUser();
                WalletBalance = userDetails.Amount;
                RemainingAmount = userDetails.Amount;
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        private async Task LoadLegalName()
        {
            try
            {
               //  when 0 index value 
                var legalname = await _companyWalletService.GetCompanyByAccountNumber(AccountNumber);
                CompanyLegalName = legalname.LegalName;
                BrandName = legalname.BrandName;
                var account = legalname.AccountNumber;
               // var locationID = legalname.com
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        private async Task LoadRupayBachaoOffer()
        {
            try
            {
                var UserID = SecureStorage.GetAsync("ApplicationUserId").Id;
                var legalname = await _companyWalletService.GetCompanyByAccountNumber(AccountNumber);
                PlatformRequestDto objPlatformRequestDto = new PlatformRequestDto();
                objPlatformRequestDto.CompanyID = legalname.Id;
                objPlatformRequestDto.UserID = UserID;
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
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

    }
}