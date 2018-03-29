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
	public partial class LoadingPageViewModel
	{
		public LoadingPageViewModel(INavigation navigation) : base(navigation)
		{
			var rootPage = new NavigationPage(new RootPage())
			{
				BarTextColor = Color.White
			};

			LoadUserData = new Command(async () =>
			{
				if (!string.IsNullOrEmpty(SettingsHandler.AccessToken))
				{
					if (await LoadUser() == false)
					{
						SetMainPage(rootPage, "De gebruiker kon niet geladen worden, controleer je internet verbinding of probeer het later opnieuw.");
						return;
					}
				}

				SetMainPage(rootPage);
			});

			if (LoadUserData.CanExecute(null))
				LoadUserData.Execute(null);
		}

		private void SetMainPage(Page page, string error = null)
		{
			if (error != null)
				MessagingCenter.Send(this, "Error", error);

			Device.BeginInvokeOnMainThread(() =>
			{
				Application.Current.MainPage = page;
			});
		}

		private async Task<bool> LoadUser()
		{
			var restResult = await ApplicationContext.Current.GetCurrentUser();

			return restResult.Success;
		}
	}
}
