//
// NetworkHandler.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;
#if __IOS__
using UIKit;
#elif __ANDROID__
using Android.App;
using Java.Net;
#endif

namespace FlexWork.Utilities.Helpers
{
	public class NetworkHandler
	{
		private const string baseUrl = "http://flexwork.space/";

		public HttpClient Client;

		private static NetworkHandler instance;
		public static NetworkHandler Instance => instance ?? new NetworkHandler();


		public NetworkHandler()
		{
			Client = new HttpClient(new NativeMessageHandler());
			Client.Timeout = TimeSpan.FromSeconds(15);
		}

		public async Task<HttpResponseMessage> DoRequestAsync(string route, HttpMethod method, object parameter = null)
		{
			if (!Client.DefaultRequestHeaders.Contains("Authorization") && !string.IsNullOrEmpty(SettingsHandler.AccessToken))
				Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {SettingsHandler.AccessToken}");

			try
			{
				switch (method.ToString())
				{
					case "GET":
						return await Client.GetAsync(baseUrl + route);

					case "PUT":
						return await Client.PutAsync(baseUrl + route, new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json"));

					case "POST":
						return await Client.PostAsync(baseUrl + route, new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json"));

					case "DELETE":
						return await Client.DeleteAsync(baseUrl + route);

					default:
						throw new NotSupportedException();
				}
			}

			catch
			{
				return null;
			}
		}
	}
}