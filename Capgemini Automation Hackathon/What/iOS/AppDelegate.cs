using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace What.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var tint = UIColor.FromRGB(232, 32, 78);

            UINavigationBar.Appearance.BarTintColor = tint;
            UINavigationBar.Appearance.TintColor = UIColor.White;

            UISegmentedControl.Appearance.TintColor = tint;

            UITabBar.Appearance.SelectedImageTintColor = tint;

            UISwitch.Appearance.OnTintColor = tint;

            UIAlertView.Appearance.TintColor = tint;

            UIView.AppearanceWhenContainedIn(typeof(UIAlertController)).TintColor = tint;
            UIView.AppearanceWhenContainedIn(typeof(UIActivityViewController)).TintColor = tint;

            Xamarin.Forms.Forms.Init();

            KeyboardOverlapRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes
                (
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );

                app.RegisterUserNotificationSettings(notificationSettings);
            }

            return base.FinishedLaunching(app, options);
        }
    }
}
