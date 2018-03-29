//
// LoginPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoundIt.ViewModels;
using FoundIt.Utilities.Helpers;

namespace FoundIt.Views.Pages
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel ViewModel => (LoginPageViewModel)BindingContext;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new LoginPageViewModel(Navigation);

            App.PostSuccessFacebookAction = async (token) =>
            {
                if (!await ViewModel.DispatchFacebookToken(token))
                {
                    await DisplayAlert("Oeps!", "Er ging iets mis tijdens het inloggen, probeer het later opnieuw.", "OK");
                }

                await NavigationHandler.PopModalAsync(Navigation);
            };

            MessagingCenter.Subscribe<LoginPageViewModel, string>(this, "Error", async (sender, message) =>
            {
                await DisplayAlert("Oeps!", message, "OK");
            });
        }
    }
}
