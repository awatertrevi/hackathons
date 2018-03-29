//
// DateTimePicker.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 11/25/2016
//
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FoundIt.Views.Controls
{
    public class DateTimePicker : ContentView
    {
        private DatePicker datePicker;
        private TimePicker timePicker;

        public static readonly BindableProperty DateTimeProperty = BindableProperty.Create<DateTimePicker, DateTime>(p => p.DateTime, DateTime.Today, BindingMode.TwoWay);

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        private void PickerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Date" || propertyChangedEventArgs.PropertyName == "Time")
                DateTime = datePicker.Date.Add(timePicker.Time);
        }

        public DateTimePicker()
        {
            datePicker = new DatePicker();
            timePicker = new TimePicker();

            datePicker.PropertyChanged += PickerOnPropertyChanged;
            timePicker.PropertyChanged += PickerOnPropertyChanged;

            datePicker.HorizontalOptions = LayoutOptions.Fill;
            timePicker.HorizontalOptions = LayoutOptions.Fill;

            var grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },

                    new ColumnDefinition()
                    {
                        Width = new GridLength(90, GridUnitType.Absolute)
                    }
                }
            };

            grid.Children.Add(datePicker, 0, 0);
            grid.Children.Add(timePicker, 1, 0);

            Content = grid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            timePicker.Time = DateTime.TimeOfDay;
            datePicker.Date = DateTime.Date;
        }
    }
}
