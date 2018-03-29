//
// MapPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoundIt.ViewModels;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using FoundIt.Models;
using System.Threading.Tasks;
using System.Linq;
using UIKit;
using Foundation;
using Plugin.Geolocator;
using FoundIt.Views.Controls;
using ImageCircle.Forms.Plugin.Abstractions;
using FoundIt.Utilities.Helpers;

namespace FoundIt.Views.Pages
{
    public partial class MapPage : ContentPage
    {
        private MapPageViewModel ViewModel => (MapPageViewModel)BindingContext;

        public MapPage()
        {
            InitializeComponent();

            BindingContext = new MapPageViewModel(Navigation);

            map.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "VisibleRegion")
                {
                    if (ViewModel.LoadLostItems.CanExecute(map.VisibleRegion))
                        ViewModel.LoadLostItems.Execute(map.VisibleRegion);
                }
            };

            MessagingCenter.Subscribe<MapPageViewModel>(this, "ClearPins", (obj) =>
            {
                map.Pins.Clear();
            });

            MessagingCenter.Subscribe<MapPageViewModel, ObservableCollection<LostItem>>(this, "PlaceLostItems", (sender, lostItems) =>
            {
                foreach (var item in lostItems)
                {
                    var pin = new Pin()
                    {
                        Label = item.Name ?? "Lost Item",
                        Address = item.LoseAddress,
                        Position = new Position(item.Latitude ?? 0, item.Longitude ?? 0)
                    };

                    pin.SetImage(new FileImageSource() { File = "umbrella.png" });

                    pin.SetCalloutView(new PinDetailView()
                    {
                        Title = item.Name,
                        Subtitle = item.Description,
                        ImageSource = ImageSource.FromUri(new Uri(item.Reporter.ProfilePhoto)),
                        GestureRecognizers = {
                            new TapGestureRecognizer((v) => {
                                NavigationHandler.PushAsync(this.Navigation,new DetailLostItemPage(item));
                            })
                        }
                    });

                    map.Pins.Add(pin);
                }
            });

            MessagingCenter.Subscribe<MapPageViewModel, ObservableCollection<FoundItem>>(this, "PlaceFoundItems", (sender, foundItems) =>
            {
                foreach (var item in foundItems)
                {
                    var pin = new Pin()
                    {
                        Label = item?.LostItem?.Name ?? "Found Item",
                        Address = item.FindAddress,
                        Position = new Position(item.Latitude, item.Longitude)
                    };

                    pin.SetImage(new FileImageSource() { File = "warning.png" });
                    pin.SetCalloutView(new PinDetailView()
                    {
                        Title = item?.LostItem?.Name ?? "Found Item",
                        Subtitle = item?.LostItem?.Description ?? "-",
                        ImageSource = item.Picture != null ? ImageSource.FromStream(() => new System.IO.MemoryStream(Convert.FromBase64String(item.Picture)))
                                          : ImageSource.FromUri(new Uri(item.Finder.ProfilePhoto))
                                          ,
                        GestureRecognizers = {
                            new TapGestureRecognizer(v => {
                                NavigationHandler.PushAsync(this.Navigation,new FoundPage(item,true));
                            })
                        }
                    });

                    map.Pins.Add(pin);
                }
            });
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
