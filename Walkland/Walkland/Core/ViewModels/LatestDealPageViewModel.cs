using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class LatestDealPageViewModel : MvxViewModel<LatestDeal>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        private string companyBanaerName { get; set; }
        private string companyLogo { get; set; }
        private decimal companyRating { get; set; }
        public long compId { get; set; }

        public LatestDealPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;

            company = new Company();
        }

        private MvxObservableCollection<Company> _companylist = new MvxObservableCollection<Company>();
        public MvxObservableCollection<Company> CompanyList
        {
            get => _companylist;
            set => SetProperty(ref _companylist, value);
        }

        private Company _company;
        public Company company
        {
            get => _company;
            set => SetProperty(ref _company, value);
        }

        private MvxObservableCollection<LatestDeal> _LatestDealcollection = new MvxObservableCollection<LatestDeal>();
        public MvxObservableCollection<LatestDeal> LatestDealcollection
        {
            get => _LatestDealcollection;
            set => SetProperty(ref _LatestDealcollection, value);
        }

        //private int _latestDealModel;
        //public int LatestDealModel
        //{
        //    get => _latestDealModel;
        //    set => SetProperty(ref _latestDealModel, value);
        //}

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
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

        private LatestDeal _latestDealModel;
        public LatestDeal latestDealModel
        {
            get => _latestDealModel;
            set => SetProperty(ref _latestDealModel, value);
        }

        public override void Prepare(LatestDeal latestDealModel)
        {
            companyBanaerName =  latestDealModel.Company.BrandName;
            companyLogo = latestDealModel.Company.LogoStoragePath;
            companyRating = latestDealModel.Company.Rating;
            compId = latestDealModel.Company.Id;

               Description = latestDealModel.Description;
               ImagePath = ImageSource.FromUri(new Uri(Constants.BannerImageURL + latestDealModel.PictureStoragePath));
               DetailURL = latestDealModel.URlForLandingPage;
        }

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        //private void LogUserIn()
        //{
        //    _navigationService.Navigate<CompanyPageViewModel, Company>(CompanyList);

        //}

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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        });

        public IMvxAsyncCommand OpenWebCommand => new MvxAsyncCommand(async () =>
        {
            try
            {
                if (DetailURL != null)
                {
                    await Browser.OpenAsync(DetailURL, BrowserLaunchMode.SystemPreferred);
                }
                else
                {
                    _userDialogs.Toast("No URL Found !");
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        });

    }
}