using System;
using DOH2015.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using DOH2015.Models;
using DOH2015.Framework;
using System.Linq;

namespace DOH2015
{
	public class MySchemaPageViewModel : ViewModelBase
	{
		INavigation Navigation { get; set; }
		public List<Presentation> AttendingPresentations {get { return get (() => this.AttendingPresentations); } set { set (() => this.AttendingPresentations, value); }}

		public MySchemaPageViewModel (INavigation navigation)
		{
			Navigation = navigation;
			if (!String.IsNullOrWhiteSpace (Settings.SelectedPresentations)) 
			{
				var selected = Settings.SelectedPresentations
					.TrimEnd (";".ToCharArray ())
					.Split (";".ToCharArray ()).Select (x => int.Parse (x)).ToList ();
				AttendingPresentations = KamerVanKoophandel.Presentations (Settings.SelectedCity, selected);
			}
		}
	}
}

