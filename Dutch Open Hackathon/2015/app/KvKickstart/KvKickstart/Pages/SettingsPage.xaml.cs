using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Framework;
using System.Linq;

namespace DOH2015
{
	public partial class SettingsPage : ContentPage
	{
		SettingsPageViewModel ViewModel { get { return (SettingsPageViewModel)BindingContext; } }

		public SettingsPage ()
		{
			InitializeComponent ();
			BindingContext = new SettingsPageViewModel (Navigation)
			{
				SelectCityCommand = new Command (async () => {
					var steden = await KamerVanKoophandel.Cities ();
					var result = await DisplayActionSheet ("Selecteer een stad", "Annuleer", null, steden.Select(x=>x.city).ToArray());
					if (result != "Annuleer") {
						city.Text = result;
						Settings.SelectedCity = result;
					}
				})
			};
		}

		async void City_Tapped (object sender, EventArgs e)
		{
			var steden = await KamerVanKoophandel.Cities ();
			var result = await DisplayActionSheet ("Selecteer een stad", "Annuleer", null, steden.Select(x=>x.city).ToArray());
			if (result != "Annuleer") {
				city.Text = result;
				Settings.SelectedCity = result;
			}
		}
	}
}

