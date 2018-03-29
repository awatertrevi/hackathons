//
// FoundPageViewModel.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using FoundIt.ComponentModel;
using Xamarin.Forms;
using FoundIt.Models;
using System.Windows.Input;
using FoundIt.Utilities.Helpers;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Linq;
using FoundIt.Views.Pages;

namespace FoundIt.ViewModels
{
    public class FoundPageViewModel : ViewModelBase
    {
        public FoundItem FoundItem { get { return get(() => this.FoundItem); } set { set(() => this.FoundItem, value); } }

        public ICommand SaveFoundItem { get { return get(() => this.SaveFoundItem); } set { set(() => this.SaveFoundItem, value); } }
        public ICommand OpenChat { get { return get(() => this.OpenChat); } set { set(() => this.OpenChat, value); } }

        public FoundPageViewModel(INavigation navigation, FoundItem foundItem) : base(navigation)
        {
            FoundItem = foundItem;

            FoundItem.Finder = ApplicationContext.Current.CurrentUser;

            SaveFoundItem = new Command(async () =>
            {
                if (await ApplicationContext.Current.AddFoundItem(FoundItem) == false)
                    return;

                if (Navigation.NavigationStack.Count > 2)
                    await NavigationHandler.PopAsync(Navigation);

                else
                    App.SetTabTo(0);
            });

            OpenChat = new Command(async () =>
            {
                await NavigationHandler.PushAsync(Navigation, new ChatPage(FoundItem));
            });
        }

        public async Task<string> GetAddressForPosition(Position position)
        {
            var geocoder = new Geocoder(); // TODO: Do with the DOH geocoder thingy.
            var addresses = await geocoder.GetAddressesForPositionAsync(new Position(position.Latitude, position.Longitude));

            var address = addresses.FirstOrDefault();

            FoundItem.FindAddress = address;

            onPropertyChanged(nameof(FoundItem));

            return address;
        }
    }
}
