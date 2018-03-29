using DOH2015.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using DOH2015.Framework;
using DOH2015.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace DOH2015
{
    public class LocationPageViewModel : ViewModelBase
    {

        public string MainLabel { get { return get(() => this.MainLabel); } set { set(() => this.MainLabel, value); } }
		public List<City> Cities { get { return get (() => this.Cities); } set { set (() => this.Cities, value); } }
		public ICommand SettingsCommand { get { return get (() => this.SettingsCommand); } set { set (() => this.SettingsCommand, value); } }
		public ICommand LocationClickedCommand { get { return get (() => this.LocationClickedCommand); } set { set (() => this.LocationClickedCommand, value); } }

		public INavigation Navigation { get; set; }


        public LocationPageViewModel(INavigation navigation)
        {
			Navigation = navigation;
            MainLabel = "Locaties";
			SettingsCommand = new Command (() => {
				Console.WriteLine("Settings clicked");
			});

			LocationClickedCommand = new Command<object> (async (location) => {
				Console.WriteLine(String.Format("Location {0} clicked", location));
				var city = location as City;
				if(city != null){
					Settings.SelectedCity = city.city;
					await Navigation.PushAsync(new HomePage (city));
				}
			});
			InitData ();

			SettingsCommand = new Command (async () => await Navigation.PushAsync (new SettingsPage ()));
        }

		public async void InitData()
		{
			Cities = await KamerVanKoophandel.Cities ();
		}
    }
}
