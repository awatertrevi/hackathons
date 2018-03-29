using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FlexWork.Utilities.Helpers;

namespace FlexWork.Views.Pages
{
	public partial class WiFiAccessGainedPage : ContentPage
	{
		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			await NavigationHandler.PopModalAsync(Navigation, Color.White);
		}

		public WiFiAccessGainedPage()
		{
			InitializeComponent();
		}
	}
}
