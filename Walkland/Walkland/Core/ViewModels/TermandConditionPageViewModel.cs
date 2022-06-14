using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Walkland.Core.ViewModels
{
    public class TermandConditionPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public TermandConditionPageViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}