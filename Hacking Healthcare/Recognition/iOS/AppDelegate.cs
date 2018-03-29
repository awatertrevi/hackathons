using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using ImageCircle.Forms.Plugin.iOS;

namespace Recognition.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var tint = UIColor.FromRGB(10, 132, 181);

			UINavigationBar.Appearance.BarTintColor = tint;
			UINavigationBar.Appearance.TintColor = UIColor.White;

			UISegmentedControl.Appearance.TintColor = tint;

			UITabBar.Appearance.SelectedImageTintColor = tint;

			UISwitch.Appearance.OnTintColor = tint;

			global::Xamarin.Forms.Forms.Init();
			ImageCircleRenderer.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
