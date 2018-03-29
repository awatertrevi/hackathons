//
// AddLostItemPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoundIt.ViewModels;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Threading.Tasks;
using FoundIt.Effects;
using Plugin.Geolocator;

namespace FoundIt.Views.Pages
{
    public partial class AddLostItemPage : ContentPage
    {
        private bool flag = false;
        private AddLostItemPageViewModel ViewModel => (AddLostItemPageViewModel)BindingContext;

        public AddLostItemPage()
        {
            InitializeComponent();

            BindingContext = new AddLostItemPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AddRanges();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            infoDialog.IsVisible = false;
        }

        private async Task<string> GetUserAddress()
        {
            var geolocator = CrossGeolocator.Current;

            if (!geolocator.IsGeolocationEnabled || !geolocator.IsGeolocationAvailable)
                return "Amsterdam, Netherlands";

            var position = await geolocator.GetPositionAsync();

            ViewModel.LostItem.Latitude = position.Latitude;
            ViewModel.LostItem.Longitude = position.Longitude;

            return await ViewModel.GetAddressForPosition(new Position(position.Latitude, position.Longitude));
        }

        private void NameChanged(object sender, TextChangedEventArgs e)
        {
            Title = e.NewTextValue;
        }

        private void AddRanges()
        {
            radiusPicker.Items.Add("1km");
            radiusPicker.Items.Add("2km");
            radiusPicker.Items.Add("5km");
            radiusPicker.Items.Add("10km");

            radiusPicker.SelectedIndex = 0;
        }

        private async void RadiusChanged(object sender, EventArgs e)
        {
            var radius = radiusPicker.Items[radiusPicker.SelectedIndex];

            ViewModel.LostItem.Radius = double.Parse(radius.Replace("km", ""));

            await DrawCircle(ViewModel.LostItem.LoseAddress);
        }

        private async void LocationUnfocused(object sender, FocusEventArgs e)
        {
            await DrawCircle(ViewModel.LostItem.LoseAddress);
        }

        private async Task DrawCircle(string address)
        {
            if (string.IsNullOrEmpty(address))
                address = await GetUserAddress();

            var geocoder = new Geocoder(); // TODO: Do with the DOH geocoder thingy.
            var positions = await geocoder.GetPositionsForAddressAsync(address);

            if (positions.Count() > 0)
            {
                var position = positions.First();

                ViewModel.LostItem.Latitude = position.Latitude;
                ViewModel.LostItem.Longitude = position.Longitude;

                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers((ViewModel.LostItem.Radius ?? 0) * 1.35)));

                var effect = map.Effects.OfType<CircleEffect>().SingleOrDefault();

                if (effect != null)
                    map.Effects.Remove(effect);

                map.Effects.Add(new CircleEffect()
                {
                    Lat = position.Latitude,
                    Lon = position.Longitude,

                    Radius = Distance.FromKilometers(ViewModel.LostItem.Radius ?? 0).Meters
                });
            }
        }
    }
}
