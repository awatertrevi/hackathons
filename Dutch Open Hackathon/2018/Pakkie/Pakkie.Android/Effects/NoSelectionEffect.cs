using System;
using Pakkie.Droid.Effects;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(NoSelectionPlatformEffect), "NoSelectionEffect")]
namespace Pakkie.Droid.Effects
{
    public class NoSelectionPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ((Android.Widget.ListView)this.Control).SetSelector(Resource.Layout.no_ripple_selector);
        }

        protected override void OnDetached()
        {
            ((Android.Widget.ListView)this.Control).SetSelector(Android.Resource.Drawable.ListSelectorBackground); // TODO: Test if this servers its intended purpose.
        }
    }
}
