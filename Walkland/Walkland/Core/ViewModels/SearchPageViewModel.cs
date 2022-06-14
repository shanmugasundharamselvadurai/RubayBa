using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;

namespace Walkland.Core.ViewModels
{
    public class SearchPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ISearchService _searchService;
        private readonly ICompanyLocationService _companyLocationService;

        public SearchPageViewModel(IMvxNavigationService navigationService,
            IUserDialogs userDialogs, ISearchService searchService, ICompanyLocationService companyLocationService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _searchService = searchService;
            _companyLocationService = companyLocationService;
        }

        private MvxObservableCollection<CompanyProduct> _searchProductList = new MvxObservableCollection<CompanyProduct>();
        public MvxObservableCollection<CompanyProduct> SearchProductList
        {
            get => _searchProductList;
            set
            {
                if (_searchProductList != value)
                {
                    SetProperty(ref _searchProductList, value);
                }
            }
        }

        private CompanyLocation _selectedLocation;
        public CompanyLocation selectedLocation
        {
            get => _selectedLocation;
            set => SetProperty(ref _selectedLocation, value);
        }


        private MvxObservableCollection<CompanyLocation> _searchCompanyList = new MvxObservableCollection<CompanyLocation>();
        public MvxObservableCollection<CompanyLocation> SearchCompanyList
        {
            get => _searchCompanyList;
            set
            {
                if (_searchCompanyList != value)
                {
                    SetProperty(ref _searchCompanyList, value);
                }
            }
        }


        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    SetProperty(ref _searchText, value);
                }
            }
        }

        private bool _IsVibleProduct =false; 
        public bool IsVibleProduct
        {
            get { return _IsVibleProduct; }
            set
            {
                if (_IsVibleProduct != value)
                {
                    SetProperty(ref _IsVibleProduct, value);
                }
            }
        }

        private bool _IsVibleCompany = false;
        public bool IsVibleCompany
        {
            get { return _IsVibleCompany; }
            set
            {
                if (_IsVibleCompany != value)
                {
                    SetProperty(ref _IsVibleCompany, value);
                }
            }
        }

        public IMvxAsyncCommand<CompanyProduct> ProductTappedCommand => new MvxAsyncCommand<CompanyProduct>(async (CompanyProduct item) =>
        {
            await _navigationService.Navigate<ProductDetailPageViewModel, CompanyProduct>(item);
        });


        public IMvxAsyncCommand<CompanyLocation> CompanyTappedCommand => new MvxAsyncCommand<CompanyLocation>(async (CompanyLocation item) =>
        {
            //await _navigationService.Navigate<ProductDetailPageViewModel, CompanyProduct>(item);

            await _navigationService.Navigate<CompanyWithLocationPageViewModel, CompanyLocation>(selectedLocation);
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        public IMvxAsyncCommand SearchCommand => new MvxAsyncCommand(async () =>
        {
            await LoadDataIncremently();
        });

        private async Task LoadDataIncremently()
        {
           try
            {
                var searchCompany = await _companyLocationService.FindNearBySearchCompanies(SearchText);
      
                if (searchCompany != null && searchCompany.Count > 0)
                {
                    SearchCompanyList.Clear();
                     IsVibleProduct = false;
                    foreach (var searchCompanylist in searchCompany)
                    {
                        SearchCompanyList.Add(searchCompanylist);
                    }
                    IsVibleCompany = true;
                }
                else
                {
                    var searchProductList = await _searchService.GetProductById(SearchText);
                    SearchProductList.Clear();
                    IsVibleCompany = false;
                    foreach (var searchprodlists in searchProductList)
                    {
                        SearchProductList.Add(searchprodlists);
                    }
                    IsVibleProduct = true;
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;

            }
        }
    }
}