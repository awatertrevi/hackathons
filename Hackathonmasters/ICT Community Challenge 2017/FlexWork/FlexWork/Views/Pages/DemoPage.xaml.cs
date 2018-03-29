using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using FlexWork.Models;

namespace FlexWork.Views.Controls
{
	public partial class DemoPage : ContentPage
	{
		public IEnumerable<DemoItem> Items { get; set; }

		public DemoPage()
		{
			InitializeComponent();
		}

		public DemoPage(IEnumerable<DemoItem> items)
		{
			InitializeComponent();

			Items = items;

			BindingContext = this;

			DrawDots();

			carousel.PositionSelected += (sender, e) =>
			{
				DrawDots();
			};
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			Navigation.PopModalAsync();
		}

		private async void DrawDots()
		{
			dots.Children.Clear();

			RoundedBoxView currentBox = null;

			for (int i = 0; i < Items.Count(); i++)
			{
				var box = new RoundedBoxView()
				{
					WidthRequest = 10,
					HeightRequest = 10,
					BackgroundColor = Color.FromHex("#40FFFFFF"),
				};

				if (carousel.Position == i)
				{
					currentBox = box;
					currentBox.BackgroundColor = Color.White;
				}

				dots.Children.Add(box);
			}

			if (currentBox != null)
			{
				await currentBox.ScaleTo(1.25, 75);
				await currentBox.ScaleTo(1, 125);
			}
		}
	}
}
