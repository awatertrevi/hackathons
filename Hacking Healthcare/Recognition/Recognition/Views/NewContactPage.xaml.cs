using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Recognition.Utilities;
using Recognition.Views.Controls;
using System.Threading.Tasks;
using Recognition.ViewModels;
using UIKit;
using Foundation;
using Plugin.Media;
using System.IO;

namespace Recognition.Views
{
	public partial class NewContactPage : ContentPage
	{
		private NewContactPageViewModel ViewModel => (NewContactPageViewModel)BindingContext;

		public NewContactPage()
		{
			InitializeComponent();

			BindingContext = new NewContactPageViewModel(Navigation);

			MessagingCenter.Subscribe<NewContactPageViewModel, string>(this, "Error", async (sender, message) =>
			{
				await DisplayAlert("Oeps!", message, "OK");
			});
		}

		async void Handle_Tapped(object sender, System.EventArgs e)
		{
			if (CrossMedia.Current.IsPickPhotoSupported)
			{
				var photo = await CrossMedia.Current.PickPhotoAsync();

				if (photo != null)
				{
					preview_image.Source = ImageSource.FromStream(() => photo.GetStream());
					ViewModel.Image = ReadFully(photo.GetStream());
				}
			}
		}

		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (var ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}
	}
}