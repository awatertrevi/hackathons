using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Recognition.ViewModels;
using Recognition.Models;
using System.Net.Http;
using ModernHttpClient;
using System.Linq;
using System.Threading.Tasks;

namespace Recognition.Views
{
	public partial class ContactsPage : ContentPage
	{
		private ContactsPageViewModel ViewModel => (ContactsPageViewModel)BindingContext;

		public ContactsPage()
		{
			InitializeComponent();

			BindingContext = new ContactsPageViewModel(Navigation);
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (ViewModel.GetPersons.CanExecute(null))
				ViewModel.GetPersons.Execute(null);

			await TrainSystem();
		}

		public async Task TrainSystem()
		{
			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var requestResult = await client.PostAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/hackathon/train", null);
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			var mi = (MenuItem)sender;
			var person = mi.CommandParameter as Person;

			var client = new HttpClient(new NativeMessageHandler());

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd21cc8207fd46828208c9b8172f5328");

			var requestResult = await client.DeleteAsync("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/hackathon/persons/" + person.personId);
			var personToDelete = ViewModel.Persons.Single(p => p.personId == person.personId);
			ViewModel.Persons.Remove(personToDelete);
		}
	}
}
