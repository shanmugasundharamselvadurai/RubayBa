using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Services;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Walkland.UI.Contorls;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class HomePageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IAppBannerService _appBannerService;
        private readonly ICompanyCategoryService _companyCategoryService;
        private readonly ILatestDealService _latestDealService;
        private readonly ICompanyLocationService _companyLocationService;
        private readonly IAuthenticationService _authenticationService;
     
        public HomePageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs,
            IAppBannerService appBannerService, ICompanyCategoryService companyCategoryService, ILatestDealService latestDealService, ICompanyLocationService companyLocationService, IAuthenticationService Authentication)
        {
           
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _appBannerService = appBannerService;
            _companyCategoryService = companyCategoryService;
            _latestDealService = latestDealService;
            _companyLocationService = companyLocationService;
            _authenticationService = Authentication;


            var tasks = new List<Task>
            {
                LoadAppBannersAsync(),
                LoadCompanyCategoriesAsync(),
                LoadLatestDealsAsync(),
                LoadNearbyCompanyStores(location)
            };
            Task.WhenAll(tasks);
        }

        private async Task LoadNearbyCompanyStores(Location location)
        {
            try
            {
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    var nearbycompanies = await _companyLocationService.FindNearByCompanies(location.Latitude.ToString(), location.Longitude.ToString());

                    FiveNearbyCompanies.Clear();
                    foreach (var nearbycompanylocation in nearbycompanies)
                    {
                        FiveNearbyCompanies.Add(nearbycompanylocation);
                        LogoStoragePath = nearbycompanylocation.Company.LogoStoragePath;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        private ImageSource _logoStoragePath;
        public ImageSource LogoStoragePath
        {
            get => _logoStoragePath;
            set => SetProperty(ref _logoStoragePath, value);
        }
        //company location

        private int _carouselPosition;
        public int CarouselPosition
        {
            get => _carouselPosition;
            set => SetProperty(ref _carouselPosition, value);
        }

        private CompanyLocation _nearbyCompany;
        public CompanyLocation NearbyCompany
        {
            get => _nearbyCompany;
            set => SetProperty(ref _nearbyCompany, value);
        }

        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get => _mobileNumber;
            set => SetProperty(ref _mobileNumber, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _contactPerson;
        public string ContactPerson
        {
            get => _contactPerson;
            set => SetProperty(ref _contactPerson, value);
        }

        private string _addressLine1;
        public string AddressLine1
        {
            get => _addressLine1;
            set => SetProperty(ref _addressLine1, value);
        }

        private string _addressLine2;
        public string AddressLine2
        {
            get => _addressLine2;
            set => SetProperty(ref _addressLine2, value);
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private string _company;
        public string Company
        {
            get => _company;
            set => SetProperty(ref _company, value);
        }

        private string _landmark;
        public string Landmark
        {
            get => _landmark;
            set => SetProperty(ref _landmark, value);
        }

        private string _pinCode;
        public string PinCode
        {
            get => _pinCode;
            set => SetProperty(ref _pinCode, value);
        }

        private string _adminArea;
        public string AdminArea
        {
            get => _adminArea;
            set => SetProperty(ref _adminArea, value);
        }

        private string _subAdminArea;
        public string SubAdminArea
        {
            get => _subAdminArea;
            set => SetProperty(ref _subAdminArea, value);
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        private string _locality;
        public string Locality
        {
            get => _locality;
            set => SetProperty(ref _locality, value);
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private bool _activityIndicator;
        public bool ActivityIndicator
        {
            get => _activityIndicator;
            set => SetProperty(ref _activityIndicator, value);
        }
        
        private string _subLocality;
        public string SubLocality
        {
            get => _subLocality;
            set => SetProperty(ref _subLocality, value);
        }

        private MvxObservableCollection<CompanyLocation> _fiveNearbyCompanies = new MvxObservableCollection<CompanyLocation>();
        public MvxObservableCollection<CompanyLocation> FiveNearbyCompanies
        {
            get => _fiveNearbyCompanies;
            set => SetProperty(ref _fiveNearbyCompanies, value);
        }

        private MvxObservableCollection<LatestDeal> _latestDeals = new MvxObservableCollection<LatestDeal>();
        public MvxObservableCollection<LatestDeal> LatestDeals
        {
            get => _latestDeals;
            set => SetProperty(ref _latestDeals, value);
        }

        private MvxObservableCollection<AppBanner> _appBanners = new MvxObservableCollection<AppBanner>();
        public MvxObservableCollection<AppBanner> AppBanners
        {
            get => _appBanners;
            set => SetProperty(ref _appBanners, value);
        }

        private MvxObservableCollection<CompanyCategory> _companyCategories = new MvxObservableCollection<CompanyCategory>();
        public MvxObservableCollection<CompanyCategory> CompanyCategories
        {
            get => _companyCategories;
            set => SetProperty(ref _companyCategories, value);
        }

        private MvxObservableCollection<CompanyOffer> _companyOffers = new MvxObservableCollection<CompanyOffer>();
        public MvxObservableCollection<CompanyOffer> CompanyOffers
        {
            get => _companyOffers;
            set => SetProperty(ref _companyOffers, value);
        }

        private Location location;
        public override async Task Initialize()
        {
            await base.Initialize();

            Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            {
                if (AppBanners.Count > 0)
                {
                    CarouselPosition = (CarouselPosition + 1) % AppBanners.Count;
                    return true;
                }
                else
                {
                    return false;
                }
            }));


            location = await Geolocation.GetLastKnownLocationAsync();
            await UpdateUserLocation(location);
 
            var tasks = new List<Task>
            {
                LoadAppBannersAsync(),
                LoadCompanyCategoriesAsync(),
                LoadLatestDealsAsync(),
                LoadNearbyCompanyStores(location)
            };
            await Task.WhenAll(tasks);
        }

        //Go To Banner Detail Page
        public IMvxAsyncCommand<AppBanner> BannerDetailItemPageCommand => new MvxAsyncCommand<AppBanner>(async (AppBanner item) =>
        {
            item = AppBanners.ToList()[CarouselPosition];
            await _navigationService.Navigate<BannerPageViewModel, AppBanner>(item);
        });

        //
        public IMvxAsyncCommand RefreshCommand => new MvxAsyncCommand(async () =>
        {
            IsRefreshing = true;
            var tasks = new List<Task>
            {
                LoadAppBannersAsync(),
                LoadCompanyCategoriesAsync(),
                LoadLatestDealsAsync(),
                LoadNearbyCompanyStores(location)
            };
            _ = Task.WhenAll(tasks);
            IsRefreshing = false;
        });

        public IMvxAsyncCommand ImageLatestFilterCommand => new MvxAsyncCommand(async () =>
        {
        //    PopupView popup = new PopupView
        //    {
        //        isNearme = false,
        //        isADeals = true,
        //        isSearch = false,
        //        isCategory =false,
        //    };
        //    await PopupNavigation.Instance.PushAsync(popup);
        });

        public IMvxAsyncCommand ImageLatestNearMeCommand => new MvxAsyncCommand(async () =>
        {
            //PopupView popup = new PopupView
            //{
            //    isNearme = true,
            //    isADeals = false,
            //    isSearch = false,
            //    isCategory = false,
            //};
            //await PopupNavigation.Instance.PushAsync(popup);
        });

        //Select Category from Category carousel
        //public IMvxAsyncCommand<CompanyCategory> CompanyCategoryItemTappedCommand => new MvxAsyncCommand<CompanyCategory>(async (CompanyCategory item) =>
        //{
        //    var applicationID = await SecureStorage.GetAsync("ApplicationUserId");
        //    await _authenticationService.InsertUserCategory(new UserCategoryRequestDto()
        //    {
        //        ApplicationUserId = Convert.ToInt32(applicationID),
        //        CategoryId = item.Id
        //    });

        //    _ = await _navigationService.Navigate<CompanyCategoryPageViewModel, CompanyCategory>(item);
        //});

        //Select Category from Category carousel
        public IMvxAsyncCommand<CompanyCategory> CompanyCategoryItemTappedCommand => new MvxAsyncCommand<CompanyCategory>(async (CompanyCategory item) =>
        {
            var applicationID = await SecureStorage.GetAsync("ApplicationUserId");
            await _authenticationService.InsertUserCategory(new UserCategoryRequestDto()
            {
                ApplicationUserId = Convert.ToInt32(applicationID),
                CategoryId = item.Id
            });

            _ = await _navigationService.Navigate<CompanySubCategoryPageViewModel, CompanyCategory>(item);
        });


        //Go To Latest Deal Detail Page
        public IMvxAsyncCommand<LatestDeal> LatestDealItemTappedCommand => new MvxAsyncCommand<LatestDeal>(async (LatestDeal item) =>
        {
            await _navigationService.Navigate<LatestDealPageViewModel, LatestDeal>(item);
        });

        //Near Me Location Tap Command
        public IMvxAsyncCommand NearbyLocationTappedCommand => new MvxAsyncCommand(async () =>
        {
            var result = await _navigationService.Navigate<NearByLocationPageViewModel, Location>();

            if (result != null)
            {
                location = result;
            }
            else
            {
                location = await Geolocation.GetLastKnownLocationAsync();
            }

            using (_userDialogs.Loading("Loading..."))
            {
                if (location != null)
                {
                    await UpdateUserLocation(location);
                    await LoadNearbyCompanyStores(location);
                }
                else
                {
                    return;
                }

            }
        });

        private async Task UpdateUserLocation(Location location)
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemarkDetails = placemarks?.FirstOrDefault();
            if (placemarkDetails != null)
            {
                AdminArea = placemarkDetails.AdminArea;
                SubLocality = placemarkDetails.SubLocality;
                Locality = placemarkDetails.Locality;
                SubAdminArea = placemarkDetails.SubAdminArea;
            }
        }

        //Go to Search Page
        public IMvxAsyncCommand SearchCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<SearchPageViewModel>();
        });

        //Go To Nearby Company Store Command Page
        public IMvxAsyncCommand<CompanyLocation> NearbyCompanyStoreCommand => new MvxAsyncCommand<CompanyLocation>(async (CompanyLocation item) =>
        {
            await _navigationService.Navigate<CompanyWithLocationPageViewModel, CompanyLocation>(item);
        });

        //Go To Notification Page      
        public IMvxAsyncCommand NotificationPageCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<NotificationPageViewModel>();
        });

        //App Banner Method
        private async Task LoadAppBannersAsync()
        {
            try
            {
                var bannerImages = await _appBannerService.GetAppBanners();
                AppBanners.Clear();
                foreach (var bannerImage in bannerImages)
                {
                    AppBanners.Add(bannerImage);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        //Company Category Method
        private async Task LoadCompanyCategoriesAsync()
        {
            try
            {
                var companyCategory = await _companyCategoryService.GetCompanyCategories();
                CompanyCategories.Clear();
                foreach (var companyCategories in companyCategory)
                {
                    CompanyCategories.Add(companyCategories);
                }

            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public async void LoadLatestDeals()
        {
         //  await LoadLatestDealsAsync();
        }

        //Latest Deal Method
        public async Task LoadLatestDealsAsync()
        {
            try
            {
                var latestDeals = await _latestDealService.GetLatestDeals();
                LatestDeals.Clear();
                foreach (var latestDealImage in latestDeals)
                {
                    LatestDeals.Add(latestDealImage);
                }
                MessagingCenter.Send<HomePageViewModel>(this,"hi");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}