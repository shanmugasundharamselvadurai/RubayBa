using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;
using Walkland.Core;
using Xamarin.Forms.PancakeView.iOS;

namespace Walkland.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<CoreApp, App>, CoreApp, App>
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            XF.Material.iOS.Material.Init();
            PancakeViewRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            Rg.Plugins.Popup.Popup.Init();
            Syncfusion.SfRating.XForms.iOS.SfRatingRenderer.Init();
            uiApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);
            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}