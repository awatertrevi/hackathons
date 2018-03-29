using System;
using Recognition.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using Recognition.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ModernHttpClient;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Recognition.ViewModels
{
	public class ContactsPageViewModel : ViewModelBase
	{
		public ObservableCollection<Person> Persons { get { return get(() => this.Persons); } set { set(() => this.Persons, value); } }
		public ICommand GetPersons { get { return get(() => this.GetPersons); } set { set(() => this.GetPersons, value); } }

		public ContactsPageViewModel(INavigation navigation) : base(navigation)
		{
			GetPersons = new Command(async () =>
			{
				IsLoading = true;

				var client = new HttpClient(new NativeMessageHandler());

				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

				var requestResult = await client.GetAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/hackathon/persons");

				Persons = JsonConvert.DeserializeObject<ObservableCollection<Person>>(await requestResult.Content.ReadAsStringAsync());

				IsLoading = false;
			});
		}
	}
}