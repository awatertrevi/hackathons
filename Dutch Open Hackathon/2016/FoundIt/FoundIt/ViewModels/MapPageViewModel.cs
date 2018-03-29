//
// MapPageViewModel.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//

using System;
using FoundIt.ComponentModel;
using Xamarin.Forms;
using FoundIt.Models;
using System.Collections.ObjectModel;
using Plugin.Geolocator;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms.Maps;

namespace FoundIt.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public ObservableCollection<LostItem> LostItems { get { return get(() => this.LostItems); } set { set(() => this.LostItems, value); } }
        public ObservableCollection<FoundItem> FoundItems { get { return get(() => this.FoundItems); } set { set(() => this.FoundItems, value); } }

        public ICommand LoadLostItems { get { return get(() => this.LoadLostItems); } set { set(() => this.LoadLostItems, value); } }

        public MapPageViewModel(INavigation navigation) : base(navigation)
        {
            LoadLostItems = new Command(async (visibleRegion) =>
            {
                IsLoading = true;

                var mapSpan = visibleRegion as MapSpan;

                var lostResult = await ApplicationContext.Current.GetNearbyLostItems(mapSpan.Center.Latitude, mapSpan.Center.Longitude, mapSpan.Radius.Kilometers * 1.33);
                var foundResult = await ApplicationContext.Current.GetNearbyFoundItems(mapSpan.Center.Latitude, mapSpan.Center.Longitude, mapSpan.Radius.Kilometers * 1.33);

                if (lostResult.Success == false || foundResult.Success == false)
                    return;


                LostItems = lostResult.Result;
                FoundItems = foundResult.Result;

                MessagingCenter.Send(this, "ClearPins");
                MessagingCenter.Send(this, "PlaceLostItems", LostItems);
                MessagingCenter.Send(this, "PlaceFoundItems", FoundItems);

                IsLoading = false;
            });
        }
    }
}
