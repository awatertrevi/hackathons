//
// AddLostItemPageViewModel.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using FoundIt.ComponentModel;
using Xamarin.Forms;
using FoundIt.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using FoundIt.Utilities.Helpers;
using System.Net;
using System.Collections.ObjectModel;
using Plugin.Geolocator;
using System.Text.RegularExpressions;
using Plugin.Geolocator.Abstractions;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xamarin.Forms.Maps;
using EventKit;

namespace FoundIt.ViewModels
{
    public class BarCodePageViewModel : ViewModelBase
    {
        public ICommand GetNearbyShippmentLocations { get { return get(() => this.GetNearbyShippmentLocations); } set { set(() => this.GetNearbyShippmentLocations, value); } }
        public ObservableCollection<ShipmentLocation> ShipmentLocations { get { return get(() => this.ShipmentLocations); } set { set(() => this.ShipmentLocations, value); } }

        public BarCodePageViewModel(INavigation navigaton) : base(navigaton)
        {
            ShipmentLocations = new ObservableCollection<ShipmentLocation>();

            GetNearbyShippmentLocations = new Command((m) =>
            {
                if (m == null)
                    return;

                var mapSpan = m as MapSpan;
                var rand = new Random();

                ShipmentLocations.Clear();

                ShipmentLocations.Add(new ShipmentLocation()
                {
                    Latitude = mapSpan.Center.Latitude,
                    Longitude = mapSpan.Center.Longitude
                });

                ShipmentLocations.Add(new ShipmentLocation()
                {
                    Latitude = mapSpan.Center.Latitude - 0.01,
                    Longitude = mapSpan.Center.Longitude - 0.015
                });

                ShipmentLocations.Add(new ShipmentLocation()
                {
                    Latitude = mapSpan.Center.Latitude + 0.02,
                    Longitude = mapSpan.Center.Longitude + 0.009
                });

                ShipmentLocations.Add(new ShipmentLocation()
                {
                    Latitude = mapSpan.Center.Latitude + 0.04,
                    Longitude = mapSpan.Center.Longitude + 0.04
                });

                ShipmentLocations.Add(new ShipmentLocation()
                {
                    Latitude = mapSpan.Center.Latitude - 0.02,
                    Longitude = mapSpan.Center.Longitude + 0.03
                });

                MessagingCenter.Send(this, "PlaceShipmentLocation", ShipmentLocations);
            });

            if (GetNearbyShippmentLocations.CanExecute(null))
                GetNearbyShippmentLocations.Execute(null);
        }

        private async Task<Plugin.Geolocator.Abstractions.Position> GetUserPosition()
        {
            var geolocator = CrossGeolocator.Current;

            if (!geolocator.IsGeolocationEnabled || !geolocator.IsGeolocationAvailable)
                return null;

            var position = await geolocator.GetPositionAsync();

            return position;
        }
    }
}
