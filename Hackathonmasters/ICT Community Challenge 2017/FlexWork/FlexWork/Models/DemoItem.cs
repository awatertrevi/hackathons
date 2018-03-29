using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace FlexWork.Models
{
	public class DemoItem
	{
		public string Title { get; set; }
		public string Text { get; set; }

		public bool HasImage { get; set; }
		public ImageSource Image { get; set; }

		public bool HasButton { get; set; }
		public string ButtonText { get; set; }

		public Color TextColor { get; set; }

		public Color ButtonTextColor { get; set; }
		public Color ButtonBackgroundColor { get; set; }

		public ICommand ButtonClickCommand { get; set; }

		public DemoItem()
		{
			HasImage = true;
			HasButton = false;
			TextColor = Color.FromHex("#F2EFEF");
			ButtonBackgroundColor = Color.White;
			ButtonTextColor = Color.Black;
		}
	}
}
