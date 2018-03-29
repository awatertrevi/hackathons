//
// RootPage.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//

using System;
using FoundIt.Utilities.Helpers;
using FoundIt.Views.Controls;
using Plugin.Connectivity;
using Xamarin.Forms;
using FoundIt.Models;

namespace FoundIt.Views.Pages
{
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            Children.Add(new MapPage());
            Children.Add(new LostItemsPage());
            Children.Add(new FoundItemsPage());
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage.Title;
            //#if DEBUG
            //            SettingsHandler.AccessToken = "LRBn50IBXKreEenlRiJYD-FAbub6FgnB_nDOpnDAJJTdDnQ5vi_eB_NE72AzUrhlubD3ohBQz3Gh_3B3bjHq8HEwegI9Nw1W4SahI2zxPAa5u4dT0dwZdydkko_npaDI1ADOLGKwZCi57LCS3w0LcpwTXsMv3BYJeO2OrZI8iieK6BlBnYC7mhHdewofKxJEI9zpqIjEB0Pc5QEWD4AyGdNERQo5lfckC1TIZ3GhlYmcSHKtLA-ctkjebHCTMlr5lIjEEyWfva1rnkkmMNkiqe3ufpEqtoucLlX--fgPqein58yXAb9PC8bKOYeLr0k23l_S5bAK-NGKAdb66OuVP0a-yJkT2ADjI7gh4wzLfhU02D_lpRbY6hIa5B_t-Mt9N1fjWxAEq52v6q1NMXgNStHc9Ch_dzOnKKVyzSLGY8YVV3omSeI8m6BWtlBUbZ26u-_jdP2uBIWiMl3ot004_4P-Z-U8Bt3G4FKqojo5h7MqC9XkvZ7VypDPJD3eU2db";
            //#else

            if (Application.Current.MainPage != null && Application.Current.MainPage.GetType() == typeof(ExtendedNavigationPage))
            {
                if (ApplicationContext.Current.CurrentUser != null)
                    return;

                if (CurrentPage.GetType() == typeof(MapPage))
                    return;

                if (Navigation.ModalStack.Count < 1 && CrossConnectivity.Current.IsConnected)
                    await NavigationHandler.PushModalAsync(Navigation, new LoginPage());

                else
                    await DisplayAlert("Oeps!", "Om in te loggen heb je een actieve internet verbinding nodig. Probeer het later opnieuw.", "OK");
            }
            //#endif
        }
    }
}