using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using DOH2015;
using DOH2015.StaticResources;

namespace DOH2015
{
	public class App : Application
	{
        public new static App Current { get; private set; }
		public static NavigationPage NavigationPage { get ; private set; }
		public App ()
		{
            Current = this;
			NavigationPage = new NavigationPage (new LocationPage ());
			MainPage = NavigationPage;
			//HACK: Otherwise the linker links the staticresources away
			var a = new Initializer ();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
