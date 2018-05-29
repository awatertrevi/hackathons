using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pakkie.Services.KPN
{
    public class SmsService
    {
        private string consumerKey = "0ZBgv2FSBvnpNDd3AQxw1zuzYrrc0ExH";
        private string consumerSecret = "KsQBYh9fyhVu2riA";
        private Uri authUri = new Uri("https://api-prd.kpn.com/oauth/client_credential/accesstoken?grant_type=client_credentials");
        private Uri sendUri = new Uri("https://api-prd.kpn.com/messaging/sms-kpn/v1/send");
        private HttpClient client = new HttpClient();
        private bool isInitialized = false;

        public async Task Init()
        {
            var token = await GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            isInitialized = true;
        }

        public async Task SendSms(string phonenumber, string message)
        {
            if (!isInitialized)
            {
                await Init();
            }

            var msg = new SmsModel
            {
                Messages = new[]
                {
                    new Message(phonenumber, message)
                },
                Sender = "Pakkie"
            };
            var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
            await client.PostAsync(sendUri, content);
        }

        private async Task<string> GetToken()
        {
            try
            {
                var res = await client.PostAsync(authUri, new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("client_id", consumerKey),
                        new KeyValuePair<string, string>("client_secret", consumerSecret)
                    }));
                var result = JObject.Parse(res.Content.ToString());
                return result["access_token"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
