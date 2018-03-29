//
// AddLostItemPageViewModel.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/9/2016
//
using System;
using FoundIt.ComponentModel;
using Xamarin.Forms;
using FoundIt.Models;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Windows.Input;
using FoundIt.Utilities.Helpers;

namespace FoundIt.ViewModels
{
    public class DetailLostItemViewModel : ViewModelBase
    {
        public LostItem LostItem { get { return get(() => this.LostItem); } set { set(() => this.LostItem, value); } }

        public ICommand NavigateToFoundIt { get { return get(() => this.NavigateToFoundIt); } set { set(() => this.NavigateToFoundIt, value); } }

        public DetailLostItemViewModel(INavigation navigation, LostItem lostItem) : base(navigation)
        {
            LostItem = lostItem;
        }
    }
}
