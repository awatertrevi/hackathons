//
// SegmentedControlRenderer.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Linq;
using System.ComponentModel;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FoundIt.iOS.CustomRenderers;
using FoundIt.Views.Controls;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace FoundIt.iOS.CustomRenderers
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, UISegmentedControl>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                UISegmentedControl uiSegmentedControl = new UISegmentedControl();

                uiSegmentedControl.ValueChanged += Control_PropertyChanged;

                SetNativeControl(uiSegmentedControl);
            }

            UpdateControlSegments();

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= Element_PropertyChanged;

            if (e.NewElement != null)
                Element.PropertyChanged += Element_PropertyChanged;
        }

        void UpdateControlSegments()
        {
            Control.RemoveAllSegments();

            if (Element == null)
                return;

            for (var i = 0; i < Element.Children.Count; i++)
            {
                Control.InsertSegment((string)Element.Children[i].Text, i, false);
                if ((string)Element.Children[i].Text == Element.SelectedValue)
                {
                    Control.ValueChanged -= Control_PropertyChanged;
                    Control.SelectedSegment = i;
                    Control.ValueChanged += Control_PropertyChanged;
                }
            }
        }

        void Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SegmentedControl.SelectedValue))
                Element_ValueChanged();

            else if (e.PropertyName == nameof(SegmentedControl.Children))
                Element_SegmentsChanged();
        }

        void Element_ValueChanged()
        {
            if (Element.Children.Count < 1)
                return;

            Control.SelectedSegment = Element.Children.IndexOf(Element.Children.Single(s => s.Text == Element.SelectedValue));
        }

        void Element_SegmentsChanged()
        {
            UpdateControlSegments();
        }

        void Control_PropertyChanged(object sender, EventArgs e)
        {
            var newValue = Control.TitleAt(Control.SelectedSegment);

            if (Element == null)
                return;

            Element.SelectedValue = newValue;
        }
    }
}
