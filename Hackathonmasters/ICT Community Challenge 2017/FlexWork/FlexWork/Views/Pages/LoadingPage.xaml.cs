using System;
using System.Collections.Generic;
using FlexWork.ViewModels;
using Xamarin.Forms;

namespace FlexWork.Views.Pages
{
	public partial class LoadingPage : ContentPage
	{
		public LoadingPage()
		{
			InitializeComponent();

			BindingContext = new LoadingPageViewModel(Navigation);

			MessagingCenter.Subscribe<LoadingPageViewModel, string>(this, "Error", async (sender, message) =>
			{
				await DisplayAlert("Oeps!", message, "OK");
			});
		}
	}
}
