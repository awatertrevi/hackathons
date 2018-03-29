using System;
using DOH2015.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using DOH2015.Framework;
using System.Linq;

namespace DOH2015
{
	public class SettingsPageViewModel : ViewModelBase
	{
		INavigation Navigation { get; set; }
		public string SelectedCity { get { return get (() => this.SelectedCity); } set { set (() => this.SelectedCity, value); } }
		public ICommand SelectCityCommand { get { return get (() => this.SelectCityCommand); } set { set (() => this.SelectCityCommand, value); } }

		public SettingsPageViewModel (INavigation navigation)
		{
			Navigation = navigation;
			SelectedCity = Settings.SelectedCity;
		}
	}
}

