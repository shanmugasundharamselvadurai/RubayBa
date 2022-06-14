using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.Core.ViewModels
{
    public class RFTSuccessfulPageViewModel : MvxViewModel<dynamic>
    {
        private readonly IMvxNavigationService _navigationService;

        public RFTSuccessfulPageViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ScreenshotCommand = new MvxAsyncCommand(async () => await CaptureScreenshot(), () => Screenshot.IsCaptureSupported);
            EmailCommand = new MvxAsyncCommand(async () => await EmailScreenshot(), () => Screenshot.IsCaptureSupported);
        }

        private string _screenShotImage;
        public string ScrennShotImage
        {
            get => _screenShotImage;
            set => SetProperty(ref _screenShotImage, value);
        }

        private string _mobilenumber;
        public string MobileNumber
        {
            get => _mobilenumber;
            set => SetProperty(ref _mobilenumber, value);
        }

        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set => SetProperty(ref _accountNumber, value);
        }

        private string _legalName;
        public string LegalName
        {
            get => _legalName;
            set => SetProperty(ref _legalName, value);
        }

        private decimal _paymentamount;
        public decimal PayAmount
        {
            get => _paymentamount;
            set => SetProperty(ref _paymentamount, value);
        }

        private string _reason;
        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }

        private string _transactionId;
        public string TransactionId
        {
            get => _transactionId;
            set => SetProperty(ref _transactionId, value);
        }

        private DateTime _createdDateTimeUtc;
        public DateTime TransactionDateTime
        {
            get => _createdDateTimeUtc;
            set => SetProperty(ref _createdDateTimeUtc, value);
        }

        private ImageSource _resultImageSource;
        public ImageSource ResultImageSource
        {
            get => _resultImageSource;
            set => SetProperty(ref _resultImageSource, value);
        }

        public override void Prepare(dynamic companyWalletTransaction)
        {
            PayAmount = companyWalletTransaction.PayAmount;
            Reason = companyWalletTransaction.Reason;
            LegalName = companyWalletTransaction.LegalName;
            TransactionId = companyWalletTransaction.TransactionId;
            TransactionDateTime = companyWalletTransaction.CreatedDateTimeUtc;
        }

        public IMvxAsyncCommand EwalletPageCommand => new MvxAsyncCommand(async () =>
        {
            await _navigationService.Navigate<DashboardPageViewModel>();
        });

        public MvxAsyncCommand ScreenshotCommand { get; }

        public MvxAsyncCommand EmailCommand { get; }

        async Task CaptureScreenshot()
        {
            var mediaFile = await Screenshot.CaptureAsync();
            var stream = await mediaFile.OpenReadAsync(ScreenshotFormat.Png);

            ResultImageSource = ImageSource.FromStream(() => stream);
        }

        async Task EmailScreenshot()
        {
            var mediaFile = await Screenshot.CaptureAsync();

            var temp = Path.Combine(FileSystem.CacheDirectory, "screenshot.jpg");
            using (var stream = await mediaFile.OpenReadAsync(ScreenshotFormat.Jpeg))
            using (var file = File.Create(temp))
            {
                await stream.CopyToAsync(file);
            }

            await Email.ComposeAsync(new EmailMessage
            {
                Attachments = { new EmailAttachment(temp) }
            });
        }
    }
}