using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class ProductDetailPageViewModel : MvxViewModel<CompanyProduct>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyProductFavoriteService _companyProductFavoriteService;

        private string companyBanaerName { get; set; }
        private string companyLogo { get; set; }
        private decimal companyRating { get; set; }
        public long compId { get; set; }

        public ProductDetailPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyProductFavoriteService companyProductFavoriteService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyProductFavoriteService = companyProductFavoriteService;
            company = new Company();
        }

        private Company _company;
        public Company company
        {
            get => _company;
            set => SetProperty(ref _company, value);
        }

        private bool _isFavoriteProduct;
        public bool IsFavoriteProduct
        {
            get => _isFavoriteProduct;
            set => SetProperty(ref _isFavoriteProduct, value);
        }

        private CompanyProduct _productDetails;
        public CompanyProduct ProductDetails
        {
            get => _productDetails;
            set => SetProperty(ref _productDetails, value);
        }

        private long _id;
        public long Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _newPrice;
        public string NewPrice
        {
            get => _newPrice;
            set => SetProperty(ref _newPrice, value);
        }

        private string _oldPrice;
        public string OldPrice
        {
            get => _oldPrice;
            set => SetProperty(ref _oldPrice, value);
        }

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set => SetProperty(ref _brandName, value);
        }

        private string _productDescription;
        public string ProductDescription
        {
            get => _productDescription;
            set => SetProperty(ref _productDescription, value);
        }

        private ImageSource _imagePath;
        public ImageSource ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        private string _detailURL;
        public string DetailURL
        {
            get => _detailURL;
            set => SetProperty(ref _detailURL, value);
        }

        private bool _isVibile;
        public bool IsVibile
        {
            get => _isVibile;
            set => SetProperty(ref _isVibile, value);
        }


        public override void Prepare(CompanyProduct companyProduct)
        {
            companyBanaerName = companyProduct.Company.BrandName;
            companyLogo = companyProduct.Company.LogoStoragePath;
            companyRating = companyProduct.Company.Rating;
            compId = companyProduct.Company.Id;

            Id = companyProduct.Id;
            Name = companyProduct.Name;
            ImagePath = ImageSource.FromUri(new Uri(Constants.BannerImageURL + companyProduct.ProductStoragePath));
            DetailURL = companyProduct.ProductLink;
            OldPrice = companyProduct.NewPrice;
            NewPrice = companyProduct.OldPrice;
            BrandName = companyProduct.Company.BrandName;
            ProductDescription = companyProduct.ProductDescription;


            if(DetailURL  == null || string.IsNullOrEmpty(DetailURL))
            {
                IsVibile = false;
            }
            else
            {
                IsVibile = true;
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadIsFavoriteProduct();
        }

        public async Task LoadIsFavoriteProduct()
        {
            try
            {
                if (await _companyProductFavoriteService.IsFavoriteProduct(Id))
                {
                    IsFavoriteProduct = true;
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        //Add To Favourite
        public IMvxAsyncCommand AddToFavorite => new MvxAsyncCommand(async () =>
        {
            try
            {
                if (await _companyProductFavoriteService.AddFavoriteProduct(Id) == "CREATED")
                {
                    IsFavoriteProduct = true;
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        });

        public IMvxAsyncCommand CompanyDetailPageCommand => new MvxAsyncCommand(async () =>
        {
            try
            {
                company.BrandName = companyBanaerName;
                company.LogoStoragePath = companyLogo;
                company.Rating = companyRating;
                company.Id = compId;

                if (company != null)
                {
                    await _navigationService.Navigate<CompanyPageViewModel, Company>(company);
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        });


        //Remove From Favourite
        public IMvxAsyncCommand RemoveFromFavorite => new MvxAsyncCommand(async () =>
        {
            try
            {
                if (await _companyProductFavoriteService.RemoveFavoriteProduct(Id) == "DELETED")
                {
                    IsFavoriteProduct = false;
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        });

        public IMvxAsyncCommand OpenWebCommand => new MvxAsyncCommand(async () =>
        {
            if(DetailURL == null || string.IsNullOrEmpty(DetailURL))
            {
                return ;
            }
            else
            {
                await Browser.OpenAsync(DetailURL, BrowserLaunchMode.SystemPreferred);
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}