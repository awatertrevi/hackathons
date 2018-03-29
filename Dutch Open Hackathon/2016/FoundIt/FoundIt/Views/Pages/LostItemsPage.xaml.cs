//
// MyLostItemPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using System.Collections.Generic;
using FoundIt.ViewModels;
using Xamarin.Forms;
using FoundIt.Models;
using System.Linq;
using FoundIt.Utilities.Helpers;

namespace FoundIt.Views.Pages
{
    public partial class LostItemsPage : ContentPage
    {
        private LostItemsPageViewModel ViewModel => (LostItemsPageViewModel)BindingContext;

        public LostItemsPage()
        {
            InitializeComponent();

            BindingContext = new LostItemsPageViewModel(Navigation);
        }

        private async void DeleteLostItem(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var lostItem = mi.CommandParameter as LostItem;

            if (await ApplicationContext.Current.DeleteMyLostItem(lostItem))
            {
                if (ViewModel.LoadLostItems.CanExecute(null))
                    ViewModel.LoadLostItems.Execute(null);
            }
        }

        private async void OpenDetail(object sender, ItemTappedEventArgs e)
        {
            await NavigationHandler.PushAsync(Navigation, new DetailLostItemPage(e.Item as LostItem));

            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.LoadLostItems.CanExecute(null))
                ViewModel.LoadLostItems.Execute(null);
        }
    }
}
