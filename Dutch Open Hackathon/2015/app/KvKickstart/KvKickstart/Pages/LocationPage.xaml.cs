using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DOH2015.Framework;
using DOH2015.Models;

namespace DOH2015
{
	public partial class LocationPage : ContentPage
	{
		public LocationPageViewModel ViewModel { get { return (LocationPageViewModel)BindingContext; } }
		public LocationPage ()
		{
			InitializeComponent ();
			BindingContext = new LocationPageViewModel(this.Navigation);
			if (Settings.SelectedCity != null) {
				var city = KamerVanKoophandel.City (Settings.SelectedCity);
				if (ViewModel.LocationClickedCommand.CanExecute (city)) {
					ViewModel.LocationClickedCommand.Execute (city);
				}
			}
			Locaties.ItemTapped += Locaties_ItemTapped;
		}

		void Locaties_ItemTapped (object sender, ItemTappedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
			if (ViewModel.LocationClickedCommand.CanExecute (null)) {
				ViewModel.LocationClickedCommand.Execute (e.Item);
			}
		}
	}
}
