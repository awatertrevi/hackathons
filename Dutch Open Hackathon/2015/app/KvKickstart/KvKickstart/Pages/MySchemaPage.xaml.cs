using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Models;

namespace DOH2015
{
	public partial class MySchemaPage : ContentPage
	{
		MySchemaPageViewModel ViewModel { get { return (MySchemaPageViewModel)BindingContext; } }

		public MySchemaPage ()
		{
			InitializeComponent ();
			presentaties.ItemSelected += Presentaties_ItemSelected;
			BindingContext = new MySchemaPageViewModel (Navigation);
		}

		async void Presentaties_ItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
			var item = e.SelectedItem as Presentation;
			if (item != null) {
				await Navigation.PushAsync (new PresentationsDetailPage (item));
			}
		}
	}
}

