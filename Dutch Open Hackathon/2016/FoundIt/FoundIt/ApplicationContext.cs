//
// ApplicationContext.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using FoundIt.ComponentModel;
using System.Collections.ObjectModel;
using FoundIt.Models;
using System.Net.Http;
using System.Threading.Tasks;
using FoundIt.Utilities.Helpers;
using Newtonsoft.Json;
using System.Globalization;
#if __ANDROID__
using Xamarin.Facebook.Login;
#elif __IOS__
using Facebook.LoginKit;
#endif

namespace FoundIt
{
    public static class ApplicationContext
    {
        public static Context Current => current ?? (current = new Context());

        private static Context current;
    }

    public partial class Context : ObservableBase
    {
        public delegate void UserChanged(User user);
        public event UserChanged CurrentUserChanged;

        public User CurrentUser
        {
            get { return get(() => this.CurrentUser); }

            set
            {
                set(() => this.CurrentUser, value);

                CurrentUserChanged?.Invoke(value);
            }
        }

        public ObservableCollection<LostItem> LostItems { get { return get(() => this.LostItems); } set { set(() => this.LostItems, value); } }

        /// <summary>
        /// Gets the current user based of the access token.
        /// </summary>
        /// <returns>The current user.</returns>
        public async Task<RestResult<User>> GetCurrentUser()
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/", HttpMethod.Get);
            var user = JsonConvert.DeserializeObject<User>(result != null ? await result.Content.ReadAsStringAsync() : "");

            CurrentUser = user;

            return new RestResult<User>(user, result == null ? false : result.IsSuccessStatusCode);
        }

        public async Task<bool> AddLostItem(LostItem lostItem)
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/lostitems", HttpMethod.Post, lostItem);

            if (result != null && result.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<RestResult<ObservableCollection<LostItem>>> GetMyLostItems()
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/lostitems/", HttpMethod.Get);
            ObservableCollection<LostItem> lostItems = null;

            if (result != null && result.IsSuccessStatusCode)
                lostItems = JsonConvert.DeserializeObject<ObservableCollection<LostItem>>(result != null ? await result.Content.ReadAsStringAsync() : "");

            return new RestResult<ObservableCollection<LostItem>>(lostItems, result == null ? false : result.IsSuccessStatusCode);
        }

        public async Task<RestResult<ObservableCollection<FoundItem>>> GetMyRetrievalRequests()
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/lostitems/found/", HttpMethod.Get);
            ObservableCollection<FoundItem> foundItems = null;

            if (result != null && result.IsSuccessStatusCode)
                foundItems = JsonConvert.DeserializeObject<ObservableCollection<FoundItem>>(result != null ? await result.Content.ReadAsStringAsync() : "");

            return new RestResult<ObservableCollection<FoundItem>>(foundItems, result == null ? false : result.IsSuccessStatusCode);
        }

        public async Task<bool> DeleteMyLostItem(LostItem lostItem)
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/lostitems/{lostItem.Id}", HttpMethod.Delete);

            if (result != null && result.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> DeleteMyFoundItem(FoundItem lostItem)
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/founditems/{lostItem.Id}", HttpMethod.Delete);

            if (result != null && result.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> AddFoundItem(FoundItem foundItem)
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/founditems/", HttpMethod.Post, foundItem);

            if (result != null && result.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<RestResult<ObservableCollection<LostItem>>> GetNearbyLostItems(double lat, double @long, double distance)
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"lostitem/nearby/{lat.ToString(CultureInfo.InvariantCulture)}/{@long.ToString(CultureInfo.InvariantCulture)}/{distance.ToString(CultureInfo.InvariantCulture)}/", HttpMethod.Get);
            ObservableCollection<LostItem> lostItems = null;

            if (result != null && result.IsSuccessStatusCode)
                lostItems = JsonConvert.DeserializeObject<ObservableCollection<LostItem>>(result != null ? await result.Content.ReadAsStringAsync() : "");

            return new RestResult<ObservableCollection<LostItem>>(lostItems, result == null ? false : result.IsSuccessStatusCode);
        }

        public async Task<RestResult<ObservableCollection<FoundItem>>> GetNearbyFoundItems(double lat, double @long, double distance)
        {
            var result = await NetworkHandler.Instance.DoRequestAsync($"founditem/nearby/{lat.ToString(CultureInfo.InvariantCulture)}/{@long.ToString(CultureInfo.InvariantCulture)}/{distance.ToString(CultureInfo.InvariantCulture)}/", HttpMethod.Get);
            ObservableCollection<FoundItem> foundItems = null;

            if (result != null && result.IsSuccessStatusCode)
                foundItems = JsonConvert.DeserializeObject<ObservableCollection<FoundItem>>(result != null ? await result.Content.ReadAsStringAsync() : "");

            return new RestResult<ObservableCollection<FoundItem>>(foundItems, result == null ? false : result.IsSuccessStatusCode);
        }

        public void SignoutFacebook()
        {
#if __ANDROID__
            LoginManager.Instance.LogOut();
#elif __IOS__
            new LoginManager().LogOut();
#endif
        }

        /// <summary>
        /// Clears the current user, selected disciplines and access token.
        /// </summary>
        public void Signout()
        {
            SettingsHandler.AccessToken = string.Empty;

            if (NetworkHandler.Instance.Client.DefaultRequestHeaders.Contains("Authorization"))
                NetworkHandler.Instance.Client.DefaultRequestHeaders.Remove("Authorization");

            CurrentUser = null;

            SignoutFacebook();
        }
    }
}
