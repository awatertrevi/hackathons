using System;
using What.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using UIKit;

[assembly: ExportRenderer(typeof(ListView), typeof(ExtendedListViewRenderer))]
namespace What.iOS
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            Control.ContentInset = new UIEdgeInsets(25, 0, 0, 0);
            Control.AllowsSelection = false;
        }
    }
}
