//
// BarCodePage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/11/2016
//
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FoundIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using FoundIt.Models;
using FoundIt.Views.Controls;
using System.Threading.Tasks;
using System.Linq;
using Plugin.Geolocator;

namespace FoundIt.Views.Pages
{
    public partial class BarCodePage : ContentPage
    {
        private BarCodePageViewModel ViewModel => (BarCodePageViewModel)BindingContext;

        public BarCodePage()
        {
            InitializeComponent();

            BindingContext = new BarCodePageViewModel(Navigation);

            map.PropertyChanged += (sender, e) =>
           {
               if (e.PropertyName == "VisibleRegion")
               {
                   if (ViewModel.GetNearbyShippmentLocations.CanExecute(map.VisibleRegion))
                       ViewModel.GetNearbyShippmentLocations.Execute(map.VisibleRegion);
               }
           };

            MessagingCenter.Subscribe<BarCodePageViewModel, ObservableCollection<ShipmentLocation>>(this, "PlaceShipmentLocation", (sender, shipmentLocation) =>
            {
                map.Pins.Clear();

                foreach (var item in shipmentLocation)
                {
                    var pin = new Pin()
                    {
                        Label = item?.Name ?? "Shipment Location",
                        Position = new Position(item.Latitude, item.Longitude)
                    };

                    pin.SetImage(new FileImageSource() { File = "postNL.png" });

                    map.Pins.Add(pin);
                }
            });
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            infoDialog.IsVisible = false;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await DisplayAlert("You are great!", "Your package will now be returned to it's owner.", "OK");

            await Navigation.PopToRootAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await FocusOnUser();
        }

        private async Task FocusOnUser()
        {
            var geolocator = CrossGeolocator.Current;

            if (geolocator.IsGeolocationEnabled)
            {
                if (geolocator.IsGeolocationAvailable)
                {
                    var position = await geolocator.GetPositionAsync(10000);

                    if (position != null)
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(10)));

                    else
                        await MoveToLocation("Amsterdam, Netherlands");
                }

                else
                    await MoveToLocation("Amsterdam, Netherlands");
            }

            else
                await MoveToLocation("Amsterdam, Netherlands");
        }

        private async Task MoveToLocation(string location)
        {
            var geocoder = new Geocoder();
            var positions = await geocoder.GetPositionsForAddressAsync(location);
            var position = positions.FirstOrDefault();

            if (position != null)
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(10)));
        }
    }
}
