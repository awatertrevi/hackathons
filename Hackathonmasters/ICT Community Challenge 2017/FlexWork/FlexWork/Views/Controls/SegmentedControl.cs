//
// SegmentedControl.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Xamarin.Forms;

namespace FlexWork.Views.Controls
{
	[ContentProperty("Children")]
	public class SegmentedControl : View, IViewContainer<SegmentedControlSegmentOption>
	{
		#region BindableProperties

		public event EventHandler<SelectedItemChangedEventArgs> SelectedValueChanged;

		public static readonly BindableProperty SelectedValueProperty =
			BindableProperty.Create(nameof(SelectedValue), typeof(string), typeof(SegmentedControl), string.Empty,
				BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
				{
					var ctrl = bindable as SegmentedControl;

					ctrl.SelectedValueChanged?.Invoke(ctrl, new SelectedItemChangedEventArgs(newValue));
				}
			);
		public string SelectedValue
		{
			get { return (string)GetValue(SelectedValueProperty); }
			set { SetValue(SelectedValueProperty, value); }
		}

		#endregion

		public IList<SegmentedControlSegmentOption> Children
		{
			get;
			private set;
		}

		public SegmentedControl()
		{
			Children = new ObservableCollection<SegmentedControlSegmentOption>();
			((ObservableCollection<SegmentedControlSegmentOption>)Children).CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
			{
				this.OnPropertyChanged(nameof(Children));
			};
		}
	}

	public class SegmentedControlSegmentOption : View
	{
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(SegmentedControlSegmentOption), string.Empty);

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
	}
}