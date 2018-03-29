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

[assembly: ExportRenderer(typeof(Entry), typeof(ExtendedEntryRenderer))]
namespace FoundIt.iOS.CustomRenderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
