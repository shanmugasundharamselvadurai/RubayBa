using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class MyFavoritePageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyProductFavoriteService _companyProductFavoriteService;

        public MyFavoritePageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyProductFavoriteService companyProductFavoriteService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyProductFavoriteService = companyProductFavoriteService;
        }

        private MvxObservableCollection<CompanyProduct> _favoriteProduct = new MvxObservableCollection<CompanyProduct>();
        public MvxObservableCollection<CompanyProduct> FavoriteProduct
        {
            get => _favoriteProduct;
            set => SetProperty(ref _favoriteProduct, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set => SetProperty(ref _brandName, value);
        }

        private long _id;
        public long Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private ImageSource _productImage;
        public ImageSource ProductImage
        {
            get => _productImage;
            set => SetProperty(ref _productImage, value);
        }

        public IMvxAsyncCommand<CompanyProduct> ProductTappedCommand => new MvxAsyncCommand<CompanyProduct>(async (CompanyProduct item) =>
        {
            await _navigationService.Navigate<ProductDetailPageViewModel, CompanyProduct>(item);
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        public override async Task Initialize()
        {
            await base.Initialize();

            await LoadFavoriteProducts();
        }

        private async Task LoadFavoriteProducts()
        {
            try
            {
                var companyProductFavorite = await _companyProductFavoriteService.GetFavoriteProducts();

                foreach (var favoriteProducts in companyProductFavorite)
                {
                    FavoriteProduct.Add(favoriteProducts);
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}