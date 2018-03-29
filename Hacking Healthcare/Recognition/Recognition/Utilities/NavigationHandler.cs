using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Recognition.Utilities
{
	public class NavigationHandler
	{
		private static bool navigating;

		#region Pages

		public static async Task PushAsync(INavigation navigation, Page page, bool animate = true)
		{
			if (navigating)
				return;

			navigating = true;
			await navigation.PushAsync(page, animate);
			navigating = false;
		}

		public static async Task PopAsync(INavigation navigation, bool animate = true)
		{
			if (navigating)
				return;

			navigating = true;

			if (navigation.NavigationStack.Count > 0)
				await navigation.PopAsync(animate);

			navigating = false;
		}

		#endregion

		#region Modals

		public static async Task PushModalAsync(INavigation navigation, Page page, bool animate = true)
		{
			if (navigating)
				return;

			navigating = true;
			await navigation.PushModalAsync(page, animate);
			navigating = false;
		}

		public static async Task PopModalAsync(INavigation navigation, bool animate = true)
		{
			if (navigating)
				return;

			navigating = true;

			if (navigation.ModalStack.Count > 0)
				await navigation.PopModalAsync(animate);

			navigating = false;
		}

		#endregion
	}
}
