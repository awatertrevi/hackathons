using System;
using Recognition.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Recognition.Utilities;
using Recognition.Views;
using BigTed;
using Realms;
using Recognition.Models;

namespace Recognition.ViewModels
{
	public class NewContactPageViewModel : ViewModelBase
	{
		public ICommand SaveContact { get { return get(() => this.SaveContact); } set { set(() => this.SaveContact, value); } }

		public string Name { get { return get(() => this.Name); } set { set(() => this.Name, value); } }
		public string Relationship { get { return get(() => this.Relationship); } set { set(() => this.Relationship, value); } }

		public byte[] Image { get { return get(() => this.Image); } set { set(() => this.Image, value); } }

		public string PersonId { get { return get(() => this.PersonId); } set { set(() => this.PersonId, value); } }

		private Realm realmInstance = Realm.GetInstance();

		public NewContactPageViewModel(INavigation navigation) : base(navigation)
		{
			SaveContact = new Command(async () =>
			{
				BTProgressHUD.Show("Saving...", maskType: ProgressHUD.MaskType.Black);

				if (string.IsNullOrEmpty(Name))
				{
					MessagingCenter.Send<NewContactPageViewModel, string>(this, "Error", "Please enter a name.");
					BTProgressHUD.Dismiss();
					return;
				}

				if (string.IsNullOrEmpty(Relationship))
				{
					MessagingCenter.Send<NewContactPageViewModel, string>(this, "Error", "Please enter a relationship.");
					BTProgressHUD.Dismiss();
					return;
				}

				if (Image == null)
				{
					MessagingCenter.Send<NewContactPageViewModel, string>(this, "Error", "Please select an image.");
					BTProgressHUD.Dismiss();
					return;
				}

				if (string.IsNullOrEmpty(PersonId))
					PersonId = await AddPerson();

				await AddPhotoForPerson(PersonId);

				BTProgressHUD.Dismiss();
			});
		}

		private async Task<string> AddPerson()
		{
			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var json = new
			{
				name = Name,
				userData = Relationship
			};

			var requestResult = await client.PostAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/hackathon/persons", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));

			if (!requestResult.IsSuccessStatusCode)
				return null;

			var result = JObject.Parse(await requestResult.Content.ReadAsStringAsync());

			return result.Value<string>("personId");
		}

		private async Task AddPhotoForPerson(string personId)
		{
			var client = new HttpClient(new NativeMessageHandler());
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var content = new ByteArrayContent(Image);

			content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			var requestResult = await client.PostAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/hackathon/persons/" + personId + "/persistedFaces", content);

			if (requestResult.IsSuccessStatusCode)
			{
				var personImage = new SavedPersonImage()
				{
					image = Convert.ToBase64String(Image),
					personId = personId
				};

				realmInstance.Write(() =>
				{
					realmInstance.Add(personImage);
				});

				await NavigationHandler.PopAsync(Navigation);
			}

			else
			{
				var error = JObject.Parse(await requestResult.Content.ReadAsStringAsync());

				MessagingCenter.Send<NewContactPageViewModel, string>(this, "Error", error["error"]["message"].ToString());
			}
		}
	}
}
