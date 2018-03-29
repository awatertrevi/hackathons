//
// FoundPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.Collections.Generic;
using FoundIt.ViewModels;
using Xamarin.Forms;
using FoundIt.Models;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Linq;
using Plugin.Geolocator;
using System.IO;

namespace FoundIt.Views.Pages
{
    public partial class FoundPage : ContentPage
    {
        private FoundPageViewModel ViewModel => (FoundPageViewModel)BindingContext;
        private bool isViewing;

        public FoundPage(FoundItem foundItem, bool isViewing)
        {
            InitializeComponent();

            BindingContext = new FoundPageViewModel(Navigation, foundItem);

            NavigationPage.SetBackButtonTitle(this, "Back");

            this.isViewing = isViewing;

            if (isViewing)
            {
                ToolbarItems.Clear();

                nameField.IsEnabled = !isViewing;
                descriptionField.IsEnabled = !isViewing;
                findingTimeField.IsEnabled = !isViewing;
                locationField.IsEnabled = !isViewing;

                var chatToolbarItem = new ToolbarItem() { Text = "Chat", };
                chatToolbarItem.SetBinding(MenuItem.CommandProperty, new Binding("OpenChat"));
                ToolbarItems.Add(chatToolbarItem);
            }

            if (!string.IsNullOrEmpty(foundItem.Picture))
            {
                image.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String((foundItem.Picture))));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await DrawPin(ViewModel.FoundItem.FindAddress);
        }

        private async void MakeImage(object sender, EventArgs e)
        {
            if (!isViewing)
                await TakePicture();
        }

        async void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await DrawPin(ViewModel.FoundItem.FindAddress);
        }

        private async Task<string> GetUserAddress()
        {
            var geolocator = CrossGeolocator.Current;

            if (!geolocator.IsGeolocationEnabled || !geolocator.IsGeolocationAvailable)
                return "Amsterdam, Netherlands";

            var position = await geolocator.GetPositionAsync();

            ViewModel.FoundItem.Latitude = position.Latitude;
            ViewModel.FoundItem.Longitude = position.Longitude;

            return await ViewModel.GetAddressForPosition(new Position(position.Latitude, position.Longitude));
        }

        private async System.Threading.Tasks.Task TakePicture()
        {
            var _mediaPicker = DependencyService.Get<Services.Media.IMediaPicker>();

            ImageSource ImageSource = null;

            await _mediaPicker.TakePhotoAsync(new Services.Media.CameraMediaStorageOptions { DefaultCamera = Services.Media.CameraDevice.Rear, MaxPixelDimension = 400, PercentQuality = 0 }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var s = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    var canceled = true;
                }
                else
                {
                    var mediaFile = t.Result;

                    System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                    mediaFile.Source.CopyTo(mstream);
                    mstream.Position = 0;
                    ViewModel.FoundItem.Picture = Convert.ToBase64String(mstream.ToArray());
                    mstream.Position = 0;
                    ImageSource = ImageSource.FromStream(() => mstream);

                    return mediaFile;
                }

                return null;
            });

            if (ImageSource != null)
            {
                image.Source = ImageSource;
            }
        }

        private async Task DrawPin(string address)
        {
            if (string.IsNullOrEmpty(address))
                address = await GetUserAddress();

            var geocoder = new Geocoder(); // TODO: Do with the DOH geocoder thingy.
            var positions = await geocoder.GetPositionsForAddressAsync(address);

            if (positions.Count() > 0)
            {
                var position = positions.First();

                ViewModel.FoundItem.Latitude = position.Latitude;
                ViewModel.FoundItem.Longitude = position.Longitude;

                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, ViewModel.FoundItem.LostItem == null ? Distance.FromMeters(700) : Distance.FromKilometers(ViewModel.FoundItem.LostItem.Radius ?? 0)));
                map.Pins.Clear();
                map.Pins.Add(new Pin()
                {
                    Label = ViewModel.FoundItem?.LostItem.Name ?? "New Found Item",
                    Address = ViewModel.FoundItem.FindAddress,
                    Position = position
                });
            }
        }
    }
}
