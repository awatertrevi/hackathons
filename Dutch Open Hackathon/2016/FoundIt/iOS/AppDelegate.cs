//
// AppDelegate.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.iOS;
using Facebook.CoreKit;
using FoundIt.Services.Media;
using FoundIt.iOS.Services.Media;

[assembly: ResolutionGroupName("FoundIt")]
namespace FoundIt.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private const string facebookAppId = "395178897486281";
        private const string facebookAppName = "FoundIt";

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Xamarin.FormsMaps.Init();

            var tint = UIColor.FromRGB(255, 193, 0);

            UINavigationBar.Appearance.BarTintColor = tint;
            UINavigationBar.Appearance.TintColor = UIColor.White;

            UISegmentedControl.Appearance.TintColor = tint;

            UITabBar.Appearance.SelectedImageTintColor = tint;

            UISwitch.Appearance.OnTintColor = tint;

            DependencyService.Register<IMediaPicker, MediaPicker>();

            ImageCircleRenderer.Init();

            Settings.AppID = facebookAppId;
            Settings.DisplayName = facebookAppName;

            LoadApplication(new App());


            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }
    }
}
