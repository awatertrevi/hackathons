using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FlexWork.Utilities.Helpers;
using FlexWork.Models;
using FlexWork.Effects;

namespace FlexWork.Views.Pages
{
	public partial class CheckoutPage : ContentPage
	{
		public CheckoutPage(Workspace workspace)
		{
			InitializeComponent();

			tableView.Effects.Add(new NoHeaderTableViewEffect());

			if (!string.IsNullOrEmpty(workspace.Description))
				description.Margin = new Thickness(15, 12.5);

			BindingContext = workspace;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			SettingsHandler.CheckinId = Guid.Empty;
			SettingsHandler.CheckinLocation = null;

			await NavigationHandler.PopAsync(Navigation);
		}
	}
}
