using System;
using DOH2015.ComponentModel;
using Xamarin.Forms;
using DOH2015.Models;
using System.Windows.Input;

namespace DOH2015
{
	public class HomePageViewModel : ViewModelBase
	{
		public City SelectedCity { get { return get (() => this.SelectedCity); } set { set (() => this.SelectedCity, value); } }
		public INavigation Navigation { get; set; }
		public ICommand SettingsCommand { get { return get (() => this.SettingsCommand); } set { set (() => this.SettingsCommand, value); } }
		public ICommand NavigateToPresentations { get { return get (() => this.NavigateToPresentations); } set { set (() => this.NavigateToPresentations, value); } }
		public ICommand NavigateToIndoors { get { return get (() => this.NavigateToIndoors); } set { set (() => this.NavigateToIndoors, value); } }
		public ICommand NavigateToSchedule { get { return get (() => this.NavigateToSchedule); } set { set (() => this.NavigateToSchedule, value); } }
		public ICommand NavigateToMyCompany { get { return get (() => this.NavigateToMyCompany); } set { set (() => this.NavigateToMyCompany, value); } }
		public ICommand NavigateToSettings { get { return get (() => this.NavigateToSettings); } set { set (() => this.NavigateToSettings, value); } }

		public HomePageViewModel (INavigation navigation, City city)
		{
			Navigation = navigation;
			SelectedCity = city;
			SettingsCommand = new Command(() =>{});
			NavigateToPresentations = new Command (async () => await Navigation.PushAsync (new PresentationsPage (SelectedCity)));
			NavigateToIndoors = new Command (async () => await Navigation.PushAsync(new IndoorsPage()));
			NavigateToSchedule = new Command (async () => await Navigation.PushAsync (new MySchemaPage ()));
			NavigateToMyCompany = new Command (async () => await Navigation.PushAsync (new MyCompanyPage ()));
			NavigateToSettings = new Command (async () => await Navigation.PushAsync (new SettingsPage ()));
		}
	}
}

