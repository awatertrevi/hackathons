//
// LoginPageViewModel.Controller.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FindItApp.Utilities;
using FlexWork.Utilities.Helpers;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace FlexWork.ViewModels
{
	public partial class LoginPageViewModel
	{
		public LoginPageViewModel(INavigation navigation) : base(navigation)
		{
			NavigateToPrivacyPolicy = new Command(() =>
			{
				Device.OpenUri(new Uri(App.PrivacyPolicy));
			});

			NavigateBack = new Command(async () =>
			{
				App.SetTabTo(TabPages.Map);

				await NavigationHandler.PopModalAsync(Navigation, Color.White);
			});
		}

		public async Task<bool> DispatchFacebookToken(string token)
		{
			var content = new StringContent($"access_token={token}&grant_type=facebook&platform={Device.OS}", Encoding.UTF8, "application/x-www-form-urlencoded");
			HttpResponseMessage response = null;

			try
			{
				response = NetworkHandler.Instance.Client.PostAsync($"http://flexwork.space/oauth2/token/", content).Result;
			}

			catch
			{
				return false;
			}

			if (response == null || response.StatusCode != HttpStatusCode.OK)
				return false;

			var jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());

			SettingsHandler.AccessToken = jsonObject["access_token"].ToString();

			var userRestResult = await ApplicationContext.Current.GetCurrentUser();

			if (userRestResult.Success == false)
				MessagingCenter.Send(this, "Error", "De gebruiker kon niet geladen worden, controleer je internet verbinding of probeer het later opnieuw.");

			return true;
		}
	}
}
