using System;
using System.Threading.Tasks;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Walkland.Core.Models;
using Walkland.Core.Services.Interfaces;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Contorls
{
    public partial class PopupView : PopupPage
    {
        public bool isADeals;
        public bool isNearme;
        public bool isSearch;
        public bool isCategory;

        public PopupView()
        {
            InitializeComponent();

            this.BindingContext = new PopupViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            isVisbleDeals.IsVisible = isADeals;
            IsVisbleNearMe.IsVisible = isNearme;
            IsVisbleSearch.IsVisible = isSearch;
            IsVisbleCategory.IsVisible = isCategory;

        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {

        }

        public void CheckBox_CheckedChanged_1(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
           // _ = BindingContext as MvxViewModel<PopupViewModel>;
            //viewModel?.ssAsync();
        }

        //Latest Deal Method
        //private async Task LoadLatestDealsAsync()
        //{
        //    try
        //    {
        //        var latestDeals = await _latestDealService.GetLatestDeals();
        //        LatestDeals.Clear();
        //        foreach (var latestDealImage in latestDeals)
        //        {
        //            LatestDeals.Add(latestDealImage);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _ = e.StackTrace;
        //    }
        //}
    }
}
