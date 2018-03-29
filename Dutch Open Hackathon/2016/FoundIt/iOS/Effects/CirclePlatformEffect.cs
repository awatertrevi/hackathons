//
// CirclePlatformEffect.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using FoundIt.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MapKit;
using System.Linq;
using FoundIt.Effects;
using UIKit;

[assembly: ExportEffect(typeof(CirclePlatformEffect), "CircleEffect")]
namespace FoundIt.iOS.Effects
{
    public class CirclePlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = this.Element.Effects.OfType<CircleEffect>().Single();
            ((MKMapView)this.Control).AddOverlay(MKCircle.Circle(new CoreLocation.CLLocationCoordinate2D(effect.Lat, effect.Lon), effect.Radius));

            ((MKMapView)this.Control).OverlayRenderer = (mapView, overlay) =>
            {
                if (overlay is MKCircle)
                    return new MKCircleRenderer(overlay as MKCircle)
                    {
                        FillColor = Color.FromHex("#FFC100").ToUIColor(),
                        Alpha = 0.35f
                    };

                return null;
            };
        }

        protected override void OnDetached()
        {
            ((MKMapView)this.Control).RemoveOverlays(((MKMapView)this.Control).Overlays.ToArray());
        }
    }
}
