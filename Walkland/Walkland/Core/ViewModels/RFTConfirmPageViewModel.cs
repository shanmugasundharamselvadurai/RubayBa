using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.ViewModels
{
    public class RFTConfirmPageViewModel : MvxViewModel<dynamic>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletService _companyWalletService;

        public RFTConfirmPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyWalletService companyWalletService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyWalletService = companyWalletService;
        }

        private string _transactionId;
        public string TransactionId
        {
            get { return _transactionId; }
            set
            {
                if (_transactionId != value)
                {
                    SetProperty(ref _transactionId, value);
                }
            }
        }

        private DateTime _transactionDateTime;
        public DateTime TransactionDateTime
        {
            get { return _transactionDateTime; }
            set
            {
                if (_transactionDateTime != value)
                {
                    SetProperty(ref _transactionDateTime, value);
                }
            }
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                if (_accountNumber != value)
                {
                    SetProperty(ref _accountNumber, value);
                }
            }
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
            get { return _legalName; }
            set
            {
                if (_legalName != value)
                {
                    SetProperty(ref _legalName, value);
                }
            }
        }

        private decimal _paymentamount;
        public decimal PayAmount
        {
            get { return _paymentamount; }
            set
            {
                if (_paymentamount != value)
                {
                    SetProperty(ref _paymentamount, value);
                }
            }
        }

        private string _reason;
        public string Reason
        {
            get { return _reason; }
            set
            {
                if (_reason != value)
                {
                    SetProperty(ref _reason, value);
                }
            }
        }

        private decimal _remainingamount;
        public decimal RemainingAmount
        {
            get => _remainingamount;
            set => SetProperty(ref _remainingamount, value);
        }

        private long? _companyLocationId;
        public long? CompanyLocationId
        {
            get { return _companyLocationId; }
            set
            {
                if (_companyLocationId != value)
                {
                    SetProperty(ref _companyLocationId, value);
                }
            }
        }

        public override void Prepare(dynamic confirmPaymentModel)
        {
            RemainingAmount = confirmPaymentModel.RemainingAmount;
            PayAmount = confirmPaymentModel.PayAmount;
            Reason = confirmPaymentModel.Reason;
            LegalName = confirmPaymentModel.LegalName;
        }

        public IMvxAsyncCommand RFTConfirmCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<RFTMPINPageViewModel, dynamic>(new
            {
                PayAmount = PayAmount,
                Reason = Reason,
                LegalName = LegalName,
            });
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}