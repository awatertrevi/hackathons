using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.CoreKit;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using NControl.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: ResolutionGroupName("FlexWork")]
namespace FlexWork.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		private const string facebookAppId = "1436857723033067";
		private const string facebookAppName = "FlexWork";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var tint = UIColor.FromRGB(172, 20, 90);

			UINavigationBar.Appearance.BarTintColor = tint;
			UINavigationBar.Appearance.TintColor = UIColor.White;

			UISegmentedControl.Appearance.TintColor = tint;

			UITabBar.Appearance.SelectedImageTintColor = tint;

			UISwitch.Appearance.OnTintColor = tint;

			Xamarin.Forms.Forms.Init();
			Xamarin.FormsMaps.Init();

			ImageCircleRenderer.Init();

			NControlViewRenderer.Init();

			LoadApplication(new App());

			Settings.AppID = facebookAppId;
			Settings.DisplayName = facebookAppName;

			ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

			return base.FinishedLaunching(app, options);
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}
	}
}
