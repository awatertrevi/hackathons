using System;
using Xamarin.Forms;
using Recognition.Utilities;
namespace Recognition.Views
{
	public class RootPage : CarouselPage
	{
		private RecognizerPage recognizerPage = new RecognizerPage();

		public static ShouldSnapChanged CurrentShouldSnapChanged;
		public delegate void ShouldSnapChanged(bool shouldSnap);

		public RootPage()
		{
			Children.Add(recognizerPage);
			Children.Add(new ContactsPage());
		}

		protected override void OnCurrentPageChanged()
		{
			base.OnCurrentPageChanged();

			Title = CurrentPage.Title;

			if (CurrentPage.GetType() == typeof(ContactsPage))
			{
				CurrentShouldSnapChanged?.Invoke(false);
				var addContactItem = new ToolbarItem()
				{
					Icon = "add.png"
				};

				addContactItem.Clicked += async (sender, e) =>
				{
					await NavigationHandler.PushAsync(Navigation, new NewContactPage());
				};

				ToolbarItems.Add(addContactItem);
			}

			else
			{
				CurrentShouldSnapChanged?.Invoke(true);
				ToolbarItems.Clear();
			}
		}
	}
}
