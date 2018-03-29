using System;
using Xamarin.Forms;
using FlexWork.Views.Pages;
using Plugin.Connectivity;
using FindItApp.Utilities;
using FlexWork.Utilities.Helpers;
using FlexWork.Views.Controls;
using System.Collections.Generic;
using FlexWork.Models;

namespace FlexWork
{
	public class RootPage : TabbedPage
	{
		public RootPage()
		{
			Children.Add(new MapPage());
			Children.Add(new WorkspacesPage());
			Children.Add(new AccountPage());

			NavigationPage.SetBackButtonTitle(this, "Terug");
		}

		protected override async void OnCurrentPageChanged()
		{
			base.OnCurrentPageChanged();

			Title = CurrentPage.Title;

			if (Application.Current.MainPage != null && Application.Current.MainPage.GetType() == typeof(NavigationPage))
			{
				if (ApplicationContext.Current.CurrentUser != null)
					return;

				if (CurrentPage.GetType() == typeof(MapPage))
					return;

				if (Navigation.ModalStack.Count < 1 && CrossConnectivity.Current.IsConnected)
					await NavigationHandler.PushModalAsync(Navigation, new LoginPage(), Color.Black);

				else
				{
					await DisplayAlert("Oeps!", "Om in te loggen heb je een actieve internet verbinding nodig. Probeer het later opnieuw.", "OK");

					App.SetTabTo(TabPages.Map);
				}
			}
		}
	}
}
