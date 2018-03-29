using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Models;

namespace DOH2015
{
	public partial class HomePage : ContentPage
	{
		public HomePage (City selectedCity)
		{
			InitializeComponent ();
			BindingContext = new HomePageViewModel (Navigation, selectedCity);
		}
	}
}

