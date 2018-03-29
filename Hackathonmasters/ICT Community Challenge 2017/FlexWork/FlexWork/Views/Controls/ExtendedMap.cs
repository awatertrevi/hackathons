using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FlexWork.Views.Controls
{
	public class ExtendedMap : Map
	{
		public static readonly BindableProperty RouteCoordinatesProperty = BindableProperty.Create<ExtendedMap, List<Position>>(p => p.RouteCoordinates, null, BindingMode.TwoWay);

		public List<Position> RouteCoordinates
		{
			get { return (List<Position>)GetValue(RouteCoordinatesProperty); }
			set { SetValue(RouteCoordinatesProperty, value); }
		}


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
			bindable.SetValue(CalloutViewProperty, value);
		}

		public static View GetCalloutView(this Pin bindable)
		{
			return (View)bindable.GetValue(CalloutViewProperty);
		}

		public static void SetCalloutView(this Pin bindable, View value)
		{
			bindable.SetValue(CalloutViewProperty, value);
		}

		///////////////////////////////////////////

		public static readonly BindableProperty IdProperty =
		  BindableProperty.CreateAttached("Id", typeof(object), typeof(ExtendedPin), null);

		public static object GetId(BindableObject bindable)
		{
			return (object)bindable.GetValue(IdProperty);
		}

		public static void SetId(BindableObject bindable, object value)
		{
			bindable.SetValue(IdProperty, value);
		}

		public static object GetId(this Pin bindable)
		{
			return GetId((BindableObject)bindable);
		}

		public static T GetId<T>(this Pin bindable) where T : class
		{
			return bindable.GetId() as T;
		}

		public static void SetId(this Pin bindable, object value)
		{
			SetId((BindableObject)bindable, value);
		}
	}
}