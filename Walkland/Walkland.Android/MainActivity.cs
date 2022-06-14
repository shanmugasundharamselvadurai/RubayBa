using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using XF.Material.Droid;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Platforms.Android.Core;

using Android.Views;
using Acr.UserDialogs;
using Walkland.Core;
using Plugin.Fingerprint;
using Helper;

namespace Walkland.Droid    
{
    [Activity(Label = "Walkland",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<CoreApp, App>, CoreApp, App>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Material.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            //Current activity 
            CrossFingerprint.SetCurrentActivityResolver(() => this);

            base.OnCreate(savedInstanceState);
            // For Add SMS Read 
            string Value = AppHashKeyHelper.GetAppHashKey(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}