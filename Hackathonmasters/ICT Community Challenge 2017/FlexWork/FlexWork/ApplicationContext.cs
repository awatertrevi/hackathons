using System;
using FlexWork.ComponentModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using FlexWork.Utilities.Helpers;
using FindItApp.Utilities;
using FlexWork.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;

#if __ANDROID__
using Xamarin.Facebook.Login;
#elif __IOS__
using Facebook.LoginKit;
#endif

namespace FlexWork
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

		/// <summary>
		/// Gets the current user based of the access token.
		/// </summary>
		/// <returns>The current user.</returns>
		public async Task<RestResult<User>> GetCurrentUser()
		{
			var result = await NetworkHandler.Instance.DoRequestAsync($"users/me/", HttpMethod.Get);

			if (result != null && result.IsSuccessStatusCode)
				CurrentUser = JsonConvert.DeserializeObject<User>(result != null ? await result.Content.ReadAsStringAsync() : "");

			else
				CurrentUser = null;

			return new RestResult<User>(CurrentUser, result != null && result.IsSuccessStatusCode, await GetErrorMessage(result));
		}

		public async Task<RestResult<ObservableCollection<Workspace>>> GetNearbyWorkspaces(double latitude, double longitude, double distance)
		{
			var result = await NetworkHandler.Instance.DoRequestAsync($"work-spaces/nearby/{latitude.ToString(CultureInfo.InvariantCulture)}/{longitude.ToString(CultureInfo.InvariantCulture)}/{distance.ToString(CultureInfo.InvariantCulture)}/", HttpMethod.Get);
			ObservableCollection<Workspace> workspaces = null;

			if (result != null && result.IsSuccessStatusCode)
				workspaces = JsonConvert.DeserializeObject<ObservableCollection<Workspace>>(result != null ? await result.Content.ReadAsStringAsync() : "");

			return new RestResult<ObservableCollection<Workspace>>(workspaces, result?.IsSuccessStatusCode ?? false, await GetErrorMessage(result));
		}

		public async Task<RestResult<ObservableCollection<Workspace>>> GetRecentWorkspaces()
		{
			var result = await NetworkHandler.Instance.DoRequestAsync($"work-spaces/recent/", HttpMethod.Get);
			ObservableCollection<Workspace> workspaces = null;

			if (result != null && result.IsSuccessStatusCode)
				workspaces = JsonConvert.DeserializeObject<ObservableCollection<Workspace>>(result != null ? await result.Content.ReadAsStringAsync() : "");

			return new RestResult<ObservableCollection<Workspace>>(workspaces, result?.IsSuccessStatusCode ?? false, await GetErrorMessage(result));
		}

		public async Task<RestResult<Workspace>> GetWorkspace(Guid id)
		{
			var result = await NetworkHandler.Instance.DoRequestAsync($"work-spaces/{id}/", HttpMethod.Get);
			Workspace workspace = null;

			if (result != null && result.IsSuccessStatusCode)
				workspace = JsonConvert.DeserializeObject<Workspace>(result != null ? await result.Content.ReadAsStringAsync() : "");

			return new RestResult<Workspace>(workspace, result?.IsSuccessStatusCode ?? false, await GetErrorMessage(result));
		}

		public User CurrentUser
		{
			get { return get(() => this.CurrentUser); }

			set
			{
				set(() => this.CurrentUser, value);

				CurrentUserChanged?.Invoke(value);
			}
		}

		private async Task<string> GetErrorMessage(HttpResponseMessage result)
		{
			if (result == null || result.IsSuccessStatusCode)
				return null;

			var content = await result.Content.ReadAsStringAsync();

			if (string.IsNullOrEmpty(content))
				return null;

			var jsonObject = JObject.Parse(content);

			return jsonObject["Message"].ToString();
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

			App.SetTabTo(TabPages.Map);
		}

		public void SignoutFacebook()
		{
#if __ANDROID__
			LoginManager.Instance.LogOut();
#elif __IOS__
			new LoginManager().LogOut();
#endif
		}
	}
}
