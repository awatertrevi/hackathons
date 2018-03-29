using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DOH2015.Models;
using System.Windows.Input;
using DOH2015.Droid;


namespace DOH2015
{
	public partial class PresentationsDetailPage : ContentPage
	{
		public PresentationsDetailPageViewModel ViewModel {get { return (PresentationsDetailPageViewModel)BindingContext; }}
		private Presentation SelectedPresentation { get; set; }

		public PresentationsDetailPage (Presentation presentation)
		{
			SelectedPresentation = presentation;
			InitializeComponent ();
			BindingContext = new PresentationsDetailPageViewModel (Navigation, presentation);
			MediaL.ItemSelected += MediaL_ItemSelected;
			if(Settings.SelectedPresentations.Contains(presentation.id.ToString())){
				ToolbarItems.Clear();
			}
		}

		void MediaL_ItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Media;
			if (item != null) {
				Device.OpenUri (new Uri (item.file, UriKind.Absolute));
			}
		}

		public async void SignUp(object sender, EventArgs args)
		{
			if (!Settings.RegisteredForEvent) {
				var noAccount = await DisplayAlert ("Aanmelden Startersdag", "Wanneer u aangeeft om aanwezig te zijn bij het programma van de startersdag moet u aangemeld zijn voor de startersdag.", "Aanmelden", "Ik ben al aangemeld!");

				if (noAccount == true) {
					await Navigation.PushAsync (new SignUpPage ());
					//HACK: Moet beter
					ToolbarItems.Clear();
					return;
				} else {
					Settings.RegisteredForEvent = true;
				}
			}
			Settings.SelectedPresentations = string.Format ("{0};{1}", ViewModel.SelectedPresentation.id, Settings.SelectedPresentations);
			ToolbarItems.Clear ();
			await DisplayAlert ("Toegevoegd aan Schema", "De presentatie is toegevoegd aan uw schema!", "Oké");
			Device.OnPlatform(Android: () => 
				NotificationServ.MyLocalNotification (SelectedPresentation.title, "Uw sessie begint over 15 minuten", DateTime.Now.AddSeconds (20)));
		}
	}
}
