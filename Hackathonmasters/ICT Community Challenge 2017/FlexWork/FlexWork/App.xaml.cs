using Xamarin.Forms;
using FindItApp.Utilities;
using System;
using FlexWork.Views.Pages;

namespace FlexWork
{
	public partial class App : Application
	{
		public static string PrivacyPolicy = "http://flexwork.space/privacy.html";

		public static Action<string> PostSuccessFacebookAction { get; set; }

		public App()
		{
			InitializeComponent();

			MainPage = new LoadingPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts.
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps.
		}

		protected override void OnResume()
		{
			// Handle when your app resumes.
		}

		/// <summary>
		/// Sets the tab in the RootPage.
		/// </summary>
		/// <param name="tab">Which tab you want.</param>
		public static void SetTabTo(TabPages tab)
		{
			var mainPage = Current.MainPage as NavigationPage;

			if (mainPage != null)
			{
				var rootPage = mainPage.CurrentPage as TabbedPage;

				if (rootPage != null)
					rootPage.CurrentPage = rootPage.Children[(int)tab];
			}
		}

		public static void SetBarColor(Color color)
		{
			var mainPage = Current.MainPage as NavigationPage;

			if (mainPage != null)
				mainPage.BarTextColor = color;
		}
	}
}
