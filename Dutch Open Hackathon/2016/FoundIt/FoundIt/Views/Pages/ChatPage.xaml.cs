//
// ChatPage.xaml.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoundIt.ViewModels;
using FoundIt.Models;

namespace FoundIt.Views.Pages
{
    public partial class ChatPage : ContentPage
    {
        private ChatPageViewModel ViewModel => (ChatPageViewModel)BindingContext;

        public ChatPage(FoundItem item)
        {
            InitializeComponent();

            BindingContext = new ChatPageViewModel(Navigation, item);

            ViewModel.Messages.CollectionChanged += (sender, e) =>
            {
                var target = ViewModel.Messages[ViewModel.Messages.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            };
        }
    }
}
