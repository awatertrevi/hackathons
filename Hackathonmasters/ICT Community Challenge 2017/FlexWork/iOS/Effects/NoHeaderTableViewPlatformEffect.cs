using System;
using System.Linq;
using CoreLocation;
using FlexWork.iOS.Effects;
using MapKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;

[assembly: ExportEffect(typeof(NoHeaderTableViewPlatformEffect), "NoHeaderTableViewEffect")]
namespace FlexWork.iOS.Effects
{
	public class NoHeaderTableViewPlatformEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			((UITableView)this.Control).TableHeaderView = new UIView(new CGRect(0.0, 0.0, 0.0, 0.1));
		}

		protected override void OnDetached()
		{
			((UITableView)this.Control).TableHeaderView = null;
		}
	}
}
