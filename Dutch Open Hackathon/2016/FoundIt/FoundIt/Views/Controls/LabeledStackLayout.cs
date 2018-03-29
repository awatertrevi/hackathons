//
// LabeledInputField.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 11/25/2016
//

using System;
using Xamarin.Forms;

namespace FoundIt.Views.Controls
{
    public class LabeledStackLayout : StackLayout
    {
        private ContentView content = new ContentView();

        public LabeledStackLayout()
        {
            var label = new Label()
            {
                TextColor = Color.FromHex("#3F5B84"),
                FontSize = 20.0F
            };

            label.BindingContext = this;

            var view = new ContentView();

            label.SetBinding(Label.TextProperty, new Binding("Text"));

            Children.Add(label);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(LabeledStackLayout), string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
