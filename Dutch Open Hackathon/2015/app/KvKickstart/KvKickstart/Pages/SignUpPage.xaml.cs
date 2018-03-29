using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Framework;
using DOH2015.Models;
using System.Linq;

namespace DOH2015
{
	public partial class SignUpPage : ContentPage
	{
		SignUpPageViewModel ViewModel { get { return (SignUpPageViewModel)BindingContext; } }

		public SignUpPage ()
		{
			InitializeComponent ();
			BindingContext = new SignUpPageViewModel (Navigation) {
				RegisterCommand = new Command(async () => {
					await DisplayAlert("Registratie succes", "U bent succesvol geregistreerd!", "Oké");
					Settings.RegisteredForEvent = true;
					await Navigation.PopAsync();
				})
			};
			foreach (var item in KamerVanKoophandel.Cities ().Result.Select (x => x.city).ToList ()) {
				locatiePicker.Items.Add (item);
			}
		}
	}
}

