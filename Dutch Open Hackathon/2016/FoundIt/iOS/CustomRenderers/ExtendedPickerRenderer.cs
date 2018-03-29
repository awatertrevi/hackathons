//
// ExtendedEntryRenderer.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/6/2016
//
using System;
using CoreGraphics;
using FoundIt.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(ExtendedPickerRenderer))]
namespace FoundIt.iOS.CustomRenderers
{
    public class ExtendedPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
