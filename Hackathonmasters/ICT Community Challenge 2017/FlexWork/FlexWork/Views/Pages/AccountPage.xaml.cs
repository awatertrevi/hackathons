using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FlexWork.ViewModels;
using FlexWork.Models;
using System.Linq;

namespace FlexWork
{
	public partial class AccountPage : ContentPage
	{
		private AccountPageViewModel ViewModel => (AccountPageViewModel)BindingContext;

		public AccountPage()
		{
			InitializeComponent();

			BindingContext = new AccountPageViewModel(Navigation);

			foreach (var @enum in Enum.GetValues(typeof(User.Expertise)))
			{
				var item = @enum.ToString();

				expertisePicker.Items.Add(string.Join(
		string.Empty,
		item.Select((x, i) => (
			 char.IsUpper(x) && i > 0 &&
			 (char.IsLower(item[i - 1]) || (i < item.Count() - 1 && char.IsLower(item[i + 1])))
		) ? " " + x : x.ToString())));
			}
		}

		private async void StartSignout(object sender, EventArgs e)
		{
			var answer = await DisplayAlert("Weet je zeker dat je wilt uitloggen?", null, "Ja", "Nee");

			if (answer)
				ApplicationContext.Current.Signout();
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			expertisePicker.Focus();
		}

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			expertiseLabel.Text = expertisePicker.Items[expertisePicker.SelectedIndex];
		}
	}
}
