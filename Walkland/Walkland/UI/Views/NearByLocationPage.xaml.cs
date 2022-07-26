using System;
using System.Collections.Generic;
using MvvmCross.Forms.Views;
using Walkland.Core.PlacesSearchBar;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace UI.Views
{
    public partial class NearByLocationPage : MvxContentPage<NearByLocationPageViewModel>
    {
        private readonly string GooglePlacesApiKey = "AIzaSyAfK6zqW-mMq_u6C07a8KXkBUmZfsO2CL4";


        public NearByLocationPage()
        {
            InitializeComponent();

            search_bar.ApiKey = GooglePlacesApiKey;
            search_bar.Type = PlaceType.Geocode;
            search_bar.Components = new Components("country:in");
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 1;
            results_list.ItemSelected += Results_List_ItemSelected;
        }


        void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;
            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
                results_list.IsVisible = true;
            else
                return;
        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    results_list.IsVisible = false;
                    spinner.IsVisible = true;
                    spinner.IsRunning = true;
                }
                else
                {
                    results_list.IsVisible = true;
                    spinner.IsRunning = false;
                    spinner.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;

                var prediction = (AutoCompletePrediction)e.SelectedItem;
                results_list.SelectedItem = null;

                var place = await Places.GetPlace(prediction.Place_ID, GooglePlacesApiKey);
                if (place != null)
                {
                    string.Format("Lat: {0}\nLon: {1}", place.Latitude, place.Longitude);
                    {
                        Console.WriteLine($"Latitude: {place.Latitude}, Longitude: {place.Longitude}");
                    }

                    ViewModel.Latitude = place.Latitude.GetValueOrDefault();
                    ViewModel.Longitude = place.Longitude.GetValueOrDefault();

                    await ViewModel.SendDataToHomePage();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
           
        }
    }
}