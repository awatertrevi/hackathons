//
// NavigationHandler.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System.Threading.Tasks;

using Xamarin.Forms;

namespace FoundIt.Utilities.Helpers
{
    /// <summary>
    /// Helper navigation service to use so we don't push multiple pages at the same time. It also sets the statusbar color on iOS.
    /// </summary>
    public static class NavigationHandler
    {
#if __IOS__
        public delegate void NavigationBarColorEventHandler(Color color);
        public static event NavigationBarColorEventHandler StackFormChanged;
#endif

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

#if __IOS__
            if (navigation.ModalStack.Count == 1)
                StackFormChanged?.Invoke(Color.Black);
#endif
        }

        public static async Task PopModalAsync(INavigation navigation, bool animate = true)
        {
            if (navigating)
                return;

#if __IOS__
            if (navigation.ModalStack.Count == 1)
                StackFormChanged?.Invoke(Color.White);
#endif

            navigating = true;

            if (navigation.ModalStack.Count > 0)
                await navigation.PopModalAsync(animate);

            navigating = false;
        }

        #endregion
    }
}

