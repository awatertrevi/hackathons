using System;
using System.Collections.Generic;
using FlexWork.Utilities.Helpers;
using FlexWork.ViewModels;
using Xamarin.Forms;
using FlexWork.Models;

namespace FlexWork.Views.Pages
{
	public partial class WorkspacesPage : ContentPage
	{
		private WorkspacesPageViewModel ViewModel => (WorkspacesPageViewModel)BindingContext;

		public WorkspacesPage()
		{
			InitializeComponent();

			BindingContext = new WorkspacesPageViewModel(Navigation);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!string.IsNullOrEmpty(SettingsHandler.CheckinLocation))
			{
				checkinText.Text = "Momenteel ingecheckt bij: " + SettingsHandler.CheckinLocation;
				checkinActive.IsVisible = true;
			}

			else
			{
				checkinActive.IsVisible = false;
			}
		}

		private void SegmentedControlSelectedIndexChanged(object sender, SelectedItemChangedEventArgs e)
		{
			ViewModel.DirtySelection = e.SelectedItem.ToString();

			if (ViewModel.LoadWorkspaces.CanExecute(null))
				ViewModel.LoadWorkspaces.Execute(null);
		}

		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			var workspace = await ApplicationContext.Current.GetWorkspace(SettingsHandler.CheckinId);

			await NavigationHandler.PushAsync(Navigation, new CheckoutPage(workspace.Result));
		}


		private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			await NavigationHandler.PushAsync(Navigation, new FlexWorkspaceDetailPage(e.Item as Workspace));
		}
	}
}
