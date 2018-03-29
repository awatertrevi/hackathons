using System;
using DOH2015.ComponentModel;
using DOH2015.Models;
using Xamarin.Forms;
using DOH2015.Framework;
using System.Collections.Generic;

namespace DOH2015
{
	public class PresentationsPageViewModel : ViewModelBase
	{
		public INavigation Navigation { get; set; } 
		public City SelectedCity { get; set; }
		public List<Presentation> Presentations { get { return get (() => this.Presentations); } set { set (() => this.Presentations, value); } }

		public PresentationsPageViewModel (INavigation navigation, City selectedCity)
		{
			Navigation = navigation;
			SelectedCity = selectedCity;
			GetData ();
		}

		async void GetData()
		{
			if (SelectedCity != null) {
				Presentations = await KamerVanKoophandel.Presentations (SelectedCity.city);
			}
				
		}
	}
}

