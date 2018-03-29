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
using System.Windows.Input;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
#if __IOS__
using UIKit;
#elif __ANDROID__
using Android.App;
using Java.Net;
#endif

namespace FoundIt.Utilities.Helpers
{
    public class NetworkHandler
    {
        public static string BaseUrl = "http://modision.com/";
        //public static string BaseUrl = "http://44ac0037.ngrok.io/";

        public HttpClient Client;

        private static NetworkHandler instance;
        public static NetworkHandler Instance => instance ?? new NetworkHandler();

        public NetworkHandler()
        {
            Client = new HttpClient(new NativeMessageHandler());
            Client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<HttpResponseMessage> DoRequestAsync(string route, HttpMethod method, object parameter = null)
        {
            if (!Client.DefaultRequestHeaders.Contains("Authorization") && !string.IsNullOrEmpty(SettingsHandler.AccessToken))
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {SettingsHandler.AccessToken}");

            if (!Client.DefaultRequestHeaders.Contains("apikey"))
                Client.DefaultRequestHeaders.Add("apikey", "AddssgK2oD5gTpFX3GtU3iPS24PRBJMu");

            try
            {
                switch (method.ToString())
                {
                    case "GET":
                        return await Client.GetAsync(BaseUrl + route);

                    case "PUT":
                        return await Client.PutAsync(BaseUrl + route, new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json"));

                    case "POST":
                        return await Client.PostAsync(BaseUrl + route, new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json"));

                    case "DELETE":
                        return await Client.DeleteAsync(BaseUrl + route);

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