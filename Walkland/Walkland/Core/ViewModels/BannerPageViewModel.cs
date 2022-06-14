using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using Walkland.Core.Constant;
using Walkland.Core.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class BannerPageViewModel : MvxViewModel<AppBanner>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        private string companyBanaerName { get; set; }
        private string companyLogo { get; set; }
        private decimal companyRating { get; set; }
        public long compId { get; set; }

        public BannerPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
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

        public override void Prepare(AppBanner appBannerModel)
        {
            companyBanaerName = appBannerModel.Company.BrandName;
            companyLogo = appBannerModel.Company.LogoStoragePath;
            companyRating = appBannerModel.Company.Rating;
            compId = appBannerModel.Company.Id;
            if (appBannerModel != null)
            {
                Description = appBannerModel.Description;
                ImagePath = ImageSource.FromUri(new Uri(Constants.BannerImageURL + appBannerModel.PictureStoragePath));
                DetailURL = appBannerModel.URlForLandingPage;
            }
        }

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });

        public IMvxAsyncCommand OpenWebCommand => new MvxAsyncCommand(async () =>
        { 
            if(DetailURL!=null)
            {
                await Browser.OpenAsync(DetailURL, BrowserLaunchMode.SystemPreferred);
            }
            else
            {
                _userDialogs.Toast("No URL Found !");
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
    }
}