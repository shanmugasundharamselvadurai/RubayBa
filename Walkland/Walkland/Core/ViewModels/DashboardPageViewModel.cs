using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Walkland.Core.ViewModels
{
    public class DashboardPageViewModel : MvxNavigationViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public DashboardPageViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs;
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }

        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

        private Task ShowInitialViewModels()
        {
            using (_userDialogs.Loading("Loading..."))
            {
                var tasks = new List<Task>
                {
                    NavigationService.Navigate<HomePageViewModel>(),                   
                    NavigationService.Navigate<CompanyWalletPageViewModel>(),
                    NavigationService.Navigate<ProfilePageViewModel>()
                };
                return Task.WhenAll(tasks);
            }
        }
    }
}