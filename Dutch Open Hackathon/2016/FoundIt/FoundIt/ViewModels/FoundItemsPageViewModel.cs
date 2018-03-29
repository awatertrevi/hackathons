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
    public class FoundItemsPageViewModel : ViewModelBase
    {
        public ICommand LoadFoundItems { get { return get(() => this.LoadFoundItems); } set { set(() => this.LoadFoundItems, value); } }

        public ObservableCollection<FoundItem> FoundItems { get { return get(() => this.FoundItems); } set { set(() => this.FoundItems, value); } }

        public FoundItemsPageViewModel(INavigation navigation) : base(navigation)
        {
            LoadFoundItems = new Command(async () =>
            {
                IsLoading = true;

                var restResult = await ApplicationContext.Current.GetMyRetrievalRequests();

                if (restResult.Success)
                    FoundItems = restResult.Result;

                IsLoading = false;
            });
        }
    }
}
