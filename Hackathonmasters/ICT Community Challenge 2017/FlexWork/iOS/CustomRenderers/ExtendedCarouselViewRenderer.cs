using System;
using Framework.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(CarouselView), typeof(ExtendedCarouselViewRenderer))]
namespace Framework.iOS.CustomRenderers
{
	public class ExtendedCarouselViewRenderer : CarouselViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<CarouselView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
				Control.ShowsHorizontalScrollIndicator = false;
		}
	}
}