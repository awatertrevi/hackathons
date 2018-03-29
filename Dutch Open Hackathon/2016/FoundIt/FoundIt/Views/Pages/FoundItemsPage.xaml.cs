//
// FoundItemsPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 1/23/2017
//
using System;
using System.Collections.Generic;
using FoundIt.Models;
using FoundIt.Utilities.Helpers;
using Xamarin.Forms;
using FoundIt.ViewModels;

namespace FoundIt.Views.Pages
{
    public partial class FoundItemsPage : ContentPage
    {
        private FoundItemsPageViewModel ViewModel => (FoundItemsPageViewModel)BindingContext;

        public FoundItemsPage()
        {
            InitializeComponent();

            BindingContext = new FoundItemsPageViewModel(Navigation);
        }

        private async void DeleteFoundItem(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var foundItem = mi.CommandParameter as FoundItem;

            if (await ApplicationContext.Current.DeleteMyFoundItem(foundItem))
            {
                ViewModel.FoundItems.Remove(mi.CommandParameter as FoundItem);
            }
        }

        async void OpenRetrievalDetail(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            await NavigationHandler.PushAsync(Navigation, new FoundPage(e.Item as FoundItem, true));

            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.LoadFoundItems.CanExecute(null))
                ViewModel.LoadFoundItems.Execute(null);
        }
    }
}
