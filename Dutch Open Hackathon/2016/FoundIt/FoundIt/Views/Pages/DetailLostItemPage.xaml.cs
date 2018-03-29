//
// DetailLostItemPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoundIt.Models;
using FoundIt.ViewModels;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;
using FoundIt.Utilities.Helpers;

namespace FoundIt.Views.Pages
{
    public partial class DetailLostItemPage : ContentPage
    {
        private DetailLostItemViewModel ViewModel => (DetailLostItemViewModel)BindingContext;

        public DetailLostItemPage(LostItem lostItem)
        {
            InitializeComponent();

            BindingContext = new DetailLostItemViewModel(Navigation, lostItem);

            DrawMapPin(lostItem);
        }

        private void DrawMapPin(LostItem lostItem)
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(lostItem.Latitude ?? 0, lostItem.Longitude ?? 0), Distance.FromKilometers(lostItem.Radius ?? 0 * 1.33)));
            map.Pins.Clear();
            map.Pins.Add(new Pin()
            {
                Label = lostItem.Name,
                Address = lostItem.LoseAddress,
                Position = new Xamarin.Forms.Maps.Position(lostItem.Latitude ?? 0, lostItem.Longitude ?? 0)
            });
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await NavigationHandler.PushAsync(Navigation, new FoundPage(new FoundItem()
            {
                LostItem = ViewModel.LostItem
            }, false));
        }
    }
}
