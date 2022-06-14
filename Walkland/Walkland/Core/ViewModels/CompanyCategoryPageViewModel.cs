using Acr.UserDialogs;
using Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
        public class CompanyCategoryPageViewModel : MvxViewModel<CompanyCategory>
        {
            private readonly IMvxNavigationService _navigationService;
            private readonly IUserDialogs _userDialogs;
            private readonly ICompanyRatingService _companyRatingService;
            private readonly ICompanyService _companyService;
            private Location location;
            private readonly ICompanyLocationService _companyLocationService;
            private readonly IAuthenticationService _authenticationService;
    
        public CompanyCategoryPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyRatingService companyRatingService, ICompanyService companyService, ICompanyLocationService companyLocationService, IAuthenticationService Authentication)
            {
                _navigationService = navigationService;
                _userDialogs = userDialogs;
                _companyRatingService = companyRatingService;
                _companyService = companyService;
                _companyLocationService = companyLocationService;
                 _authenticationService = Authentication;
        }

            private MvxObservableCollection<Company> _companylist = new MvxObservableCollection<Company>();
            public MvxObservableCollection<Company> CompanyList
            {
                get => _companylist;
                set => SetProperty(ref _companylist, value);
            }


        private CompanyCategory _companyCategory;
        public CompanyCategory CompanyCategory
        {
            get => _companyCategory;
            set => SetProperty(ref _companyCategory, value);
        }

        private string _categoryName;
            public string CategoryName
            {
                get => _categoryName;
                set => SetProperty(ref _categoryName, value);
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

            private string _brandName;
            public string BrandName
            {
                get => _brandName;
                set => SetProperty(ref _brandName, value);
            }

            private ImageSource _companyLogoImage;
            public ImageSource CompanyLogoImage
            {
                get => _companyLogoImage;
                set => SetProperty(ref _companyLogoImage, value);
            }

        private decimal _rating;
            public decimal Rating
            {
                get => _rating;
                set => SetProperty(ref _rating, value);
            }

            public override void Prepare(CompanyCategory companyCategoryModel)
            {
                CompanyCategory = companyCategoryModel;
                Id = CompanyCategory.Id;
                CategoryName = CompanyCategory.Name;
            
            }

            public override async Task Initialize()
            {
                await base.Initialize();

                await LoadGetCompanyByCompanyCategory();
                await LoadCompanyRating();
            }

            public IMvxAsyncCommand<Company> CompanyListTappedCommand => new MvxAsyncCommand<Company>(async (Company item) =>
            {
                var applicationID = await SecureStorage.GetAsync("ApplicationUserId");
                await _authenticationService.InsertUserCompanyCategory(new UserCompanyRequestDto()
                {
                    ApplicationUserId = Convert.ToInt32(applicationID),
                    CompanyCategoryId = item.CompanyCategoryId,
                    CompanyId = item.Id
                });
                await _navigationService.Navigate<CompanyPageViewModel, Company>(item);

            });

            public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
            {
                await _navigationService.Close(this);
            });

            //Load Company List
            private async Task LoadGetCompanyByCompanyCategory()
            {
                try
                {
                  location = await Geolocation.GetLastKnownLocationAsync();
                   var companyCategory = await _companyService.GetCompanyByCompanyCategoryId(Id);
                  //var companyCategory = await _companyService.FindNearByCompaniesByCategory(Id, location.Latitude.ToString(), location.Longitude.ToString());

                foreach (var companyCategories in companyCategory)
                    {
                        CompanyList.Add(companyCategories);
                        CompanyId = companyCategories.Id;
                    }
                }
                catch (Exception e)
                {
                System.Console.WriteLine(e.Message);
                    _ = e.StackTrace;
                }
            }


            private async Task LoadCompanyRating()
            {
                try
                {
                    var companyRating = await _companyRatingService.GetCompanyRating(CompanyId);
                }
                catch (Exception e)
                {
                    _ = e.StackTrace;
                }
            }
        }
    }
