using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Recognition.Views
{
	public partial class LoadingScreen : ContentPage
	{
		public LoadingScreen()
		{
			InitializeComponent();

			Device.BeginInvokeOnMainThread(async () =>
			{
				await Task.Delay(1500);

				Application.Current.MainPage = new NavigationPage(new RootPage())
				{
					BarTextColor = Color.White,
					Icon = "add.png"
				};
			});
		}
	}
}
