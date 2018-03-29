using System;

using Xamarin.Forms;

namespace What
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RootPage())
            {
                BarTextColor = Color.White
            };
        }
    }
}
