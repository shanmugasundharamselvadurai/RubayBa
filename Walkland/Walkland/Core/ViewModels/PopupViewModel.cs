using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.ViewModels
{

    public class PopupViewModel: MvxViewModel
    {
        private readonly ILatestDealService _latestDealService;
       
        public PopupViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ILatestDealService latestDealService)
        {
            _latestDealService = latestDealService;
        }

        public PopupViewModel()
        {

        }

        public IMvxAsyncCommand basedonLocationCommand => new MvxAsyncCommand(async () =>
        {
        });

        private MvxObservableCollection<LatestDeal> _latestDeals = new MvxObservableCollection<LatestDeal>();
        public MvxObservableCollection<LatestDeal> LatestDeals
        {
            get => _latestDeals;
            set => SetProperty(ref _latestDeals, value);
        }

        public async Task ssAsync()
        {
            System.Console.WriteLine("hellow");
            try
            {
                var latestDeals = await _latestDealService.GetLatestDeals();
                LatestDeals.Clear();
                foreach (var latestDealImage in latestDeals)
                {
                    LatestDeals.Add(latestDealImage);
                }
                //MessagingCenter.Send<HomePageViewModel>(this, "hi");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                System.Console.WriteLine(e.Message);
            }
        }
      }
}
