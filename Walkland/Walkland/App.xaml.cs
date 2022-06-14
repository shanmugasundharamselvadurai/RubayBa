using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;


namespace Walkland
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDQ2NDY3QDMxMzkyZTMxMmUzMEdXUVE5MWZ0dkdDNm9GM1ZJRHhIMEhIcnpidE5xb0UyWGJwZlRRam1Kelk9");
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=5f496166-c94f-446b-8786-a82bfb007619;" +
                  "ios=2d5e402a-d22d-4ae5-936b-0f8432eba0b3",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
