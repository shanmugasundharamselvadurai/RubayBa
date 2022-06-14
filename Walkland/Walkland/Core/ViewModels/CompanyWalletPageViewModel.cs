using Acr.UserDialogs;
using Walkland.Core.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.ViewModels
{
    public class CompanyWalletPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyWalletService _companyWalletService;
        private readonly ICompanyWalletTransactionService _companyWalletTransactionService;

        public CompanyWalletPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyWalletService companyWalletService, ICompanyWalletTransactionService companyWalletTransactionService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyWalletService = companyWalletService;
            _companyWalletTransactionService = companyWalletTransactionService;
        }

        private MvxObservableCollection<CompanyWalletTransaction> _companyWalletTransactions = new MvxObservableCollection<CompanyWalletTransaction>();
        public MvxObservableCollection<CompanyWalletTransaction> CompanyWalletTransactions
        {
            get => _companyWalletTransactions;
            set => SetProperty(ref _companyWalletTransactions, value);
        }

        private string _walletBalance;
        public string WalletBalance
        {
            get => _walletBalance;
            set => SetProperty(ref _walletBalance, value);
        }


        private MvxNotifyTask _refreshDataTask;
        public MvxNotifyTask RefreshDataTask
        {
            get => _refreshDataTask;
            private set => SetProperty(ref _refreshDataTask, value);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            await GetEmployeeWalletDetails();
            await LoadCompanyWalletTransaction();
        }

        public IMvxAsyncCommand PayCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<SendPageViewModel>();
        });
        
        private async Task GetEmployeeWalletDetails()
        {
            try
            {
                var userDetails = await _companyWalletService.GetWalletBalanceForCustomerUser();
                WalletBalance = userDetails.Amount.ToString("#0.00");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        private async Task LoadCompanyWalletTransaction()
        {
            try
            {
                var companyWalletTransactions = await _companyWalletTransactionService.GetCompanyEmployeeWalletTransactionServices();

                companyWalletTransactions.Sort((x, y) => y.CreatedDateTimeUtc.CompareTo(x.CreatedDateTimeUtc));

                CompanyWalletTransactions.Clear();

                foreach (var companyWalletTransaction in companyWalletTransactions)
                {
                    CompanyWalletTransactions.Add(companyWalletTransaction);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}