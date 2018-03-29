using System;
using Xamarin.Forms;

namespace FlexWork.Views.Controls
{
	public class RoundedBoxView : BoxView
	{
		public static BindableProperty ShapeProperty = BindableProperty.Create(nameof(Shape), typeof(ShapeName), typeof(RoundedBoxView), ShapeName.Circle, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
		{
			((RoundedBoxView)bindable).Shape = (ShapeName)newValue;
		});

		public ShapeName Shape { get { return (ShapeName)GetValue(ShapeProperty); } set { SetValue(ShapeProperty, value); } }

		public enum ShapeName
		{
			Circle,
			Ring
		}
	}
}