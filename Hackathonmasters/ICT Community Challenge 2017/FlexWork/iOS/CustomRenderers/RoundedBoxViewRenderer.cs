using System;
using FlexWork.iOS.CustomRenderers;
using FlexWork.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]
namespace FlexWork.iOS.CustomRenderers
{
	public class RoundedBoxViewRenderer : VisualElementRenderer<BoxView>
	{
		private RoundedBoxView Box;

		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				Box = (RoundedBoxView)Element;

				if (Box.Shape == RoundedBoxView.ShapeName.Ring)
				{
					Layer.BorderWidth = (float)(Element.WidthRequest / 8.0f);
					Layer.BorderColor = Element.BackgroundColor.ToCGColor();
					Layer.BackgroundColor = Color.Transparent.ToCGColor();
				}

				Layer.MasksToBounds = true;
				Layer.CornerRadius = (float)((Element.WidthRequest) / 2.0f);
			}
		}
	}
}
