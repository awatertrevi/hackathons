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
using Xamarin.Forms.Maps;
using System.Linq;
using System.Windows.Input;
using FoundIt.Utilities.Helpers;

namespace FoundIt.ViewModels
{
    public class AddLostItemPageViewModel : ViewModelBase
    {
        public LostItem LostItem { get { return get(() => this.LostItem); } set { set(() => this.LostItem, value); } }

        public ICommand SaveLostItem { get { return get(() => this.SaveLostItem); } set { set(() => this.SaveLostItem, value); } }

        public AddLostItemPageViewModel(INavigation navigation) : base(navigation)
        {
            LostItem = new LostItem()
            {
                EstimatedLoseTime = DateTime.Now,
                IsStolen = false
            };

            SaveLostItem = new Command(async () =>
            {
                var position = await GetPositionForAddress(LostItem.LoseAddress);

                await GetAddressForPosition(position);

                if (await ApplicationContext.Current.AddLostItem(LostItem))
                    await NavigationHandler.PopAsync(Navigation);
            });
        }

        public async Task<Position> GetPositionForAddress(string address)
        {
            var geocoder = new Geocoder();
            var positions = await geocoder.GetPositionsForAddressAsync(address);

            var position = positions.FirstOrDefault();

            LostItem.Latitude = position.Latitude;
            LostItem.Longitude = position.Longitude;

            onPropertyChanged(nameof(LostItem));

            return position;
        }

        public async Task<string> GetAddressForPosition(Position position)
        {
            var geocoder = new Geocoder();
            var addresses = await geocoder.GetAddressesForPositionAsync(new Position(position.Latitude, position.Longitude));

            var address = addresses.FirstOrDefault();

            LostItem.LoseAddress = address;

            onPropertyChanged(nameof(LostItem));

            return address;
        }
    }
}
