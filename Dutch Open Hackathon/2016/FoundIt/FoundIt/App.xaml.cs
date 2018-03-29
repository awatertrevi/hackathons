//
// App.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using FoundIt.Views.Pages;
using Xamarin.Forms;
using FoundIt.Views.Controls;
using System;
using FoundIt.Utilities.Helpers;
using System.Threading.Tasks;

namespace FoundIt
{
    public partial class App : Application
    {
        public static Action<string> PostSuccessFacebookAction { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new ExtendedNavigationPage(new RootPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Sets the tab in the RootPage.
        /// </summary>
        /// <param name="tab">Which tab you want.</param>
        public static void SetTabTo(int tab)
        {
            if (Current.MainPage.GetType() == typeof(ExtendedNavigationPage))
            {
                var mainPage = Current.MainPage as ExtendedNavigationPage;
                var rootPage = mainPage.CurrentPage as TabbedPage;

                rootPage.CurrentPage = rootPage.Children[tab];
            }
        }
    }
}
