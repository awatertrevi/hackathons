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

[assembly: ExportRenderer(typeof(DatePicker), typeof(ExtendedDatePickerRenderer))]
namespace FoundIt.iOS.CustomRenderers
{
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
