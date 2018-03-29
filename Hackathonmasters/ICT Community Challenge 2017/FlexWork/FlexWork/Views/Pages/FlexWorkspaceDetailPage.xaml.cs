using System;
using System.Collections.Generic;
using Cross.Pie.Forms;
using FlexWork.ViewModels;
using Xamarin.Forms;
using FlexWork.Models;
using FlexWork.Views.Controls;
using Xamarin.Forms.Maps;
using FlexWork.Utilities.Helpers;
using FlexWork.Effects;
using System.Linq;

namespace FlexWork.Views.Pages
{
	public partial class FlexWorkspaceDetailPage : ContentPage
	{
		private FlexWorkspaceDetailPageViewModel ViewModel => (FlexWorkspaceDetailPageViewModel)BindingContext;

		public FlexWorkspaceDetailPage(Workspace workspace)
		{
			InitializeComponent();

			tableView.Effects.Add(new NoHeaderTableViewEffect());

			BindingContext = new FlexWorkspaceDetailPageViewModel(Navigation, workspace);

			var pin = new Pin()
			{
				Label = ViewModel.SelectedWorkspace.Name ?? string.Empty,
				Address = ViewModel.SelectedWorkspace.Address ?? string.Empty,
				Position = ViewModel.SelectedWorkspace.Position
			};

			pin.SetImage(new FileImageSource()
			{
				File = ViewModel.SelectedWorkspace.CompanyType == CompanyType.Corporate ? "organisation.png" : "small-business.png"
			});

			map.Pins.Add(pin);
			map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMeters(500)));

			foreach (var facility in ViewModel.SelectedWorkspace.WorkspaceFacilities)
			{
				facilitiesStack.Children.Add(new Button()
				{
					Image = facility.Facility.Icon,
					HeightRequest = 32,
					WidthRequest = 32,
					BorderRadius = 16,
					BackgroundColor = Color.FromHex("#AC145A")
				});
			}

			pie.Add(new PieItem()
			{
				Value = 14,
				Color = Color.FromHex("AC145A")
			});

			pie.Add(new PieItem()
			{
				Value = 26,
				Color = Color.FromHex("BD5887")
			});

			pie.Add(new PieItem()
			{
				Value = 39,
				Color = Color.FromHex("E28CB3")
			});

			pie.Add(new PieItem()
			{
				Value = 12,
				Color = Color.FromHex("F3C5D9")
			});

			pie.IsValueVisible = false;
			pie.IsPercentVisible = false;
			pie.Update();

			checkinBtn.IsVisible = string.IsNullOrEmpty(SettingsHandler.CheckinLocation);

			if (!string.IsNullOrEmpty(ViewModel.SelectedWorkspace.Description))
				description.Margin = new Thickness(15, 12.5);

			if (ApplicationContext.Current.CurrentUser == null)
				checkinBtn.IsVisible = false;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			SettingsHandler.CheckinLocation = $"{ViewModel.SelectedWorkspace.Name} - {ViewModel.SelectedWorkspace.FullAddress}";
			SettingsHandler.CheckinId = ViewModel.SelectedWorkspace.Id;
			checkinBtn.IsVisible = false;

			await NavigationHandler.PushModalAsync(Navigation, new WiFiAccessGainedPage(), Color.Black);
			Navigation.RemovePage(Navigation.NavigationStack.Last());
		}
	}
}
