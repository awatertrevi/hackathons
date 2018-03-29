//
// LostItemPageViewModel.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using FoundIt.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using FoundIt.Utilities.Helpers;
using FoundIt.Views.Pages;
using System.Collections.ObjectModel;
using FoundIt.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoundIt.ViewModels
{
    public class LostItemsPageViewModel : ViewModelBase
    {
        public ICommand AddNewLostItem { get { return get(() => this.AddNewLostItem); } set { set(() => this.AddNewLostItem, value); } }
        public ICommand LoadLostItems { get { return get(() => this.LoadLostItems); } set { set(() => this.LoadLostItems, value); } }
        public ICommand LoadFoundItems { get { return get(() => this.LoadFoundItems); } set { set(() => this.LoadFoundItems, value); } }

        public ObservableCollection<LostItemGrouping> LostItems { get { return get(() => this.LostItems); } set { set(() => this.LostItems, value); } }
        public ObservableCollection<FoundItem> FoundItems { get { return get(() => this.FoundItems); } set { set(() => this.FoundItems, value); } }

        public LostItemsPageViewModel(INavigation navigation) : base(navigation)
        {
            AddNewLostItem = new Command(async () =>
            {
                await NavigationHandler.PushAsync(Navigation, new AddLostItemPage());
            });

            LoadLostItems = new Command(async () =>
            {
                IsLoading = true;

                var restResult = await ApplicationContext.Current.GetMyLostItems();

                if (restResult.Success)
                    LostItems = GroupLostItems(restResult.Result);

                IsLoading = false;
            });

            LoadFoundItems = new Command(async () =>
            {
                IsLoading = true;

                var restResult = await ApplicationContext.Current.GetMyRetrievalRequests();

                if (restResult.Success)
                    FoundItems = restResult.Result;

                IsLoading = false;
            });

            ApplicationContext.Current.CurrentUserChanged += (user) =>
            {
                if (LoadLostItems.CanExecute(null))
                    LoadLostItems.Execute(null);
            };
        }

        public ObservableCollection<LostItemGrouping> GroupLostItems(IEnumerable<LostItem> lostItems)
        {
            return new ObservableCollection<LostItemGrouping>(lostItems.OrderBy(l => l.EstimatedLoseTime)
                                                                       .GroupBy(e => e.EstimatedLoseTime?.ToString("dddd, d MMMM") ?? "Unknown")
                                                                       .Select(e => new LostItemGrouping(e.Key, e.OrderBy(ex => ex.EstimatedLoseTime))));
        }
    }
}
