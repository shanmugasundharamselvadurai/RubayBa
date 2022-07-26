using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class CompanySubCategoryPageViewModel:MvxViewModel<CompanyCategory>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyRatingService _companyRatingService;
        private readonly ICompanyCategoryService _companySubCategoryService;
        private readonly ICompanyService _companyService;
        private Location location;
        private long companySubCategoryID;
        private Company _companyCategory;
        private readonly ICompanyLocationService _companyLocationService;
        private readonly IAuthenticationService _authenticationService;

        public CompanySubCategoryPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs,
            ICompanyRatingService companyRatingService, ICompanyCategoryService companySubCategoryService,
            ICompanyLocationService companyLocationService, IAuthenticationService Authentication, ICompanyService companyService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyRatingService = companyRatingService;
            _companySubCategoryService = companySubCategoryService;
            _companyLocationService = companyLocationService;
            _authenticationService = Authentication;
            _companyService = companyService;
            _companyCategory = new Company();
        }

        private MvxObservableCollection<Company> _companylist = new MvxObservableCollection<Company>();
        public MvxObservableCollection<Company> CompanyList
        {
            get => _companylist;
            set => SetProperty(ref _companylist, value);
        }

        private MvxObservableCollection<CompanySubCategory> _companySublist = new MvxObservableCollection<CompanySubCategory>();
        public MvxObservableCollection<CompanySubCategory> CompanySubList
        {
            get => _companySublist;
            set => SetProperty(ref _companySublist, value);
        }


        public Company CompanyCategory
        {
            get => _companyCategory;
            set => SetProperty(ref _companyCategory, value);
        }

        //private CompanyCategory _companyCategory;
        //public CompanyCategory CompanyCategory
        //{
        //    get => _companyCategory;
        //    set => SetProperty(ref _companyCategory, value);
        //}
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
            Id = companyCategoryModel.Id;
            CategoryName = companyCategoryModel.Name;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadGetCompanyByCompanyCategory();
       
        }

        public IMvxAsyncCommand<CompanySubCategory> CompanyListTappedCommand => new MvxAsyncCommand<CompanySubCategory>(async (CompanySubCategory item) =>
        {
            var applicationID = await SecureStorage.GetAsync("ApplicationUserId");
            await _authenticationService.InsertUserCompanyCategory(new UserCompanyRequestDto()
            {
                ApplicationUserId = Convert.ToInt32(applicationID),
                CompanyCategoryId = item.CompanyCategoryId,
                CompanyId = item.Id,
            });


           await _navigationService.Navigate<CompanyCategoryPageViewModel, CompanySubCategory>(item);
               
        });

        private async Task callCompanyMethod()
        {
            await _navigationService.Navigate<CompanyPageViewModel, Company>(CompanyCategory);
        }

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
                var companyCategory = await _companySubCategoryService.GetCompanySubCategories(Id);
                foreach (var companyCategories in companyCategory)
                {
                    CompanySubList.Add(companyCategories);
                 //   CompanyId = companyCategories.Id;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                _ = e.StackTrace;
            }
        }

        private async Task loadSubCategory()
        {
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
                var companyCategory = await _companyService.GetCompanyByCompanySubCategoryId(companySubCategoryID);

                foreach (var companyCategories in companyCategory)
                {
                    CompanyList.Add(companyCategories);
                    CompanyCategory.Id = companyCategories.Id;
                    CompanyCategory.LogoStoragePath = companyCategories.LogoStoragePath;
                    CompanyCategory.BrandName = companyCategories.BrandName;
                    companyCategories.Rating = companyCategories.Rating;
                }
              _ = callCompanyMethod();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                _ = e.StackTrace;
            }
        }
    }
}
