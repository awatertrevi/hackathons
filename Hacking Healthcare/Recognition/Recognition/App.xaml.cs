﻿using Xamarin.Forms;
using Recognition.Views;

namespace Recognition
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new LoadingScreen();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
