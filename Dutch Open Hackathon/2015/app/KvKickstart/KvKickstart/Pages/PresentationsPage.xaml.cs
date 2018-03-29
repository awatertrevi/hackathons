using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Models;

namespace DOH2015
{
	public partial class PresentationsPage : ContentPage
	{
		public PresentationsPage (City city)
		{
			InitializeComponent ();
			BindingContext = new PresentationsPageViewModel(this.Navigation, city);
			Presentaties.ItemSelected += Presentaties_ItemSelected;
		}

		async void Presentaties_ItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Presentation;
			((ListView)sender).SelectedItem = null;
			if(item != null)
				await Navigation.PushAsync (new PresentationsDetailPage (item));
		}
	}
}
