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
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FoundIt.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public ICommand NavigateBack { get { return get(() => this.NavigateBack); } set { set(() => this.NavigateBack, value); } }

        public LoginPageViewModel(INavigation navigation) : base(navigation)
        {
            NavigateBack = new Command(async () =>
            {
                await NavigationHandler.PopModalAsync(Navigation);
            });
        }

        public async Task<bool> DispatchFacebookToken(string token)
        {
            var content = new StringContent($"access_token={token}&grant_type=facebook&platform={Device.OS}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = NetworkHandler.Instance.Client.PostAsync($"{NetworkHandler.BaseUrl}/oauth2/token/", content).Result;

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());

            SettingsHandler.AccessToken = jsonObject["access_token"].ToString();

            var userRestPair = await ApplicationContext.Current.GetCurrentUser();

            if (userRestPair.Success == false)
                MessagingCenter.Send(this, "Error", "De gebruiker kon niet geladen worden, controleer je internet verbinding of probeer het later opnieuw.");

            return true;
        }
    }
}
