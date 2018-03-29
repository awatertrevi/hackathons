using System.ComponentModel;
using FoundIt.iOS.CustomRenderers;
using FoundIt.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using MapKit;
using UIKit;
using Foundation;
using ObjCRuntime;
using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq.Expressions;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace FoundIt.iOS.CustomRenderers
{
    public class ExtendedMapRenderer : MapRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                ((MKMapView)this.Control).DidSelectAnnotationView += ExtendedMapRenderer_DidSelectAnnotationView;
                ((MKMapView)this.Control).GetViewForAnnotation = new MapDelegate(this).GetViewForAnnotation;

            }
        }

        public ExtendedMapRenderer()
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "VisibleRegion")
            {
                if (!calloutView.Frame.IsEmpty)
                    calloutView.DismissCallout(true);
            }
        }

        public SMCalloutView.CalloutView calloutView = SMCalloutView.CalloutView.PlatformCalloutView;

        private void ExtendedMapRenderer_DidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var elementPin = GetPin(e.View.Annotation);
            if (elementPin != null)
            {
                var platformProperty = typeof(Element).GetProperty("Platform", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //var getPlatform = new Func<Element, IPlatform>((element) => (IPlatform)platformProperty.GetValue(element));
                //var setPlatform = new Action<Element, IPlatform>((element, platform) =>platformProperty.SetValue(element,platform));
                var getPlatform = new Func<Element, object>((element) => platformProperty.GetValue(element));
                var setPlatform = new Action<Element, object>((element, platform) => platformProperty.SetValue(element, platform));

                //    this.calloutView.SetValueForKey(NSObject.FromObject(new UIEdgeInsets()), new NSString("contentViewInset"));


                // apply the MKAnnotationView's basic properties
                this.calloutView.Title = e.View.Annotation.GetTitle();
                this.calloutView.Subtitle = e.View.Annotation.GetSubtitle();

                if (elementPin.GetCalloutView() != null)
                {
                    var customView = elementPin.GetCalloutView();
                    IVisualElementRenderer renderer;
                    if ((renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(customView)) == null)
                    {
                        if (getPlatform(customView) == null)
                            setPlatform(customView, getPlatform(this.Element));

                        Xamarin.Forms.Platform.iOS.Platform.SetRenderer(customView, renderer = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer(customView));
                    }

                    customView.Layout(new Rectangle(new Point(0, 0), renderer.Element.Measure(this.Element.Width - 20, 300).Request));
                    renderer.NativeView.Frame = new CoreGraphics.CGRect(0, 0, customView.Width, customView.Height);
                    this.calloutView.ContentView = renderer.NativeView;
                    this.calloutView.ContentView.Bounds = renderer.NativeView.Frame.Inset(15, 15);
                }

                // Apply the MKAnnotationView's desired calloutOffset (from the top-middle of the view)
                this.calloutView.CalloutOffset = e.View.CalloutOffset;

                // This does all the magic.
                this.calloutView.PresentCallout(e.View.Bounds, e.View, this, true);

            }
        }

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            UIView calloutMabye = calloutView.HitTest(calloutView.ConvertPointFromView(point, this), uievent);
            if (calloutMabye is UIControl)
                return calloutMabye;
            else if (calloutMabye != null)
                return calloutView.ContentView;

            if (!calloutView.Frame.IsEmpty)
                calloutView.DismissCallout(true);

            return base.HitTest(point, uievent);
        }


        Pin GetPin(IMKAnnotation annotationObject)
        {
            NSObject annotation = Runtime.GetNSObject(annotationObject.Handle);
            if (annotation == null)
                return null;

            // lookup pin
            Pin targetPin = null;
            for (var i = 0; i < ((Map)this.Element).Pins.Count; i++)
            {
                Pin pin = ((Map)this.Element).Pins[i];
                // object target = pin.Id;
                object target = typeof(Pin).GetProperty("Id", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(pin);
                if (target != annotation)
                    continue;

                targetPin = pin;
                break;
            }
            return targetPin;
        }

        internal class MapDelegate : MKMapViewDelegate
        {
            // keep references alive, removing this will cause a segfault
            static readonly List<object> List = new List<object>();
            readonly ExtendedMapRenderer _mapRenderer;
            UIView _lastTouchedView;

            internal MapDelegate(ExtendedMapRenderer mapRenderer)
            {
                _mapRenderer = mapRenderer;
            }


            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                Pin elementPin = _mapRenderer.GetPin(annotation);
                MKAnnotationView mapPin = null;

                // https://bugzilla.xamarin.com/show_bug.cgi?id=26416
                var userLocationAnnotation = Runtime.GetNSObject(annotation.Handle) as MKUserLocation;
                if (userLocationAnnotation != null)
                    return null;

                const string defaultPinId = "defaultPin";
                mapPin = (MKAnnotationView)mapView.DequeueReusableAnnotation(defaultPinId);
                if (mapPin == null)
                {
                    mapPin = new MKAnnotationView(annotation, defaultPinId);
                    mapPin.CanShowCallout = false;
                    mapPin.Layer.AnchorPoint = new CoreGraphics.CGPoint(0.5, 1);
                }

                if (elementPin.GetImage() != null)
                    mapPin.Image = UIImage.FromFile(elementPin.GetImage().File);
                else mapPin.Image = UIImage.LoadFromData(Foundation.NSData.FromUrl(Foundation.NSUrl.FromString("http://icons.iconarchive.com/icons/icons-land/vista-map-markers/256/Map-Marker-Push-Pin-1-Pink-icon.png")), 4f);

                mapPin.Annotation = annotation;
                AttachGestureToPin(mapPin, annotation);

                return mapPin;
            }

            void AttachGestureToPin(MKAnnotationView mapPin, IMKAnnotation annotation)
            {
                UIGestureRecognizer[] recognizers = mapPin.GestureRecognizers;

                if (recognizers != null)
                {
                    foreach (UIGestureRecognizer r in recognizers)
                    {
                        mapPin.RemoveGestureRecognizer(r);
                    }
                }

                Action<UITapGestureRecognizer> action = g => OnClick(annotation, g);
                var recognizer = new UITapGestureRecognizer(action)
                {
                    ShouldReceiveTouch = (gestureRecognizer, touch) =>
                    {
                        _lastTouchedView = touch.View;
                        return true;
                    }
                };
                List.Add(action);
                List.Add(recognizer);
                mapPin.AddGestureRecognizer(recognizer);
            }

            void OnClick(object annotationObject, UITapGestureRecognizer recognizer)
            {
                // lookup pin
                Pin targetPin = _mapRenderer.GetPin((IMKAnnotation)annotationObject);

                // pin not found. Must have been activated outside of forms
                if (targetPin == null)
                    return;

                // if the tap happened on the annotation view itself, skip because this is what happens when the callout is showing
                // when the callout is already visible the tap comes in on a different view
                if (_lastTouchedView is MKAnnotationView)
                    return;

                //targetPin.SendTap();
                typeof(Pin).GetMethod("SendTap", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(targetPin, null);
            }
        }

    }
}