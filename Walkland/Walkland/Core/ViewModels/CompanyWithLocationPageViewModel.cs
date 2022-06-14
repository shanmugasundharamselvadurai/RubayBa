using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class CompanyWithLocationPageViewModel : MvxViewModel<CompanyLocation>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyProductService _companyProductService;
        private CompanyLocation _companyLocation;
        private readonly ICompanyOfferService _companyOfferService;
        private readonly ICompanyService _companyService;

        private string companyBanaerName { get; set; }
        private string companyLogo { get; set; }
        private decimal companyRating { get; set; }
        public long compId { get; set; }

        public CompanyWithLocationPageViewModel(IMvxNavigationService navigationService, ICompanyProductService companyProductService, IUserDialogs userDialogs, ICompanyOfferService companyOfferService,ICompanyService companyService)
        {
            _navigationService = navigationService;
            _companyProductService = companyProductService;
            _userDialogs = userDialogs;
            _companyOfferService = companyOfferService;
            _companyService = companyService;
            CompanyCategory = new Company();
        }

        private MvxObservableCollection<CompanyProduct> _productlist = new MvxObservableCollection<CompanyProduct>();
        public MvxObservableCollection<CompanyProduct> ProductList
        {
            get => _productlist;
            set => SetProperty(ref _productlist, value);
        }

        private MvxObservableCollection<CompanyOffer> _companyOffer = new MvxObservableCollection<CompanyOffer>();
        public MvxObservableCollection<CompanyOffer> CompanyOffer
        {
            get => _companyOffer;
            set => SetProperty(ref _companyOffer, value);
        }

        private CompanyProduct _category;
        public CompanyProduct CompanyProduct
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private Company _companyCategory;
        public Company CompanyCategory
        {
            get => _companyCategory;
            set => SetProperty(ref _companyCategory, value);
        }

        private CompanyLocation _nearbyCompany;
        public CompanyLocation NearbyCompany
        {
            get => _nearbyCompany;
            set => SetProperty(ref _nearbyCompany, value);
        }

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set => SetProperty(ref _brandName, value);
        }

        private string _companyCategoryName;
        public string CompanyCategoryName
        {
            get => _companyCategoryName;
            set => SetProperty(ref _companyCategoryName, value);
        }

        private string _logoStoragePath;
        public string LogoStoragePath
        {
            get => _logoStoragePath;
            set => SetProperty(ref _logoStoragePath, value);
        }

        private long _id;
        public long Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private long _companyId;
        public long CompanyId
        {
            get => _companyId;
            set => SetProperty(ref _companyId, value);
        }

        private decimal _rating;
        public decimal Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
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

        private bool _isVisbleCompanyOffer;
        public bool IsVisbleCompanyOffer
        {
            get => _isVisbleCompanyOffer;
            set => SetProperty(ref _isVisbleCompanyOffer, value);
        }


        public override void Prepare(CompanyLocation companyLocation)
        {
            if (string.IsNullOrEmpty(companyLocation.Company.Id.ToString()))
            {
                return;
            }
            else if (string.IsNullOrEmpty(companyLocation.Company.LogoStoragePath))
            {
                return;
            }
            else if (string.IsNullOrEmpty(companyLocation.Company.BrandName))
            {
                return;
            }
            else if (string.IsNullOrEmpty(companyLocation.Company.Rating.ToString()))
            {
                return;
            }
            else
            {
                CompanyId = companyLocation.Company.Id;
                LogoStoragePath = companyLocation.Company.LogoStoragePath;
                BrandName = companyLocation.Company.BrandName;
                Rating = companyLocation.Company.Rating;

            }
            _companyLocation = companyLocation;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            MobileNumber = _companyLocation.MobileNumber;
            City = _companyLocation.City;
            ContactPerson = _companyLocation.ContactPerson;
            AddressLine1 = _companyLocation.AddressLine1;
            AddressLine2 = _companyLocation.AddressLine2;
            Latitude = _companyLocation.Latitude;
            Longitude = _companyLocation.Longitude;
            PinCode = _companyLocation.PinCode;
            Landmark = _companyLocation.Landmark;

      
            var tasks = new List<Task>
            {
                LoadCompanyProduct(),
                LoadCompanyOffer(),
                LoadGetCompanyByCompanyCategory()
            };
            await Task.WhenAll(tasks);
        }

        private MvxObservableCollection<Company> _companylist = new MvxObservableCollection<Company>();
        public MvxObservableCollection<Company> CompanyList
        {
            get => _companylist;
            set => SetProperty(ref _companylist, value);
        }


        public IMvxAsyncCommand<CompanyProduct> ProductTappedCommand => new MvxAsyncCommand<CompanyProduct>(async (CompanyProduct item) =>
        {
            await _navigationService.Navigate<ProductDetailPageViewModel, CompanyProduct>(item);
        });

        public IMvxAsyncCommand NavigateToReviewPageCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<CompanyReviewPageViewModel, dynamic>(new 
            {
                Id = Id,
                BrandName = BrandName,
                Rating = Rating,
                LogoStoragePath = ImageSource.FromUri(new Uri(Constants.BannerImageURL + _companyLocation.Company.LogoStoragePath)),
            });
        });       
       
        //Open Location on Google Map
        public IMvxAsyncCommand NavigateToMap => new MvxAsyncCommand(async () =>
        {
            if (Latitude != null && Longitude != null)
            {
                var location = new Location(Convert.ToDouble(Latitude), Convert.ToDouble(Longitude));
                var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };
                await Map.OpenAsync(location, options);
            }
            else
            {
                _userDialogs.Toast("Location is not available");
            }
        });


        public IMvxAsyncCommand CompanyDetailPageCommand => new MvxAsyncCommand(async () =>
        {
            companyBanaerName = BrandName;
            companyLogo = LogoStoragePath;
            companyRating = Rating;
            compId = CompanyId;

            CompanyCategory.BrandName = companyBanaerName;
            CompanyCategory.LogoStoragePath = companyLogo;
            CompanyCategory.Rating = companyRating;
            CompanyCategory.Id = compId;

            if(CompanyCategory != null)
            {
                await _navigationService.Navigate<CompanyPageViewModel, Company>(CompanyCategory);
            }
            else
            {
                return;
            }

        });


        //Call Now Button Task
        public IMvxAsyncCommand PhoneCallClicked => new MvxAsyncCommand(() =>
        {
            try
            {
                if (MobileNumber != null && !string.IsNullOrEmpty(MobileNumber))
                {
                    PhoneDialer.Open(MobileNumber);
                }
                else
                {
                    _userDialogs.Toast("Phone Number is not available");
                }
            }
            catch (FeatureNotSupportedException)
            {
                _userDialogs.Toast(" Phone Dialer is not supported on this device.");
            }
            catch (Exception)
            {
                _userDialogs.Toast("Other error has occurred. ");
            }

          return Task.CompletedTask;
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        private async Task LoadCompanyProduct()
        {
            try
            {
                var companyCategoryProduct = await _companyProductService.GetProductByCompanyId(CompanyId);

                foreach (var productCategories in companyCategoryProduct)
                {
                    ProductList.Add(productCategories);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        //Load Company List
        private async Task LoadGetCompanyByCompanyCategory()
        {
            try
            {
                var companyCategory = await _companyService.GetCompanyByCompanyCategoryId(2);
                foreach (var companyCategories in companyCategory)
                {
                    CompanyList.Add(companyCategories);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                _ = e.StackTrace;
            }
        }


        private async Task LoadCompanyOffer()
        {
            try
            {
                var companyOffer = await _companyOfferService.GetOfferByCompanyId(CompanyId);

                if (companyOffer != null && companyOffer.Count > 0)
                {
                    foreach (var companyOffers in companyOffer)
                    {
                        IsVisbleCompanyOffer = true;
                        CompanyOffer.Add(companyOffers);
                    }
                }
                else
                {
                    IsVisbleCompanyOffer = false;
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                IsVisbleCompanyOffer = false;
            }
        }
    }
}