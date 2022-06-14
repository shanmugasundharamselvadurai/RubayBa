using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;

using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class CompanyReviewPageViewModel : MvxViewModel<dynamic>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ICompanyRatingService _companyRatingService;

        public CompanyReviewPageViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ICompanyRatingService companyRatingService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _companyRatingService = companyRatingService;
        }

        private string _brandName;
        public string BrandName
        {
            get => _brandName;
            set => SetProperty(ref _brandName, value);
        }

        private ImageSource _logoStoragePath;
        public ImageSource LogoStoragePath
        {
            get => _logoStoragePath;
            set => SetProperty(ref _logoStoragePath, value);
        }

        private decimal _rating;
        public decimal Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        private decimal _inputRating;
        public decimal InputRating
        {
            get => _inputRating;
            set => SetProperty(ref _inputRating, value);
        }

        private long _companyId;
        public long CompanyId
        {
            get => _companyId;
            set => SetProperty(ref _companyId, value);
        }

        public override void Prepare(dynamic company)
        {
            CompanyId = company.Id;
            BrandName = company.BrandName;
            Rating = company.Rating;
            LogoStoragePath = company.LogoStoragePath;
        }

        public IMvxAsyncCommand CreateRatingCommand => new MvxAsyncCommand(async () =>
        {
            try
            {
                var companyRatings = await _companyRatingService.CreateRating(CompanyId, InputRating);
                await _userDialogs.AlertAsync("Thanks For Your Feedback", "Rupay Bachao", "OK");
            }

            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        });

        public IMvxAsyncCommand NavigateBackCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Close(this);
        });
    }
}