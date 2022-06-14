using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.ViewModels
{
    public class NotificationPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly INotificationService _NotificationService;
        private readonly ICompanyWalletTransactionService _companyWalletTransation;

        public NotificationPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, INotificationService NotificationService, ICompanyWalletTransactionService companyWalletTransation)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _NotificationService = NotificationService;
            _companyWalletTransation = companyWalletTransation;
        }

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        private MvxObservableCollection<CompanyWalletTransaction> _CompanyWaletTransaction = new MvxObservableCollection<CompanyWalletTransaction>();
        public MvxObservableCollection<CompanyWalletTransaction> CompanyWaletTransaction
        {
            get => _CompanyWaletTransaction;
            set => SetProperty(ref _CompanyWaletTransaction, value);
        }

        private MvxObservableCollection<Notification> _NotificationOffer = new MvxObservableCollection<Notification>();
        public MvxObservableCollection<Notification> NotificationOffer
        {
            get => _NotificationOffer;
            set => SetProperty(ref _NotificationOffer, value);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            var tasks = new List<Task>
            {
                LoadRupayNotiBachaoOffer(),
                LoadRupaywalleBachaTransation()
            };
            await Task.WhenAll(tasks);
        }

        private async Task LoadRupayNotiBachaoOffer()
        {
            try
            {
                var rupayBachos = await _NotificationService.GetNotification();

                foreach (var companyOffers in rupayBachos)
                {
                    NotificationOffer.Add(companyOffers);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        private async Task LoadRupaywalleBachaTransation()
        {
            try
            {
                var rupayBachos = await _companyWalletTransation.GetCompanyEmployeeWalletTransactionServices();

                rupayBachos.Sort((x, y) => y.CreatedDateTimeUtc.CompareTo(x.CreatedDateTimeUtc));

                foreach (var companyOffers in rupayBachos)
                {
                    System.Console.WriteLine("Today Date time" + DateTime.Today);
                    System.Console.WriteLine(" Server date time" + companyOffers.CreatedDateTimeUtc);

                    if (DateTime.Today.Date.Equals(companyOffers.CreatedDateTimeUtc.Date) == true)
                    {
                        CompanyWaletTransaction.Add(companyOffers);
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
    }
}