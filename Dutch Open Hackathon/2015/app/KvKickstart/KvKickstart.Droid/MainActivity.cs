using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DOH2015.Droid
{
	[Activity (Theme = "@style/CustomTheme", ScreenOrientation = ScreenOrientation.Portrait, Label = "@string/app_name", Icon = "@drawable/logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new DOH2015.App ());
		}
	}
}

