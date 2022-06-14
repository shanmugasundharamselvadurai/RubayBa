using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Walkland.Core.ViewModels
{
    public class NearByLocationPageViewModel : MvxViewModelResult<Location>
    {
        private readonly IMvxNavigationService _navigationService;

        public NearByLocationPageViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public async Task SendDataToHomePage()
        {
            await _navigationService.Close(this, new Location
            {
                Latitude = Latitude,
                Longitude = Longitude
            });
           
            await Task.FromResult(false);
        }

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}