//
// ExtendedNavigationPage.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using FoundIt.Utilities.Helpers;
using Xamarin.Forms;

namespace FoundIt.Views.Controls
{
    public class ExtendedNavigationPage : NavigationPage
    {
        public ExtendedNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public ExtendedNavigationPage()
        {
            Init();
        }

        private void Init()
        {
#if __IOS__
            BarTextColor = Color.White;

            NavigationHandler.StackFormChanged += (Color color) =>
            {
                BarTextColor = color;
            };
#endif
        }
    }
}
