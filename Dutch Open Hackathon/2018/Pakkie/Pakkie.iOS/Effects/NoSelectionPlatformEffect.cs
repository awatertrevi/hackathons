using System;
using Pakkie.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportEffect(typeof(NoSelectionPlatformEffect), "NoSelectionEffect")]
namespace Pakkie.iOS.Effects
{
    public class NoSelectionPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ((UITableView)this.Control).AllowsSelection = false;
        }

        protected override void OnDetached()
        {
            ((UITableView)this.Control).AllowsSelection = true;
        }
    }
}
