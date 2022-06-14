using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class RFTPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletService _companyWalletService;

        public RFTPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyWalletService companyWalletService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyWalletService = companyWalletService;      
        }

        private decimal _walletBalance;
        public decimal WalletBalance
        {
            get => _walletBalance;
            set => SetProperty(ref _walletBalance, value);
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set => SetProperty(ref _accountNumber, value);
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
            set => SetProperty(ref _companyLegalName, value);
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
                    SetProperty(ref _paymentamount, value);
                }
            }
        }

        private string _legalName;
        public string LegalName
        {
            get => _legalName;
            set => SetProperty(ref _legalName, value);
        }

        private decimal _remainingamount;
        public decimal RemainingAmount
        {
            get => _remainingamount;
            set => SetProperty(ref _remainingamount, value);
        }

        private string _reason = "";
        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            var tasks = new List<Task>
            {
                    LoadWalletBalance(),
                    LoadLegalName()
        };
            await Task.WhenAll(tasks);
        }

        private async Task LoadLegalName()
        {
            try
            {
                long companyId = long.Parse(await SecureStorage.GetAsync("CompanyId"));

                var legalname = await _companyWalletService.GetCompanyById(companyId);
                LegalName = legalname.LegalName;
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

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

        public IMvxAsyncCommand ReturnCommand => new MvxAsyncCommand(async () =>
        {
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
                using (_userDialogs.Loading("Loading..."))
                {
                    await LoadLegalName();
                    await _navigationService.Navigate<RFTConfirmPageViewModel, dynamic>(new
                    { 
                        RemainingAmount = RemainingAmount,
                        LegalName = LegalName,
                        PayAmount = PaymentAmount,
                        Reason = Reason,
                    });
                }
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}