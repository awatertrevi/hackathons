using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoundIt.Views.Controls
{
    public class ExtendedMap : Map
    {
        public ExtendedMap()
        {
        }
    }

    public static class ExtendedPin
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.CreateAttached("Image", typeof(FileImageSource), typeof(ExtendedPin), null);

        public static FileImageSource GetImage(BindableObject bindable)
        {
            return (FileImageSource)bindable.GetValue(ImageProperty);
        }

        public static void SetImage(BindableObject bindable, FileImageSource value)
        {
            bindable.SetValue(ImageProperty, value);
        }

        public static FileImageSource GetImage(this Pin bindable)
        {
            return (FileImageSource)bindable.GetValue(ImageProperty);
        }

        public static void SetImage(this Pin bindable, FileImageSource value)
        {
            bindable.SetValue(ImageProperty, value);
        }

        ///////////////////////////////////////////

        public static readonly BindableProperty CalloutViewProperty =
          BindableProperty.CreateAttached("CalloutView", typeof(View), typeof(ExtendedPin), null);

        public static View GetCalloutView(BindableObject bindable)
        {
            return (View)bindable.GetValue(CalloutViewProperty);
        }

        public static void SetCalloutView(BindableObject bindable, View value)
        {
            bindable.SetValue(CalloutViewProperty, bindable);
        }

        public static View GetCalloutView(this Pin bindable)
        {
            return (View)bindable.GetValue(CalloutViewProperty);
        }

        public static void SetCalloutView(this Pin bindable, View value)
        {
            bindable.SetValue(CalloutViewProperty, value);
        }
    }
}
