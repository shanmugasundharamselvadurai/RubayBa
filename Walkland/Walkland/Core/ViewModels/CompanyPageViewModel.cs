using Acr.UserDialogs;
using Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class CompanyPageViewModel : MvxViewModel<Company>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyProductService _companyProductService;
        private readonly ICompanyOfferService _companyOfferService;
        private readonly ICompanyLocationService _companyLocationService;

        private Company _company;
       // public ICommand CompanyTappedCommand { private set; get; }

        public CompanyPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs,
            ICompanyProductService companyProductService, ICompanyOfferService companyOfferService, ICompanyLocationService companyLocationService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyProductService = companyProductService;
            _companyOfferService = companyOfferService;
            _companyLocationService = companyLocationService;
        }

        private CompanyLocation _nearbyCompany;
        public CompanyLocation NearbyCompany
        {
            get => _nearbyCompany;
            set => SetProperty(ref _nearbyCompany, value);
        }

        private MvxObservableCollection<CompanyProduct> _productlist = new MvxObservableCollection<CompanyProduct>();
        public MvxObservableCollection<CompanyProduct> ProductList
        {
            get => _productlist;
            set => SetProperty(ref _productlist, value);
        }

        private MvxObservableCollection<CompanyLocation> _productCompanyLoca = new MvxObservableCollection<CompanyLocation>();
        public MvxObservableCollection<CompanyLocation> productCompanyLoca
        {
            get => _productCompanyLoca;
            set => SetProperty(ref _productCompanyLoca, value);
        }

        private MvxObservableCollection<CompanyOffer> _companyOffer = new MvxObservableCollection<CompanyOffer>();
        public MvxObservableCollection<CompanyOffer> CompanyOffer
        {
            get => _companyOffer;
            set => SetProperty(ref _companyOffer, value);
        }

        private MvxObservableCollection<PlatformOffer> _platformOffer = new MvxObservableCollection<PlatformOffer>();
        public MvxObservableCollection<PlatformOffer> PlatformOffer
        {
            get => _platformOffer;
            set => SetProperty(ref _platformOffer, value);
        }


        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set => SetProperty(ref _brandName, value);
        }

        public ObservableCollection<CompanyLocation> selected { get; private set; }


        private CompanyLocation _selectedLocation;
        public CompanyLocation selectedLocation
        {
            get => _selectedLocation;
            set => SetProperty(ref _selectedLocation, value);
        }

        private ImageSource _logoStoragePath;
        public ImageSource LogoStoragePath
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

        private decimal _rating;
        public decimal Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        private bool _isVisbleRubay;
        public bool IsVisbleRubay
        {
            get => _isVisbleRubay;
            set => SetProperty(ref _isVisbleRubay, value);
        }
        private bool _isVisbleCompanyOffer;
        public bool IsVisbleCompanyOffer
        {
            get => _isVisbleCompanyOffer;
            set => SetProperty(ref _isVisbleCompanyOffer, value);
        }

        public override void Prepare(Company company)
        {
            Id = company.Id;
            LogoStoragePath = ImageSource.FromUri(new Uri(Constants.BannerImageURL + company.LogoStoragePath));
            BrandName = company.BrandName;
            Rating = company.Rating;
            _company = company;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            var tasks = new List<Task>
            {
                LoadCompanyProduct(),
                LoadCompanyOffer(),
                LoadCompanyLocation(),
                LoadRupayBachaoOffer()
            };
            await Task.WhenAll(tasks);
        }

        public IMvxAsyncCommand<CompanyProduct> ProductTappedCommand => new MvxAsyncCommand<CompanyProduct>(async (CompanyProduct item) =>
        {
            await _navigationService.Navigate<ProductDetailPageViewModel, CompanyProduct>(item);
        });

        //Go To  Company Store Command Page
        private MvxObservableCollection<CompanyLocation> _fiveNearbyCompanies = new MvxObservableCollection<CompanyLocation>();
        public MvxObservableCollection<CompanyLocation> FiveNearbyCompanies
        {
            get => _fiveNearbyCompanies;
            set => SetProperty(ref _fiveNearbyCompanies, value);
        }

        public IMvxAsyncCommand<CompanyLocation> CompanyTappedCommand => new MvxAsyncCommand<CompanyLocation>(async (CompanyLocation item) =>
        {
            await _navigationService.Navigate<CompanyWithLocationPageViewModel, CompanyLocation>(selectedLocation);
        });

        //Navigate To Review Page
        public IMvxAsyncCommand NavigateToReviewPageCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<CompanyReviewPageViewModel, dynamic>(new
            {
                Id = Id,
                BrandName = BrandName,
                Rating = Rating,
                LogoStoragePath = LogoStoragePath,
            });
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        //Company Product list
        private async Task LoadCompanyProduct()
        {
            try
            {
                var companyCategoryProduct = await _companyProductService.GetProductByCompanyId(Id);

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

        //CompanyLocation
        private async Task LoadCompanyLocation()
        {
            try
            {
                var companyCategoryProduct = await _companyLocationService.GetCompanyAllLocation(Id);
                foreach (var companyLocation in companyCategoryProduct)
                {
                    productCompanyLoca.Add(companyLocation);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public IMvxAsyncCommand OpenWebCommand => new MvxAsyncCommand(async () =>
        {
            try
            {
                //if (DetailURL != null)
                //{
                //    await Browser.OpenAsync(DetailURL, BrowserLaunchMode.SystemPreferred);
                //}
                //else
                //{
                //    _userDialogs.Toast("No URL Found !");
                //}
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        });

        private async Task LoadCompanyOffer()
        {
            try
            {
                var companyOffer = await _companyOfferService.GetOfferByCompanyId(Id);

                if (companyOffer !=null && companyOffer.Count > 0 )
                {
                   IsVisbleCompanyOffer = true;
                    foreach (var companyOffers in companyOffer)
                    {
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
                IsVisbleCompanyOffer = false;
                _ = e.StackTrace;
            }
        }

        private async Task LoadRupayBachaoOffer()
        {
            try
            {
                var rupayBachos = await _companyOfferService.GetPlatformOfferByCompanyId(Id);
                if (rupayBachos.Count > 0)
                {
                    IsVisbleRubay = true;
                    foreach (var companyRupayBachosOffers in rupayBachos)
                    {
                        PlatformOffer.Add(companyRupayBachosOffers);
                    }
                }
                else
                {
                     IsVisbleRubay = false;
                }

            }
            catch (Exception e)
            {
                IsVisbleRubay = false;
                _ = e.StackTrace;
            }
        }

    }
}